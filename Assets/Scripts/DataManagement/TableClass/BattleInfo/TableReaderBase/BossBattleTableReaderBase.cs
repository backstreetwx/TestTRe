using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using DataManagement.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataManagement.TableClass.BattleInfo.TableReaderBase
{
  public class BossBattleTableReaderBase : AbstractTableReader<BossBattleTable> 
  {
    public static string ColumnAreaLevelIDName = "AreaLevelID";
    public static string ColumnAreaName = "Area";
    public static string ColumnLevelName = "Level";

    public override string TablePath {
      get {
        return "Data/BattleInfo/boss_battle.csv";
      }
    }

    public virtual List<BossBattleTable> FindDefault(ushort areaLevelID)
    {
      using(var _reader = FileIO.Instance.CSVReader<BossBattleTable>(TablePath))
      {
        List<BossBattleTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaLevelIDName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;

          return ((ushort)_areaIDInfo.GetValue (_row)) == areaLevelID;
        }).ToList ();

        return _rows;
      }
    }

    public virtual List<BossBattleTable> FindDefault(short area, short level)
    {
      using(var _reader = FileIO.Instance.CSVReader<BossBattleTable>(TablePath))
      {
        List<BossBattleTable> _rows = _reader.Where(_row => {
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
  }
}
