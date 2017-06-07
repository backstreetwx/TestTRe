using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.Common;
using System.Linq;
using System.Reflection;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.BattleInfo.TableReaderBase
{
  public class MonsterBattleSpawnTableReaderBase : AbstractTableReader<MonsterBattleSpawnTable> 
  {
    public static string ColumnAreaLevelIDName = "AreaLevelID";

    public override string TablePath 
    {
      get {
        return "Data/BattleInfo/monster_battle_spawn.csv";
      }
    }

    public virtual List<MonsterBattleSpawnTable> FindDefault(ushort areaLevelID)
    {
      using(var _reader = FileIO.Instance.CSVReader<MonsterBattleSpawnTable>(TablePath))
      {
        List<MonsterBattleSpawnTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaLevelIDName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;

          return ((ushort)_areaIDInfo.GetValue (_row)) == areaLevelID;
        }).ToList ();

        return _rows;
      }
    }

    public int CalculateCount(ushort areaLevelID)
    {
      var _spwanProbabilityList = FindDefault (areaLevelID);
      if(_spwanProbabilityList == null)
        throw new System.NullReferenceException ("_spwanProbabilityList");
      
      int[] _probabilityList = new int[_spwanProbabilityList.Count];
      for (int i = 0; i < _spwanProbabilityList.Count; i++) 
      {
        _probabilityList [i] = _spwanProbabilityList [i].Probability;
      }
      int _countIndex = PJMath.ProbabilityHelper.CalculateIndex (_probabilityList);
      int _monsterCount = _spwanProbabilityList[_countIndex].SpawnCount;

      Debug.Log ("_monsterCount =" + _monsterCount);
      return _monsterCount;
    }
  }
}
