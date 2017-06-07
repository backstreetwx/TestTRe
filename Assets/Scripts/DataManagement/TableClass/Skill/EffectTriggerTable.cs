using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;

namespace DataManagement.TableClass.Skill
{
  [System.Serializable]
  public class EffectTriggerTable : AbstractTable 
  {
    public short SkillID;
    public short EffectID;
    public SKILL_TRIGGER_TYPE TriggerType;
    public short TriggerTableID;
  }
}
