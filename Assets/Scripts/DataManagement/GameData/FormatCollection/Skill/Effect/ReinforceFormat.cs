using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class ReinforceFormat : AbsSkillEffectBase 
  {
    public float CRISp;
    public float ACSp;
    public float RESSp;
    public float HPMaxSp;

    public ReinforceFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = ReinforceTableReader.Instance.FindDefaultUnique ((ushort)dbData.EffectTableID);
      this.CRISp = _row.CRISp;
      this.ACSp = _row.ACSp;
      this.RESSp = _row.RESSp;
      this.HPMaxSp = _row.HPMaxSp;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      selfFightData.OneTurnFightData.AttributesBuff.CRI += base.root.Level * this.CRISp;
      selfFightData.OneTurnFightData.AttributesBuff.AC += base.root.Level * this.ACSp;
      selfFightData.OneTurnFightData.AttributesBuff.RES += base.root.Level * this.RESSp;
      var _hp = Mathf.FloorToInt(base.root.Level * this.HPMaxSp);
      selfFightData.OneTurnFightData.AttributesBuff.HPMax += _hp;
      selfFightData.OneTurnFightData.AttributesBuff.HP += _hp;

      selfFightData.CalculateFinalAttributes ();
    }

    #endregion
  }
}
