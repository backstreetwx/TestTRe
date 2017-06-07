using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;
using ConstCollections.PJEnums.Skill;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class LastStandAttackPowerFormat : AbsSkillEffectBase
  {
    [ReadOnly]
    public float NormCp;
    [ReadOnly]
    public float NormSp;

    public LastStandAttackPowerFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {

      // Init skill effects
      var _attackDamageData = LastStandAttackPowerTableReader.Instance.FindDefaultFirst((ushort)dbData.EffectTableID);

      this.NormCp = _attackDamageData.NormCp;
      this.NormSp = _attackDamageData.NormSp;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      if (TargetType == SKILL_TARGET_TYPE.OTHER_GROUP) 
      {
        float _damageATK = selfFightData.FinalAttributesCache.ATK;
        float _damageDeltaHP = selfFightData.FinalAttributesCache.HPMax - selfFightData.FinalAttributesCache.HP;

        float _damageNorm = this.root.Level * this.NormSp + this.NormCp;

        selfFightData.OneTurnFightData.AttackerPower = Mathf.FloorToInt(_damageATK + _damageDeltaHP * _damageNorm);

        Debug.LogWarningFormat ("{0} LastStandAttackPower is actived",selfFightData.Type);
      }
    }

    #endregion
  }
}
