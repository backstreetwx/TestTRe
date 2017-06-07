using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using DataManagement.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataManagement.TableClass.BattleInfo.TableReaderBase
{
  public class BattleAreaTableReaderBase : AbstractTableReader<BattleAreaTable> 
  {
    public static string ColumnAreaName = "Area";

    public override string TablePath {
      get {
        return "Data/BattleInfo/battle_area.csv";
      }
    }

    public virtual BattleAreaTable FindDefaultUniqueByArea(short area)
    {
      using(var _reader = FileIO.Instance.CSVReader<BattleAreaTable>(TablePath))
      {
        List<BattleAreaTable> _rows = _reader.Where(_row => {
          // field
          FieldInfo _areaIDInfo = _row.GetType ().GetField (ColumnAreaName, BindingFlags.Instance | BindingFlags.Public);
          if(_areaIDInfo == null)
            return false;

          return ((short)_areaIDInfo.GetValue (_row)) == area;
        }).ToList ();

        if (_rows.Count == 0)
          throw new System.NullReferenceException ();

        if (_rows.Count > 1)
          throw new System.Exception ("Find Unique but got duplicated!");

        return _rows[0];
      }
    }

    public int GetAreaCount()
    {
      using (var _reader = FileIO.Instance.CSVReader<BattleAreaLevelTable> (TablePath)) 
      {
        var _count = _reader.Count();

        return _count;
      }
    }
  }
}
