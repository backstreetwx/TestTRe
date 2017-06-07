using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class ChangeHitCountFormat : AbsSkillEffectBase 
  {
    public int HitCount;
    public ChangeHitCountFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = ChangeHitCountTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);
      this.HitCount = _row.HitCount;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      selfFightData.OneTurnFightData.HitCount = this.HitCount;
    }

    #endregion
  }
}
