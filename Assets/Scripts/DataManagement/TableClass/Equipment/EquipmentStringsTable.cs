using UnityEngine;
using System.Collections;
using DataManagement.Common;

namespace DataManagement.TableClass.Equipment
{
  [System.Serializable]
  public class EquipmentStringsTable : AbsMultiLanguageTable 
  {
    [CsvColumnAttribute(1)]
    public short EquipmentID;
  	
  }
}
