using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Equipment;

namespace DataManagement.TableClass.Equipment
{
  [System.Serializable]
  public class EquipmentTable : AbstractTable 
  {
    public EQUIPMENT_TYPE EquipmentType;
    public short QualityGrade;
    public string TexturePath;
    public int TextureIconID;
    public int DimensionChipOutput;
    public int DimensionChipOutputProbability;
    public int Weights;
  }
}
