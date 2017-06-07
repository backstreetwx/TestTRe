using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Skill.Trigger;
using System.Collections.Generic;
using ConstCollections.PJEnums.Skill;
using ConstCollections.PJEnums.Character;
using ConstCollections.PJEnums.Battle;
using DataManagement.TableClass.Skill.Effect;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class StandardAttackPowerFormat : AbsSkillEffectBase
  {
    [ReadOnly]
    public float ATKCp;
    [ReadOnly]
    public float ATKSp;
    [ReadOnly]
    public float MAGCp;
    [ReadOnly]
    public float MAGSp;
    [ReadOnly]
    public float VITCp;
    [ReadOnly]
    public float VITSp;
    [ReadOnly]
    public float NormCp;
    [ReadOnly]
    public float NormSp;

    public StandardAttackPowerFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {

      // Init skill effects
      var _attackDamageData = StandardAttackPowerTableReader.Instance.FindDefaultFirst((ushort)dbData.EffectTableID);

      this.ATKCp = _attackDamageData.ATKCp;
      this.ATKSp = _attackDamageData.ATKSp;
      this.MAGCp = _attackDamageData.MAGCp;
      this.MAGSp = _attackDamageData.MAGSp;
      this.VITCp = _attackDamageData.VITCp;
      this.VITSp = _attackDamageData.VITSp;
      this.NormCp = _attackDamageData.NormCp;
      this.NormSp = _attackDamageData.NormSp;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      if (TargetType == SKILL_TARGET_TYPE.OTHER_GROUP) 
      {
        float _damageATK = selfFightData.FinalAttributesCache.ATK * (this.ATKCp + this.root.Level * this.ATKSp);
        float _damageMAG = selfFightData.FinalAttributesCache.MAG * (this.MAGCp + this.root.Level * this.MAGSp);
        float _VIT = 0;

        if (selfFightData.Type == CHARACTER_TYPE.HERO) 
        {
          var _heroAttributes = selfFightData.AttributeOriginCache as HeroAttributeFormat;
          var _attributesWithEquipment = HeroDataManager.Instance.CalculateAttributesWithEquipments (_heroAttributes, selfFightData.EquipmentList);

          _VIT = _attributesWithEquipment.VIT;
        }
        
        float _damageVIT = _VIT * (this.VITCp + this.root.Level * this.VITSp);
        float _damageNorm = this.root.Level * this.NormSp + this.NormCp;

        selfFightData.OneTurnFightData.AttackerPower = Mathf.FloorToInt(_damageATK + _damageMAG + _damageVIT + _damageNorm);
        Debug.LogWarningFormat ("{0} EffectAttackDamage is actived",selfFightData.Type);
      }
    }

    #endregion
  }
}
