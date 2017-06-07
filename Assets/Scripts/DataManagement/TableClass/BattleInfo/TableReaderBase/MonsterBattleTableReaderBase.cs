using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.Common;
using System.Linq;
using System.Reflection;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.BattleInfo.TableReaderBase
{
  public class MonsterBattleTableReaderBase : AbstractTableReader<MonsterBattleTable> 
  {
    public static string ColumnAreaLevelIDName = "AreaLevelID";
    public static string ColumnAreaName = "Area";
    public static string ColumnLevelName = "Level";

    public override string TablePath 
    {
      get {
        return "Data/BattleInfo/monster_battle.csv";
      }
    }

    public virtual List<MonsterBattleTable> FindDefault(ushort areaLevelID)
    {
      using(var _reader = FileIO.Instance.CSVReader<MonsterBattleTable>(TablePath))
      {
        List<MonsterBattleTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaLevelIDName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;

          return ((ushort)_areaIDInfo.GetValue (_row)) == areaLevelID;
        }).ToList ();

        return _rows;
      }
    }

    public virtual List<MonsterBattleTable> FindDefault(short area, short level)
    {
      using(var _reader = FileIO.Instance.CSVReader<MonsterBattleTable>(TablePath))
      {
        List<MonsterBattleTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaInfo = _row.GetType ().GetField (ColumnAreaName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaInfo == null)
            return false;

          FieldInfo _levelInfo = _row.GetType ().GetField (ColumnLevelName, BindingFlags.Instance | BindingFlags.Public);
          if(_levelInfo == null)
            return false;

          return ((short)_areaInfo.GetValue (_row)) == area && 
            ((short)_levelInfo.GetValue (_row)) == level;
        }).ToList ();

        return _rows;
      }
    }

    public List<MonsterBattleTable> GenerateListByProbability(short area, short level)
    {
      var _currentLevelinfo = BattleAreaLevelTableReader.Instance.FindDefaultUnique(area, level);
      if (_currentLevelinfo == null)
        throw new System.NullReferenceException ("_currentLevelinfo");

      var _monsterList = MonsterBattleTableReader.Instance.FindDefault (_currentLevelinfo.ID);
      if (_monsterList == null)
        throw new System.NullReferenceException ("_monsterList");

      int _monsterCount = MonsterBattleSpawnTableReader.Instance.CalculateCount(_currentLevelinfo.ID);

      int[] _probabilityList = new int[_monsterList.Count];
      for (int i = 0; i < _monsterList.Count; i++) 
      {
        _probabilityList [i] = _monsterList [i].Probability;
      }

      List<MonsterBattleTable> _list = new List<MonsterBattleTable> ();
      for (int i = 0; i < _monsterCount; i++) 
      {
        int _monsterIndex = PJMath.ProbabilityHelper.CalculateIndex (_probabilityList);
        _list.Add (_monsterList [_monsterIndex]);
      }

      if (_list.Count == 0)
        return null;

      return _list;
    }
  }
}
