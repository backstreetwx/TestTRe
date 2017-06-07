using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using ConstCollections.PJEnums.Equipment;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using DataManagement.Common;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{
  public class EquipmentReinforceAttributeRangeTableReaderBase : AbstractTableReader<EquipmentReinforceAttributeRangeTable> {

    public static string ColumnAttributeTypeName = "AttributeType";

    public override string TablePath {
      get {
        return "Data/Equipment/equipment_reinforce_attribute_range.csv";
      }
    }

    public virtual EquipmentReinforceAttributeRangeTable FindDefaultUniqueByAttributeType(ATTRIBUTE_TYPE type)
    {
      
      var _cacheList =  this.DefaultCachedList.FindAll (row => {
        return row.AttributeType == type; 
      });

      if (_cacheList.Count == 0)
        throw new System.NullReferenceException ();

      if (_cacheList.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _cacheList[0];

    }

  }
}
