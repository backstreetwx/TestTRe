using UnityEngine;
using System.Collections;
using Common;
using DataManagement.GameData.FormatCollection;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;
using DataManagement.TableClass.BattleInfo;
using PJMath;
using ConstCollections.PJConstOthers;
using ConstCollections.PJEnums.Battle;
using GameFlow.Battle.Controller;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData
{
  public class BattleDataManager : Singleton<BattleDataManager> 
  {
    public event System.Action<BattleDataFormat> BattleDataCacheChangedEvent = delegate(BattleDataFormat obj) {};
    public event System.Action<short,short> BattleAreaLevelChangedEvent = delegate(short area,short level) {};

    public BattleDataFormat BattleDataCache
    {
      get
      { 
        return this.cache;
      }
    }

    public BATTLE_TYPE BattleType
    {
      get
      {
        return this.cache.CurrentBattleType;
      }
      set
      {
        this.cache.CurrentBattleType = value;
      }
    }

    public BattleDataManager()
    {
      this.cache = new BattleDataFormat (BattleSaveDataManager.Instance.BattleSaveData);
      BattleSaveDataManager.Instance.BattleSaveDataChangedEvent += UpdateBySaveData;
    }

    public string GetBattleBGPath()
    {
      var _data = BattleAreaLevelTableReader.Instance.FindDefaultUnique (this.cache.Area, this.cache.Level);
      return _data.TexturePath;
    }

    public string GetBattleBGPath(short area,short level)
    {
      var _data = BattleAreaLevelTableReader.Instance.FindDefaultUnique (area, level);
      return _data.TexturePath;
    }

    public bool HitSuccess (FightDataFormat attacker, FightDataFormat defender)
    {
      return attacker.OneTurnFightData.HitSuccess (attacker, defender);
    }

    public bool BlastSuccess (FightDataFormat attacker, FightDataFormat defender)
    {
      return attacker.OneTurnFightData.BlastSuccess (attacker, defender);
    }

    public float CalculateBlastPower(FightDataFormat attacker, FightDataFormat defender)
    {
      return attacker.OneTurnFightData.CalculateBlastPower (attacker, defender);
    }

    public float CalculateAttackerPower (FightDataFormat attacker, FightDataFormat defender)
    {
      return attacker.OneTurnFightData.CalculateAttackPower(attacker, defender);
    }

    public int CalculateDamageToOther (FightDataFormat attacker, FightDataFormat defender)
    {
      return attacker.OneTurnFightData.CalculateDamageToOther(attacker, defender);
    }

    void UpdateBySaveData(BattleSaveDataFormat saveData)
    {
      bool _updateLevelArea = false;
      if (this.cache.Level != saveData.Level || this.cache.Area != saveData.Area)
        _updateLevelArea = true;
      
      this.cache = new BattleDataFormat(saveData);

      BattleDataCacheChangedEvent.Invoke (this.cache);

      if(_updateLevelArea)
        BattleAreaLevelChangedEvent.Invoke (this.cache.Area,this.cache.Level);
    }
      
    BattleDataFormat cache;
  }

}
