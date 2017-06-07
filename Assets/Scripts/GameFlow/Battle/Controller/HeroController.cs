using UnityEngine;
using System.Collections;
using GameFlow.Battle.Common.Controller;
using GameFlow.Battle.Common.View;
//using ConstCollections.PJEnums;
using DataManagement.GameData.FormatCollection;
using Common;
using ConstCollections.PJEnums.Character;
using System.Collections.Generic;
using PJMath;
using ConstCollections.PJConstOthers;
using DataManagement.SaveData;
using DataManagement.GameData;
using ConstCollections.PJEnums.Battle;
using DataManagement.GameData.FormatCollection.Battle;

namespace GameFlow.Battle.Controller
{
  public class HeroController : AbsCharacterController
  {
    public HeroDataFormat HeroDataCache;

    #region implemented abstract members of CharacterBaseController

    public override STATES State
    {
      get
      {
        if (this.HeroDataCache == null || this.HeroDataCache.Attributes == null) 
        {
          this.state = STATES.NONE;
        }
        
        return state;
      }
    }

    public override short SlotID {
      get {
        if (this.HeroDataCache == null || this.HeroDataCache.Attributes == null) 
        {
          return -1;
        }

        return (short)this.HeroDataCache.Attributes.SlotID;
      }
    }

    public override void InitTurn(BATTLE_FIGHT_TYPE fightType)
    {
      base.FightData.InitTurn (this.HeroDataCache, fightType);
      SkillDataManager.Instance.ActiveSkill (TURN_STATES.INIT, base.FightData);

      base.HPView.SetValue (base.FightData.FinalAttributesCache.HP, base.FightData.FinalAttributesCache.HPMax);

      if (base.FightData.FinalAttributesCache.HP <= 0 && this.state != STATES.DEAD) 
      {
        base.View.SetAnimationTrigger (ANIMATION_TRIGGERS.GOTO_DEAD);
        this.state = STATES.DEAD;
      }
    }

    public override void PostFightData(FightDataFormat fightData)
    {
      base.HPView.SetValue (fightData.FinalAttributesCache.HP, fightData.FinalAttributesCache.HPMax);
    }

    public override IEnumerator AttackOtherCoroutine (AbsCharacterController defender, Queue<AbsCharacterController> targetQueue)
    {
      yield return base.AttackOtherCoroutine (defender, targetQueue);

      if (defender.State == STATES.DEAD) 
      {
        EnemyController _enemyController = defender as EnemyController;
        var _achievement = FindObjectOfType<BattleManager> ().Instance.Achievement;

        _achievement.EXP += _enemyController.EnemyDataCache.Attributes.EXPOutput;

        _achievement.Aura += _enemyController.EnemyDataCache.Attributes.AuraOutput;

        bool _gotDimenshionChip = PJMath.ProbabilityHelper.TrySuccess (_enemyController.EnemyDataCache.Attributes.DimensionChipOutputProbability);
        Debug.Log ("_gotDimenshionChip = " + _gotDimenshionChip);
        if (_gotDimenshionChip)
          _achievement.DimensionChip += _enemyController.EnemyDataCache.Attributes.DimensionChipOutput;
      }
    }

    #endregion

    void Awake()
    {
      base.Type = CHARACTER_TYPE.HERO;
      base.FightData = new FightDataFormat ();
    }

    void OnEnable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

    public void InitFight(HeroDataFormat heroData)
    {
      this.HeroDataCache = heroData.CloneEx();
      base.FightData.InitFight (this.HeroDataCache);

      base.InitBodyView (this.HeroDataCache.AnimationInfo.TexturePath,
        this.HeroDataCache.AnimationInfo.IdleID, 
        this.HeroDataCache.AnimationInfo.AttackID,
        this.HeroDataCache.AnimationInfo.GetDamageID,
        this.HeroDataCache.AnimationInfo.DeadID);
      
      this.state = STATES.ALIVE;
    }

    public void ExpUp(int exp, System.Action<HeroAttributeFormat> onGotEXP = null, System.Action<HeroAttributeFormat> onLevelUp = null, System.Action<HeroAttributeFormat> onAllLevelUp = null)
    {
      if(this.HeroDataCache.Attributes.Active)
        this.HeroDataCache.EXPUp (exp, true, onGotEXP, onLevelUp, onAllLevelUp);
    }

    void OnHeroCacheChanged(int slotID, HeroDataFormat heroCache)
    {
      if (this.HeroDataCache == null)
        return;
      if (this.HeroDataCache.Attributes.SlotID != slotID)
        return;

      this.HeroDataCache = heroCache.CloneEx ();
    }
  }

  [System.Serializable]
  public class SortHeroControllerBySlotID : IComparer<HeroController>
  {
    #region IComparer implementation
    public int Compare (HeroController x, HeroController y)
    {
      return x.HeroDataCache.Attributes.SlotID.CompareTo (y.HeroDataCache.Attributes.SlotID);
    }
    #endregion
  }
}
