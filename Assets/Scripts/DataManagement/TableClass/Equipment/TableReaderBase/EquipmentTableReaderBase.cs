using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Linq;
using System.Reflection;
using DataManagement.Common;
using System.Collections.Generic;
using ConstCollections.PJEnums.Equipment;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{
  public class EquipmentTableReaderBase : AbstractTableReader<EquipmentTable>  {

    public static string ColumnEquipmentTypeName = "EquipmentType";
    public static string ColumnQualityGradeName = "QualityGrade";

    public override string TablePath {
      get {
        return "Data/Equipment/equipment.csv";
      }
    }

    public virtual List<EquipmentTable> FindDefault(EQUIPMENT_TYPE type, short qualityGrade)
    {

      return this.DefaultCachedList.FindAll (row => {
        return row.EquipmentType == type&&row.QualityGrade == qualityGrade;
      });

    }
  }
}
