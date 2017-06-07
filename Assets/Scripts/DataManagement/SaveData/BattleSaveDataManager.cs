using UnityEngine;
using System.Collections;
using Common;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJEnums.Battle;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection;
using DataManagement.TableClass.BattleInfo;

namespace DataManagement.SaveData
{
  public class BattleSaveDataManager : Singleton<BattleSaveDataManager> 
  {
    public event System.Action<BattleSaveDataFormat> BattleSaveDataChangedEvent = delegate(BattleSaveDataFormat obj) {};

    public BattleSaveDataFormat BattleSaveData
    {
      get{ 
        return UserSaveDataManager.Instance.UserBasic.BattleSaveData;
      }
    }

    public void MoveToNextLevelOrArea(System.Action OnAllAreaCleared = null)
    {
      var _dataCache = UserSaveDataManager.Instance.UserBasic.BattleSaveData;
      var _areaCount = BattleAreaTableReader.Instance.GetAreaCount ();
      var _levelCount = BattleAreaLevelTableReader.Instance.GetLevelCount (_dataCache.Area);

      if (_dataCache.Level == _levelCount - 1 && _dataCache.Area == _areaCount - 1) 
      {
        // already all cleared

        // Update with current value
        UserSaveDataManager.Instance.SearchProgress = UserSaveDataManager.Instance.SearchProgress;
        return;
      }

      var _data = this.BattleSaveData.CloneEx();

      _data.Level++;

      UserSaveDataManager.Instance.SearchProgress = 0;

      if(_data.Level > _levelCount - 1)
      {
        _data.Level = 0;
        _data.Area++;

        if (_data.Area == _areaCount - 1) 
        {
          if(OnAllAreaCleared != null)
            OnAllAreaCleared.Invoke ();

          this.Overwrite (_data);
          return;
        }
      }
      this.Overwrite (_data);
    }

    public void SaveEnemyData(List<EnemySaveDataFormat> enemySaveDataList)
    {
      var _data = this.BattleSaveData.CloneEx();
      _data.BossBattleEnemyList = enemySaveDataList.CloneEx();

      this.Overwrite (_data);
    }

    public void ClearEnemyData()
    {
      var _data = this.BattleSaveData.CloneEx();

      if (_data.BossBattleEnemyList != null && _data.BossBattleEnemyList.Count != 0) {
        _data.BossBattleEnemyList.Clear ();

        this.Overwrite (_data);
      }
    }

    public void AddSearchPointWhenBattleWin()
    {
      var _data = BattleAreaLevelTableReader.Instance.FindDefaultUnique (this.BattleSaveData.Area, this.BattleSaveData.Level);
      UserSaveDataManager.Instance.SearchProgress += _data.SearchPointOutput;
    }

    public void MinusSearchPointWhenBattleLose()
    {
      var _data = BattleAreaLevelTableReader.Instance.FindDefaultUnique (this.BattleSaveData.Area, this.BattleSaveData.Level);
      UserSaveDataManager.Instance.SearchProgress += _data.SearchPointPunishment;
    }

    public void Save() 
    {
      UserSaveDataManager.Instance.WriteToPlayerPrefs ();
      // This method will be called by Unity automatically when application exiting
      PlayerPrefs.Save ();
    }

    public bool Overwrite(BattleSaveDataFormat data,bool write = true)
    {
      UserSaveDataManager.Instance.UserBasic.BattleSaveData = data.CloneEx ();

      if(write)
        UserSaveDataManager.Instance.WriteToPlayerPrefs ();

      this.BattleSaveDataChangedEvent.Invoke (BattleSaveDataManager.Instance.BattleSaveData);
      return true;
    }
  }
}
