using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Battle;
using System.Collections.Generic;
using DataManagement.SaveData.FormatCollection;
using DataManagement.TableClass.BattleInfo;
using Common;

namespace DataManagement.GameData.FormatCollection.Battle
{
  [System.Serializable]
  public class BattleDataFormat
  {
    public short Area;
    public short Level;
    public int SearchPointForBoss;
    public int SearchPointOutput;
    public int SearchPointPunishment;
    public bool EnableBossBattle;
    public BATTLE_TYPE CurrentBattleType;
    public List<EnemySaveDataFormat> BossBattleEnemyList;

    public BattleDataFormat()
    {
      this.Area = -1;
      this.Level = -1;
      this.SearchPointForBoss = -1;
    }

    public BattleDataFormat(short area, short level)
    {
      this.Area = area;
      this.Level = level;

      var _battleAreaLevel = BattleAreaLevelTableReader.Instance.FindDefaultUnique (area, level);
      this.SearchPointOutput = _battleAreaLevel.SearchPointOutput;
      this.SearchPointPunishment = _battleAreaLevel.SearchPointPunishment;
      this.SearchPointForBoss = _battleAreaLevel.SearchPointForBoss;
    }

    public BattleDataFormat(BattleSaveDataFormat saveData)
    {
      this.Area = saveData.Area;
      this.Level = saveData.Level;
      this.EnableBossBattle = saveData.EnableBossBattle;
      this.CurrentBattleType = saveData.CurrentBattleType;

      var _battleAreaLevel = BattleAreaLevelTableReader.Instance.FindDefaultUnique (saveData.Area, saveData.Level);
      this.SearchPointOutput = _battleAreaLevel.SearchPointOutput;
      this.SearchPointPunishment = _battleAreaLevel.SearchPointPunishment;
      this.SearchPointForBoss = _battleAreaLevel.SearchPointForBoss;

      if(saveData.BossBattleEnemyList != null)
        this.BossBattleEnemyList = saveData.BossBattleEnemyList.CloneEx();
    }

    //    public BattleSaveDataFormat ConvertToSaveData()
    //    {
    //      return new BattleSaveDataFormat(this.Area, this.Level, this.EnableBossBattle, this.CurrentBattleType, this.BossBattleEnemyList);
    //    }
  }
}
