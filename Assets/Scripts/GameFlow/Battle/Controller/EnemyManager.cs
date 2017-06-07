using UnityEngine;
using System.Collections;
using GameFlow.Battle.Common.Controller;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection;
using System.Linq;
using DataManagement.TableClass;
using DataManagement.TableClass.Enemy;
using ConstCollections.PJConstOthers;
using ConstCollections.PJEnums.Character;
using DataManagement.TableClass.BattleInfo;
using DataManagement.GameData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;

namespace GameFlow.Battle.Controller
{
  public class EnemyManager : AbsCharacterManager 
  {
    public event System.Action<CHARACTER_TYPE,List<AbsCharacterController>> EnemyControllerListChangedEvent = delegate(CHARACTER_TYPE characterType,List<AbsCharacterController> obj) {};

    #region implemented abstract members of CharacterBaseManager

    public override bool AllDead
    {
      get
      { 
        bool _allDead = true;
        this.ControllerList.ForEach (_enemy => {
          if(_enemy.State == ConstCollections.PJEnums.Character.STATES.ALIVE)
          {
            _allDead = false;
          }
        });

        return _allDead;
      }
    }

    public override List<AbsCharacterController> AliveList{
      get
      { 
        if (this.AllDead)
          return null;

        List<AbsCharacterController> _aliveList = this.ControllerList.FindAll (_enemy => {
          return _enemy.State == ConstCollections.PJEnums.Character.STATES.ALIVE;
        });

        return _aliveList;
      }
    } 

    public override void InitFight()
    {
      base.InitFight ();

      this.CharacterType = CHARACTER_TYPE.ENEMY;

      var _enemyDataList = EnemyDataManager.Instance.LoadEnemyData ();

      for (int i = 0; i < _enemyDataList.Count; i++) 
      {
        int _slotID = _enemyDataList [i].Attributes.SlotID;

        EnemyController _enemyController = this.ControllerList.Find (col => col.SlotID == _slotID) as EnemyController;

        if (_enemyController == null) {
          var _gameObject = Instantiate (this.CharacterPrefab, this.SlotTransformList [_slotID], false) as GameObject;
          _enemyController = _gameObject.GetComponent<EnemyController> ();
          _enemyController.InitFight(_enemyDataList [i]);
          this.ControllerList.Add (_enemyController);
        } 
        else 
        {
          _enemyController.gameObject.SetActive (true);
          _enemyController.InitFight (_enemyDataList [i]);
        }
      }

      IComparer<EnemyController> _sortBySlotID = new SortEnemyControllerBySlotID (); 
      this.ControllerList.Cast<EnemyController> ().ToList ().Sort (_sortBySlotID);
      this.EnemyControllerListChangedEvent.Invoke (CHARACTER_TYPE.ENEMY,this.ControllerList);
    }

    public override IEnumerator OneTurnCoroutine (AbsCharacterManager otherManager)
    {
      yield return base.OneTurnCoroutine (otherManager);

      // Save data for Boss Battle
      if (BattleDataManager.Instance.BattleType == ConstCollections.PJEnums.Battle.BATTLE_TYPE.BOSS_BATTLE) 
      {
        List<EnemySaveDataFormat> _enemySaveDataList = new List<EnemySaveDataFormat> ();
        this.ControllerList.ForEach (col => {
          EnemyController _enemyController = col as EnemyController;
          var _savedata = new EnemySaveDataFormat(_enemyController.EnemyDataCache.Attributes, _enemyController.FightData.AttributesAggregateBuff);
          _enemySaveDataList.Add(_savedata);
        });

        BattleSaveDataManager.Instance.SaveEnemyData (_enemySaveDataList);
      }
    }

    #endregion
  }
}
