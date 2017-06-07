using UnityEngine;
using System.Collections;
using GameFlow.Battle.Common.View;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData;
using Common;
using DataManagement.GameData.FormatCollection.Common;
using ConstCollections.PJEnums.Battle;
using System.Collections.Generic;
using System.Linq;
using GameFlow.Battle.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace GameFlow.Battle.Common.Controller
{
  public abstract class AbsCharacterController : MonoBehaviour 
  {
    public CharacterBaseView View;
    public CharacterHPView HPView;
    public FightDataFormat FightData;
    public CHARACTER_TYPE Type;

    public abstract STATES State { get; }

    public abstract short SlotID { get;}

    public abstract void InitTurn (BATTLE_FIGHT_TYPE fightType);
    public abstract void PostFightData(FightDataFormat data);

    protected virtual void Start()
    {
      this.battleInfoManagerScript = FindObjectOfType<BattleInfoManager> ();
    }

    public virtual IEnumerator AttackOtherCoroutine (AbsCharacterController defender, Queue<AbsCharacterController> targetQueue)
    {
      if(this.FightData.OneTurnFightData.AttackPowerType == ATTACK_POWER_TYPE.NORMAL)
        this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.ACTIVE_ATTACK, this.FightData);

      float _time = this.View.SetAnimationTrigger (ANIMATION_TRIGGERS.GOTO_ATTACK);

      yield return new WaitForSeconds (_time);

      SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_HIT_COUNT, this.FightData);

      int _hitCount = this.FightData.OneTurnFightData.HitCount;
      while (_hitCount > 0 && defender.State == STATES.ALIVE) 
      {
        yield return this.HitCoroutine (defender, targetQueue);
        _hitCount--;
      }

      yield break;
    }

    public virtual IEnumerator HitCoroutine(AbsCharacterController defender, Queue<AbsCharacterController> targetQueue)
    {
      if (!HitSuccess (defender)) 
      {
        this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.AVOID_ATTACK, this.FightData, defender.FightData);
        yield break;
      }

      if (BlastSuccess (defender)) 
      {
        CalculateBlastPower (defender);
      }
        
      CalculateAttackerPower (defender);

      int _damage = CalculateDamageToOther (defender);

      yield return defender.GetDamageCoroutine (this, _damage);

      SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_DEBUFF, this.FightData, defender.FightData);
      SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_STATE_EXCEPTION, this.FightData, defender.FightData);
      SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_OTHER_BURN_DOT, this.FightData, defender.FightData);

      yield break;
    }

    public virtual bool HitSuccess (AbsCharacterController other)
    {
      return BattleDataManager.Instance.HitSuccess (this.FightData, other.FightData);
    }

    public virtual bool BlastSuccess (AbsCharacterController other)
    {
      return BattleDataManager.Instance.BlastSuccess (this.FightData, other.FightData);
    }

    public virtual float CalculateBlastPower (AbsCharacterController other)
    {
      return BattleDataManager.Instance.CalculateBlastPower (this.FightData, other.FightData);
    }

    public virtual float CalculateAttackerPower (AbsCharacterController other)
    {
      return BattleDataManager.Instance.CalculateAttackerPower (this.FightData, other.FightData);
    }

    public virtual int CalculateDamageToOther (AbsCharacterController other)
    {
      return BattleDataManager.Instance.CalculateDamageToOther (this.FightData, other.FightData);
    }

    public virtual IEnumerator GetDamageCoroutine (AbsCharacterController attacker, int damage)
    {
      this.FightData.GetDamage(damage, attacker.FightData);

      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.GET_DAMAGE, attacker.FightData, this.FightData);

      PostFightData (this.FightData);

      if (this.FightData.DeadCheck(attacker.FightData)) 
      {
        yield return DeadCoroutine ();
        yield break;
      }

      float _timeDamage = this.View.SetAnimationTrigger (ANIMATION_TRIGGERS.GOTO_GET_DAMAGE);
      yield return new WaitForSeconds (_timeDamage);
    }

    public virtual IEnumerator GetDamageByDotCoroutine ()
    {
      this.FightData.GetDamageByDot();

      PostFightData (this.FightData);

      if (this.FightData.DeadCheck()) 
      {
        yield return DeadCoroutine ();
        yield break;
      }

      float _timeDamage = this.View.SetAnimationTrigger (ANIMATION_TRIGGERS.GOTO_GET_DAMAGE);
      yield return new WaitForSeconds (_timeDamage);
    }

    public virtual IEnumerator DeadCoroutine ()
    {
      this.state = STATES.DEAD;
      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.IS_DEAD, null, this.FightData);
      float _timeDead = this.View.SetAnimationTrigger (ANIMATION_TRIGGERS.GOTO_DEAD);
      yield return new WaitForSeconds (_timeDead);
    }

    public virtual void InitFight(){}

    public virtual void InitBodyView(string path, int idle, int attack, int getDamage, int dead)
    {
      this.View.InitSpriteList (path);
      this.View.InitAnimationSprite (idle, attack, getDamage, dead);
      this.View.SetAnimationTrigger(ANIMATION_TRIGGERS.GOTO_IDLE);
    }
      
//    public virtual void InitHPView(int hp, int hpMax)
//    {
//      this.HPView.SetValue (hp, hpMax);
//    }
      
    protected STATES state;
    protected BattleInfoManager battleInfoManagerScript;
  }
}
