using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Equipment;

namespace DataManagement.TableClass.Equipment
{
  [System.Serializable]
  public class EquipmentReinforceCostTable : AbstractTable 
  {
    public int Level;
    public EQUIPMENT_REINFORCE_COST_TYPE CostType;
    public int CostNumber;
  }
}
