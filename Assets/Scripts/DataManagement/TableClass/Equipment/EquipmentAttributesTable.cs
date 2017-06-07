using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Equipment;

namespace DataManagement.TableClass.Equipment
{
  [System.Serializable]
  public class EquipmentAttributesTable : AbstractTable 
  {
    public ushort EquipmentID;
    public ATTRIBUTE_TYPE AttributeType;
    public int ParameterA;
    public int ParameterB;
  }
}
