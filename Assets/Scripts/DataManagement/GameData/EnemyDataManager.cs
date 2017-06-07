using UnityEngine;
using System.Collections;
using Common;
using DataManagement.GameData.FormatCollection;
using System.Collections.Generic;
using DataManagement.SaveData;
using DataManagement.TableClass.BattleInfo;
using System.Linq;
using DataManagement.TableClass.Enemy;
using ConstCollections.PJEnums.Character;
using ConstCollections.PJEnums.Battle;
using DataManagement.SaveData.FormatCollection;

namespace DataManagement.GameData
{
  public class EnemyDataManager : Singleton<EnemyDataManager> 
  {
    public List<EnemyDataFormat> LoadEnemyData()
    {
      switch (BattleDataManager.Instance.BattleType) 
      {
      case BATTLE_TYPE.MONSTER_BATTLE:
        return LoadMonsterBattleEnemyData();
      case BATTLE_TYPE.BOSS_BATTLE:
        return LoadBossBattleEnemyData ();
      case BATTLE_TYPE.EX_BOSS_BATTLE:
        return null;
      default:
        return null;
      }
    }

    public List<EnemyDataFormat> LoadMonsterBattleEnemyData()
    {
      var _battleSaveData = BattleDataManager.Instance.BattleDataCache;

      var _monsterBattleTableList = MonsterBattleTableReader.Instance.GenerateListByProbability(_battleSaveData.Area, _battleSaveData.Level);

      var _monsterTableListWithSlotID = _monsterBattleTableList.FindAll (item => {
        return item.SlotID != -1;
      });

      var _monsterTableListWithoutSlotID = _monsterBattleTableList.FindAll (item => {
        return item.SlotID == -1;
      });

      var _enemyList = new List<EnemyDataFormat> ();
      var _slotIDList = Enumerable.Repeat<bool> (false, _monsterBattleTableList.Count).ToList();//new bool[_monsterTableList.Count];

      if (_monsterTableListWithSlotID != null && _monsterTableListWithSlotID.Count > 0) 
      {
        foreach (var _monsterData in _monsterTableListWithSlotID) {
          int _slotID = _monsterData.SlotID;
          if (_slotIDList [_slotID]) 
          {
            _slotID = _slotIDList.FindIndex (item => {
              return item == false;
            });
          }
          _slotIDList [_slotID] = true;

          var _enemy = new EnemyDataFormat(_slotID, MonsterTableReader.Instance.FindDefaultUnique(_monsterData.MonsterID));
          _enemyList.Add (_enemy);
        }
      }

      if (_monsterTableListWithoutSlotID != null && _monsterTableListWithoutSlotID.Count > 0) 
      {
        foreach (var _monsterData in _monsterTableListWithoutSlotID) {
          int _slotID = _slotIDList.ToList ().FindIndex (item => {
            return item == false;
          });
          _slotIDList [_slotID] = true;

          var _enemy = new EnemyDataFormat(_slotID, MonsterTableReader.Instance.FindDefaultUnique(_monsterData.MonsterID));
          _enemyList.Add (_enemy);
        }
      }

      if (_enemyList.Count == 0)
        return null;

      return _enemyList;
    }


    public List<EnemyDataFormat> LoadBossBattleEnemyData()
    {
      var _battleSaveData = BattleDataManager.Instance.BattleDataCache;

      var _bossBattleTableList = BossBattleTableReader.Instance.FindDefault(_battleSaveData.Area, _battleSaveData.Level);

      var _enemyList = new List<EnemyDataFormat> ();
      _bossBattleTableList.ForEach (enemyInfo => {
        EnemyDataFormat _enemy = null;
        EnemySaveDataFormat _savedEnemyData = null;

        if(_battleSaveData.BossBattleEnemyList != null && _battleSaveData.BossBattleEnemyList.Count > 0)
        {
          _savedEnemyData = _battleSaveData.BossBattleEnemyList.Find(data => {
            return data.EnemyID == enemyInfo.EnemyID && data.Type == enemyInfo.Type;
          });
        }

        if(enemyInfo.Type == ENEMY_TYPE.MONSTER)
          _enemy = new EnemyDataFormat(enemyInfo.SlotID, MonsterTableReader.Instance.FindDefaultUnique(enemyInfo.EnemyID), _savedEnemyData);
        else if(enemyInfo.Type == ENEMY_TYPE.BOSS)
          _enemy = new EnemyDataFormat(enemyInfo.SlotID, BossTableReader.Instance.FindDefaultUnique(enemyInfo.EnemyID), _savedEnemyData);
        _enemyList.Add (_enemy);
      });
        
      if (_enemyList.Count == 0)
        return null;

      return _enemyList;
    }
  }
}
