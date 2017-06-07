using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Character;

namespace DataManagement.TableClass.Skill.Effect
{
  [System.Serializable]
  public class DotTable : AbstractTable 
  {
    public DOT_TYPE DotType;
    public float Damp;
    public float INTCp;
    public float INTSp;
    public float ATKCp;
    public float ATKSp;
    public float PoisonCp;
  }
}
