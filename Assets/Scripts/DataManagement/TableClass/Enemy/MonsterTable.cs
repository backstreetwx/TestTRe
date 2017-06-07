using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Character;

namespace DataManagement.TableClass.Enemy
{
  [System.Serializable]
  public class MonsterTable : AbstractTable 
  {
//    public ushort ID;
    public ushort MonsterNameID;

    // Attributes
    public int Level;
    public int HPMax;
    public float RES;
    public float ATK;
    public float MAG;
    public float DEF;
    public float AC;
    public float CRI;
    public float PEN;
    public float HIT;
    public float AVD;

    // For output
    public int EXPOutput;
    public int AuraOutput;
    public int DimensionChipOutput;
    public int DimensionChipOutputProbability;

    // For animation
    public string TexturePath;
    public int TextureIconID;
    public int TextureIdleID;
    public int TextureAttackID;
    public int TextureGetDamageID;
    public int TextureDeadID;
  }
}
