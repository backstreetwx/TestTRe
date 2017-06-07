using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataManagement.Common;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{

  public class EquipmentQualityGradeAuraTableReaderBase : AbstractTableReader<EquipmentQualityGradeAuraTable> {

    public static string ColumnQualityGradeName = "QualityGrade";

    public override string TablePath {
      get {
        return "Data/Equipment/equipment_quality_grade_aura.csv";
      }
    }

    public int GetCostAura(int qualityGrade)
    {

      var _cacheList =  this.DefaultCachedList.FindAll (row => {
        return row.QualityGrade == qualityGrade; 
      });

      if (_cacheList.Count == 0)
        throw new System.NullReferenceException ();

      if (_cacheList.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _cacheList[0].CostAura;

    }

  }
}
