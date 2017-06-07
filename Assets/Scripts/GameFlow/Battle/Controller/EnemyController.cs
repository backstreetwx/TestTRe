using UnityEngine;
using System.Collections;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJEnums.Character;
using System.Collections.Generic;
using Common;
using PJMath;
using DataManagement.GameData;
using ConstCollections.PJEnums.Battle;
using DataManagement.GameData.FormatCollection.Battle;

namespace GameFlow.Battle.Controller
{
  public class EnemyController : AbsCharacterController 
  {
    public static readonly string IMAGE_PATH = "Textures/Test/Monsters/monsters";

    public EnemyDataFormat EnemyDataCache;

    #region implemented abstract members of AbsCharacterController

    public override STATES State
    {
      get
      {
        if (this.EnemyDataCache == null || this.EnemyDataCache.Attributes == null) 
        {
          this.state = STATES.NONE;
        }

        return state;
      }
    }

    public override short SlotID {
      get {
        if (this.EnemyDataCache == null || this.EnemyDataCache.Attributes == null) 
        {
          return -1;
        }

        return (short)this.EnemyDataCache.Attributes.SlotID;
      }
    }

    public override void InitTurn(BATTLE_FIGHT_TYPE fightType)
    {
      base.FightData.InitTurn (this.EnemyDataCache, fightType);
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

    public EnemyAnimationFormat GetEnemyAnimationInfo()
    {
      return this.EnemyDataCache.AnimationInfo;
    }

    #endregion

    void Awake()
    {
      base.Type = CHARACTER_TYPE.ENEMY;
    }

    public void InitFight(EnemyDataFormat enemyData)
    {
      this.EnemyDataCache = enemyData.CloneEx();
      base.FightData.InitFight (this.EnemyDataCache);

      base.InitBodyView (this.EnemyDataCache.AnimationInfo.TexturePath,
        this.EnemyDataCache.AnimationInfo.IdleID, 
        this.EnemyDataCache.AnimationInfo.AttackID,
        this.EnemyDataCache.AnimationInfo.GetDamageID,
        this.EnemyDataCache.AnimationInfo.DeadID);

      this.state = STATES.ALIVE;
    }
  }

  [System.Serializable]
  public class SortEnemyControllerBySlotID : IComparer<EnemyController>
  {
    #region IComparer implementation
    public int Compare (EnemyController x, EnemyController y)
    {
      return x.EnemyDataCache.Attributes.SlotID.CompareTo (y.EnemyDataCache.Attributes.SlotID);
    }
    #endregion
  }
}
