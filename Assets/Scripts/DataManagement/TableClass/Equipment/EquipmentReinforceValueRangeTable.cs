using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Equipment
{
  [System.Serializable]
  public class EquipmentReinforceValueRangeTable : AbstractTable {

    public int Level;
    public int Base;
    public float RandMin;
    public float RandMax;

  }
}
