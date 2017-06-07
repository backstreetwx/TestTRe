using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill.Trigger;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Trigger
{
  [System.Serializable]
  public class DebuffFormat : AbsSkillTriggerBase
  {
    public float Max;

    public DebuffFormat(ICommonSkill skill, TriggerTypeIDMapFormat triggerMap): base(skill, triggerMap)
    {
      if (triggerMap.TriggerID < 0)
        return;

      var _dbData = DebuffTableReader.Instance.FindDefaultFirst((ushort)triggerMap.TriggerID);
      this.Max = _dbData.Max;
    }

    #region implemented abstract members of SkillTriggerBase

    public override bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      var _probability = this.Max - otherFightData.FinalAttributesCache.RES;

      return PJMath.ProbabilityHelper.TrySuccess(Mathf.FloorToInt(_probability));
    }

    public override void Reset ()
    {
      
    }

    #endregion

  }
}
