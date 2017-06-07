using UnityEngine;
using System.Collections;
//using ConstCollections.PJEnums;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums.Character;
using System.Linq;

namespace GameFlow.Battle.Common.View
{
  public class CharacterBaseView : MonoBehaviour 
  {
    public int SpIdleID;
    public int SpAttackID;
    public int SpGetDamageID;
    public int SpDeadID;

//    public Sprite SpIdle;
//    public Sprite SpAttack;
//    public Sprite SpGetDamage;
//    public Sprite SpDead;

    protected virtual void Awake () 
    {
      this.spriteRender = GetComponent<SpriteRenderer> ();
      this.animator = GetComponent<Animator> ();
    }

    public virtual void InitSpriteList(string path)
    {
      this.spriteList = Resources.LoadAll<Sprite> (path);
    }

    public virtual void InitAnimationSprite(int idle, int attack, int getDamage, int dead)
    {
      this.SpIdleID = idle;
      this.SpAttackID = attack;
      this.SpGetDamageID = getDamage;
      this.SpDeadID = dead;
    }

//    public virtual void InitAnimationSprite(Sprite idle, Sprite attack, Sprite getDamage, Sprite dead)
//    {
//      this.SpIdle = idle;
//      this.SpAttack = attack;
//      this.SpGetDamage = getDamage;
//      this.SpDead = dead;
//    }

    public virtual float SetAnimationTrigger(ANIMATION_TRIGGERS trigger, bool noWaitTime = true)
    {

      if(trigger != ANIMATION_TRIGGERS.GOTO_IDLE)
        this.animator.SetTrigger (BattleString.Character.Animation.TriggerDic [trigger]);

      if (noWaitTime)
        return -1.0F;

      string _nextAnimName = BattleString.Character.Animation.NextAnimationDic[trigger];
      var _runtimeAnimator = this.animator.runtimeAnimatorController;
      foreach (var item in _runtimeAnimator.animationClips) 
      {
        if (item.name == _nextAnimName)
          return item.length;
      }

      return -1.0F;
    }

    public virtual void SetSprite(ANIMATION_STATES state)
    {
      switch (state) 
      {
      case ANIMATION_STATES.NONE:
        this.spriteRender.sprite = null;
        break;
      case ANIMATION_STATES.IDLE:
        this.spriteRender.sprite = this.spriteList[this.SpIdleID];
        break;
      case ANIMATION_STATES.ATTACK:
        this.spriteRender.sprite = this.spriteList[this.SpAttackID];
        break;
      case ANIMATION_STATES.GET_DAMAGE:
        this.spriteRender.sprite = this.spriteList[this.SpGetDamageID];
        break;
      case ANIMATION_STATES.DEAD:
        this.spriteRender.sprite = this.spriteList[this.SpDeadID];
        break;
      default:
        break;
      }
    }

    public void SetIdle()
    {
      this.state = ANIMATION_STATES.IDLE;
      SetSprite (ANIMATION_STATES.IDLE);
    }

    public void SetAttack()
    {
      this.state = ANIMATION_STATES.ATTACK;
      SetSprite (ANIMATION_STATES.ATTACK);
    }

    public void SetGetDamage()
    {
      this.state = ANIMATION_STATES.GET_DAMAGE;
      SetSprite (ANIMATION_STATES.GET_DAMAGE);
    }

    public void SetDead()
    {
      this.state = ANIMATION_STATES.DEAD;
      SetSprite (ANIMATION_STATES.DEAD);
    }

    protected SpriteRenderer spriteRender;
    [SerializeField, ReadOnly]
    protected Sprite[] spriteList;
    protected Animator animator;
    [SerializeField, ReadOnly]
    ANIMATION_STATES state;
  }

//  [System.Serializable]
//  public class CharacterSpriteFormat
//  {
//    public Sprite SpIdle;
//    public Sprite SpAttack;
//    public Sprite SpGetDamage;
//    public Sprite SpDead;
//  }
}

