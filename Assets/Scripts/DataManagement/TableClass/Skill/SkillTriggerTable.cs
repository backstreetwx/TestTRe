using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;

namespace DataManagement.TableClass.Skill
{
  [System.Serializable]
  public class SkillTriggerTable : AbstractTable 
  {
    public ushort SkillID;
    public SKILL_TRIGGER_TYPE TriggerType;
    public short TriggerTableID;
  }
}
