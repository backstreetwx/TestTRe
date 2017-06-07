using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.TableClass.TableReaderBase;
using System.Linq;
using System.Reflection;
using DataManagement.Common;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{
  public class EquipmentReinforceValueRangeTableReaderBase : AbstractTableReader<EquipmentReinforceValueRangeTable> {

    public static string ColumnLevelName = "Level";

    public override string TablePath {
      get {
        return "Data/Equipment/equipment_reinforce_value_range.csv";
      }
    }

    public virtual EquipmentReinforceValueRangeTable FindDefaultUniqueByLevel(int level)
    {
      var _cacheList =  this.DefaultCachedList.FindAll (row => {
        return row.Level == level; 
      });

      if (_cacheList.Count == 0)
        throw new System.NullReferenceException ();

      if (_cacheList.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _cacheList[0];

    }
  	
  }
}
