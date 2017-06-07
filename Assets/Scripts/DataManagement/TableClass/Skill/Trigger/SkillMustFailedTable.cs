using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums;

namespace DataManagement.TableClass.Skill.Trigger
{
  [System.Serializable]
  public class SkillMustFailedTable : AbstractTable 
  {
    public float Delta_ATK_MAG;
    public COMPARE_TYPE CompareType;
    public int Probability;
  }
}
