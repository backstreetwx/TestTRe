﻿using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using ConstColections.PJEnums.Hero;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class IgnoreDefenderDEFFormat : AbsSkillEffectBase 
  {
    public IgnoreDefenderDEFFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData){}

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      var _has = selfFightData.OneTurnFightData.IgnoreDefenderAttributeList.Contains (ATTRIBUTE_TYPE.DEF);

      if (!_has)
        selfFightData.OneTurnFightData.IgnoreDefenderAttributeList.Add (ATTRIBUTE_TYPE.DEF);
    }

    #endregion
  }
}
