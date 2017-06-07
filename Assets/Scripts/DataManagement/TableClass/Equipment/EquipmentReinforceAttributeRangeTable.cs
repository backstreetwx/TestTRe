using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Equipment;

namespace DataManagement.TableClass.Equipment
{
  [System.Serializable]
  public class EquipmentReinforceAttributeRangeTable : AbstractTable 
  {
    public ATTRIBUTE_TYPE AttributeType;
    public int RandMin;
    public int RandMax;
  }
}
