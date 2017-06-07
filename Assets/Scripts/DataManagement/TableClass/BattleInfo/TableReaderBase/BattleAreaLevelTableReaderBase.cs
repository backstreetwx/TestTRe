using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.Common;
using System.Linq;
using System.Reflection;
using DataManagement.TableClass.BattleInfo;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.BattleInfo.TableReaderBase
{
  public class BattleAreaLevelTableReaderBase : AbstractTableReader<BattleAreaLevelTable> 
  {
    public static string ColumnAreaName = "Area";
    public static string ColumnLevelName = "Level";

    public override string TablePath {
      get {
        return "Data/BattleInfo/battle_area_level.csv";
      }
    }

    public virtual BattleAreaLevelTable FindDefaultUnique(short area, short level)
    {
      using(var _reader = FileIO.Instance.CSVReader<BattleAreaLevelTable>(TablePath))
      {
        List<BattleAreaLevelTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;

          FieldInfo _levelIDInfo = _row.GetType ().GetField (ColumnLevelName, BindingFlags.Instance | BindingFlags.Public);
          if(_levelIDInfo == null)
            return false;

          return ((short)_areaIDInfo.GetValue (_row)) == area &&
            ((short)_levelIDInfo.GetValue (_row)) == level;
        }).ToList ();

        if (_rows.Count == 0)
          throw new System.NullReferenceException ();

        if (_rows.Count > 1)
          throw new System.Exception ("Find Unique but got duplicated!");

        return _rows[0];
      }
    }

    public virtual List<BattleAreaLevelTable> FindDefault(short area)
    {
      using(var _reader = FileIO.Instance.CSVReader<BattleAreaLevelTable>(TablePath))
      {
        List<BattleAreaLevelTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;

          return ((short)_areaIDInfo.GetValue (_row)) == area;
        }).ToList ();

        return _rows;
      }
    }


    public int GetLevelCount(short area)
    {

      using (var _reader = FileIO.Instance.CSVReader<BattleAreaLevelTable> (TablePath)) 
      {
        var _count = _reader.Count(_row => {
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;
          
          return ((short)_areaIDInfo.GetValue (_row)) == area;
        });

        return _count;
      }

    }
  }
}
