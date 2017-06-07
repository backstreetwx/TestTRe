using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;
using DataManagement.GameData.FormatCollection.Battle;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class ChangeDamageFormat : AbsSkillEffectBase 
  {
    public float NormCp;
    public float NormSp;

    public ChangeDamageFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = ChangeDamageTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);
      this.NormCp = _row.NormCp;
      this.NormSp = _row.NormSp;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      if (selfFightData.OneTurnFightData.IsBlast.Value) 
      {
        selfFightData.OneTurnFightData.BlastPower = base.root.Level * this.NormSp + this.NormCp;
      }
    }

    #endregion
  }
}
