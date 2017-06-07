using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Battle;
using ConstCollections.PJEnums.Skill;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Trigger
{
  [System.Serializable]
  public class BattleFightTypeFormat : AbsSkillTriggerBase
  {
    public BATTLE_FIGHT_TYPE FightType;

    public BattleFightTypeFormat(ICommonSkill skill, TriggerTypeIDMapFormat triggerMap): base(skill, triggerMap)
    {
      switch (triggerMap.Type) 
      {
      case SKILL_TRIGGER_TYPE.IS_ATTACKER:
        this.FightType = BATTLE_FIGHT_TYPE.ATTACKER;
        break;
      case SKILL_TRIGGER_TYPE.IS_DEFENDER:
        this.FightType = BATTLE_FIGHT_TYPE.DEFENDER;
        break;
      default:
        this.FightType = BATTLE_FIGHT_TYPE.NONE;
        break;
      }
    }

    #region implemented abstract members of SkillTriggerBase

    public override bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      if (selfFightData.FightType == this.FightType)
        return true;

      return false;
    }

    public override void Reset ()
    {
      
    }

    #endregion


  }
}
