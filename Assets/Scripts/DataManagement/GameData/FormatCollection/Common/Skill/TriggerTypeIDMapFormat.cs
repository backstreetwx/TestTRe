using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill;

namespace DataManagement.GameData.FormatCollection.Common.Skill
{
  [System.Serializable]
  public class TriggerTypeIDMapFormat
  {
    public SKILL_TRIGGER_TYPE Type;
    public short TriggerID;

    public TriggerTypeIDMapFormat(SkillTriggerTable dbData)
    {
      this.Type = dbData.TriggerType;
      this.TriggerID = dbData.TriggerTableID;
    }

    public TriggerTypeIDMapFormat(EffectTriggerTable dbData)
    {
      this.Type = dbData.TriggerType;
      this.TriggerID = dbData.TriggerTableID;
    }
  }
}
