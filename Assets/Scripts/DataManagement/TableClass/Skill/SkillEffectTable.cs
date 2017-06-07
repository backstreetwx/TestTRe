using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;

namespace DataManagement.TableClass.Skill
{
  [System.Serializable]
  public class SkillEffectTable : AbstractTable 
  {
    public short SkillID;
    public short EffectID;
    public SKILL_EFFECT_TYPE EffectType;
    public short EffectTableID;
    public SKILL_TARGET_TYPE TargetType;
  }
}
