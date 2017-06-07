using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Battle;
using ConstCollections.PJEnums.Skill;
using System.Collections.Generic;
using ConstCollections.PJConstOthers;
using DataManagement.GameData.FormatCollection.Common;
using PJMath;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Character;
using Common;
using System.Linq;
using ConstColections.PJEnums.Hero;
using ConstCollections.PJConstOthers.Battle;

namespace DataManagement.GameData.FormatCollection.Battle
{
  [System.Serializable]
  public class OneTurnFightDataFormat
  {
    public bool? IsHitSuccess;
    public int HitCount;

    public bool? IsBlast;
    public float BlastPower;

    public ATTACK_POWER_TYPE AttackPowerType;
    public float AttackerPower;

    public int DamageToDefender;
    public int GetDamageFinal;

    public bool? AllSkillFailed;

    public bool? IsDead;

    public SKILL_AFFECT_RANGE SkillAffectRange;
    public SKILL_AFFECT_TYPE SkillAffectType;
    public List<short> SkillCustomAffectSlotIDList;

    public CommonAttributeFormat AttributesBuff;

    public List<ATTRIBUTE_TYPE> IgnoreDefenderAttributeList;

    public OneTurnFightDataFormat()
    {
      if(this.AttributesBuff == null)
        this.AttributesBuff = new CommonAttributeFormat(true);

      if (this.IgnoreDefenderAttributeList == null)
        this.IgnoreDefenderAttributeList = new List<ATTRIBUTE_TYPE> ();
      
      this.Clear ();
    }

    public void Clear()
    {
      this.IsBlast = null;
      this.IsHitSuccess = null;
      this.AllSkillFailed = null;
      this.IsDead = null;
      this.AttackerPower = 0;
      this.BlastPower = FIGHT.BLAST_POWER_NONE;
      this.AttackPowerType = ATTACK_POWER_TYPE.NORMAL;
      this.DamageToDefender = 0;
      this.GetDamageFinal = 0;
      this.HitCount = FIGHT.HIT_COUNT_DEFAULT;
      this.SkillAffectRange = SKILL_AFFECT_RANGE.SINGLE_CURRENT;
      this.SkillCustomAffectSlotIDList = null;
      this.AttributesBuff.ClearToZero ();
      this.IgnoreDefenderAttributeList.Clear ();
    }

    public bool HitSuccess(FightDataFormat attacker, FightDataFormat defender)
    {
      this.IsHitSuccess = null;

      SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_HIT, attacker, defender);

      if (this.IsHitSuccess != null) 
      {
        return this.IsHitSuccess.Value;
      }

      float _t = Mathf.Clamp(defender.FinalAttributesCache.AVD - attacker.FinalAttributesCache.HIT, FIGHT.HIT_SUCCESS_PROBABILITY_MIN, FIGHT.HIT_SUCCESS_PROBABILITY_MAX);

      int _resultValue = FIGHT.HIT_SUCCESS_CP_0 - Mathf.FloorToInt(_t);
      this.IsHitSuccess = ProbabilityHelper.TrySuccess (_resultValue);

      return this.IsHitSuccess.Value;
    }

    public bool BlastSuccess (FightDataFormat attacker, FightDataFormat defender)
    {
      this.IsBlast = null;

      SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_BLAST, attacker, defender);

      if (this.IsBlast != null) 
      {
        return this.IsBlast.Value;
      }

      var _defenderAC = defender.FinalAttributesCache.AC;
      this.IgnoreDefenderAttributeList.ForEach (type => {
        if(type == ATTRIBUTE_TYPE.AC)
        {
          _defenderAC = 0.0F;
        }
      });

      float _t = Mathf.Clamp(_defenderAC - attacker.FinalAttributesCache.PEN, FIGHT.BLAST_SUCCESS_AC_MINUS_PEN_MIN, FIGHT.BLAST_SUCCESS_AC_MINUS_PEN_MAX);

      var _resultValue = attacker.FinalAttributesCache.CRI * FIGHT.BLAST_SUCCESS_CRI_CP_0 * Mathf.FloorToInt(FIGHT.BLAST_SUCCESS_CP_0 -  _t);

      this.IsBlast = ProbabilityHelper.TrySuccess (Mathf.FloorToInt(_resultValue));

      return this.IsBlast.Value;
    }

    public float CalculateBlastPower(FightDataFormat attacker, FightDataFormat defender)
    {
      if (this.IsBlast.Value) 
      {
        this.BlastPower = FIGHT.BLAST_POWER_NORMAL;
        SkillDataManager.Instance.ActiveSkill (TURN_STATES.CALCULATE_BLAST_POWER, attacker, defender);
      }
      else
        this.BlastPower = FIGHT.BLAST_POWER_NONE;

      return this.BlastPower;
    }

    public float CalculateAttackPower (FightDataFormat attacker, FightDataFormat defender)
    {
      if (this.AttackPowerType == ATTACK_POWER_TYPE.INITIATIVE_SKILL) 
      {
        SkillDataManager.Instance.ActiveSkill(TURN_STATES.CALCULATE_ATTACK_POWER, attacker);
        return this.AttackerPower;
      }

      this.AttackerPower = attacker.FinalAttributesCache.ATK;
      return this.AttackerPower;
    }

    public int CalculateDamageToOther (FightDataFormat attacker, FightDataFormat defender)
    {
      var _defenderAC = defender.FinalAttributesCache.AC;
      this.IgnoreDefenderAttributeList.ForEach (type => {
        if(type == ATTRIBUTE_TYPE.AC)
        {
          _defenderAC = 0.0F;
        }
      });

      float _t = Mathf.Clamp(_defenderAC - attacker.FinalAttributesCache.PEN, FIGHT.CALCULATE_DAMAGE_TO_OTHER_AC_MINUS_PEN_MIN, FIGHT.CALCULATE_DAMAGE_TO_OTHER_AC_MINUS_PEN_MAX);
      _t = FIGHT.CALCULATE_DAMAGE_TO_OTHER_CP_0 - (_t / FIGHT.CALCULATE_DAMAGE_TO_OTHER_CP_1);

      float _resultValue = 0;

      var _defenderDEF = defender.FinalAttributesCache.DEF;
      this.IgnoreDefenderAttributeList.ForEach (type => {
        if(type == ATTRIBUTE_TYPE.DEF)
        {
          _defenderDEF = 0.0F;
        }
      });

      _resultValue = (this.AttackerPower * this.BlastPower - _defenderDEF) * _t;

      _resultValue *= Random.Range (FIGHT.CALCULATE_DAMAGE_TO_OTHER_MUL_RANDOM_MIN, FIGHT.CALCULATE_DAMAGE_TO_OTHER_MUL_RANDOM_MAX);

      _resultValue = Mathf.Clamp (_resultValue, this.AttackerPower * FIGHT.CALCULATE_DAMAGE_TO_OTHER_ATK_MIN_COE, float.PositiveInfinity);

      this.DamageToDefender = Mathf.FloorToInt (_resultValue);
      return this.DamageToDefender;
    }

    public void CalculateDamageFinal(int damage)
    {
      this.GetDamageFinal = damage;
    }
  }


}
