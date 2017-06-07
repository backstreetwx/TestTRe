using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using ConstCollections.PJEnums.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class SkillMustFaildFormat : AbsSkillEffectBase 
  {
    public SkillMustFaildFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData){}

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      if (this.TargetType == SKILL_TARGET_TYPE.SELF_GROUP) 
      {
        selfFightData.OneTurnFightData.AllSkillFailed = true;

        base.root.BattleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.SKILL_MUST_FAILED, selfFightData, otherFightData);
      }
    }

    #endregion
  }
}
