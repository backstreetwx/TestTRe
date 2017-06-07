using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;

namespace DataManagement.TableClass.Skill.Effect
{
  [System.Serializable]
  public class ChangeAffectRangeTable : AbstractTable 
  {
    public SKILL_AFFECT_RANGE AffectRange;
    public SKILL_AFFECT_TYPE AffectType;
    public bool EnableSlotID_0;
    public bool EnableSlotID_1;
    public bool EnableSlotID_2;
  }
}
