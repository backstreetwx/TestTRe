using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{
  public class EquipmentAttributesReaderBase : AbstractTableReader<EquipmentAttributesTable> 
  {

    public override string TablePath {
      get {
        return "Data/Equipment/equipment_attributes.csv";
      }
    }

    public List<EquipmentAttributesTable> FindDefaultByEquipmentID(ushort equipmentID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.EquipmentID == equipmentID;
      });

    }
  }
}
