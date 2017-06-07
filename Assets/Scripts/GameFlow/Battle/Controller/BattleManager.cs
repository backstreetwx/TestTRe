using UnityEngine;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.TableClass;
using DataManagement.TableClass.Enemy;
using System.Linq;
using DataManagement.GameData.FormatCollection;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData;
using ConstCollections.PJEnums.Battle;
using Common;
using DataManagement.GameData.FormatCollection.Battle;

namespace GameFlow.Battle.Controller
{
  public class BattleManager : SingletonObject<BattleManager> 
  {
    public HeroManager HeroManagerScript;
    public EnemyManager EnemyManagerScript;

    [Range(0.0F, 10.0F)]
    public float TurnIntervalTime = 1.0F;
    [Range(0.0F, 10.0F)]
    public float BattleIntervalTime = 1.0F;
    public TURN_STATES TurnState;

    public BattleAchievementDataFormat Achievement{
      get
      { 
        if(this.achievementCache == null)
          this.achievementCache = new BattleAchievementDataFormat();
        return this.achievementCache;
      }

      set
      { 
        this.achievementCache = value.CloneEx();
      }
    }

    // Use this for initialization
    void Start () 
    {
      this.initHeroOnly = false;

      this.battleInfoManagerScript = FindObjectOfType<BattleInfoManager> ();

      this.monsterBattleCoroutine = null;
      this.bossBattleCoroutine = null;

      this.HeroManagerScript.TurnIntervalTime = this.TurnIntervalTime;
      this.EnemyManagerScript.TurnIntervalTime = this.TurnIntervalTime;

      var _previousBattleType = BattleDataManager.Instance.BattleType;

      // When player return to battle start monster battle
      switch (_previousBattleType) 
      {
      case BATTLE_TYPE.MONSTER_BATTLE:
      case BATTLE_TYPE.BOSS_BATTLE:
      case BATTLE_TYPE.EX_BOSS_BATTLE:
        StartMonsterBattle ();
        break;
      default:
        break;
      }

    }
      
    public void StartMonsterBattle()
    {
      StopAllCoroutines ();
      BattleDataManager.Instance.BattleType = BATTLE_TYPE.MONSTER_BATTLE;

      this.initHeroOnly = false;
      this.monsterBattleCoroutine = MonsterBattleCoroutine ();
      StartCoroutine (this.monsterBattleCoroutine);
    }

    public void StartBossBattle()
    {
      StopAllCoroutines ();
      BattleDataManager.Instance.BattleType = BATTLE_TYPE.BOSS_BATTLE;

      this.initHeroOnly = false;
      this.bossBattleCoroutine = BossBattleCoroutine ();
      StartCoroutine (this.bossBattleCoroutine);
    }

    void SwapCharacterManager(ref AbsCharacterManager a, ref AbsCharacterManager b)
    {
      AbsCharacterManager _tmp = a;
      a = b;
      b = _tmp;
    }

    IEnumerator MonsterBattleCoroutine()
    {
      while (true) 
      {
        this.HeroManagerScript.InitFight ();

        if(!this.initHeroOnly)
          this.EnemyManagerScript.InitFight ();

        yield return FightCoroutine ();

        if (this.EnemyManagerScript.AllDead) 
        {
          Debug.Log ("this.EnemyManagerScript.AllDead");
          MonsterBattleWin();
        }

        if (this.HeroManagerScript.AllDead) 
        {
          Debug.Log ("this.HeroManagerScript.AllDead");
          MonsterBattleLose();
        }

        if (BattleDataManager.Instance.BattleType != BATTLE_TYPE.MONSTER_BATTLE)
          break;

        yield return null;
        yield return new WaitForSeconds (this.BattleIntervalTime);
      }

      yield break;
    }


    void PostAchievement()
    {
      Debug.Log("----------------Post achivement------------");

      this.HeroManagerScript.ControllerList.ForEach (col => {
        (col as HeroController).ExpUp (this.Achievement.EXP, this.OnGotEXP, null, this.OnAllLevelUp);
      });

      UserSaveDataManager.Instance.Aura += this.Achievement.Aura;
      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.GOT_AURA, null, null, this.Achievement.Aura);

      UserSaveDataManager.Instance.DimensionChip += this.Achievement.DimensionChip;
      if(this.Achievement.DimensionChip > 0)
        this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.GOT_DIMENSION_CHIP, null, null, this.Achievement.DimensionChip);

      this.battleInfoManagerScript.Show();
      Debug.Log("----------------End------------");
    }

    void OnGotEXP(HeroAttributeFormat heroAttributes)
    {
      FightDataFormat _data = new FightDataFormat (heroAttributes);

      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.GOT_EXP, _data, null, this.Achievement.EXP);
    }

    void OnAllLevelUp(HeroAttributeFormat heroAttributes)
    {
      FightDataFormat _data = new FightDataFormat (heroAttributes);

      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.LEVEL_UP, _data, null, heroAttributes.Level);
    }

    void ClearAchievementCache()
    {
      this.Achievement.EXP = 0;
      this.Achievement.Aura = 0;
      this.Achievement.DimensionChip = 0;
    }
 
    void MonsterBattleWin()
    {
      this.PostAchievement ();
      this.ClearAchievementCache ();
      BattleSaveDataManager.Instance.AddSearchPointWhenBattleWin ();
      this.initHeroOnly = false;
    }

    void MonsterBattleLose()
    {
      this.PostAchievement ();
      this.ClearAchievementCache ();
      BattleSaveDataManager.Instance.MinusSearchPointWhenBattleLose ();
      this.initHeroOnly = true;
    }

    public IEnumerator BossBattleCoroutine()
    {
      this.HeroManagerScript.InitFight ();
      this.EnemyManagerScript.InitFight ();

      yield return FightCoroutine ();

      if (this.EnemyManagerScript.AllDead) 
      {
        Debug.Log ("this.EnemyManagerScript.AllDead");
        BossBattleWin();
      }

      if (this.HeroManagerScript.AllDead) 
      {
        Debug.Log ("this.HeroManagerScript.AllDead");
        BossBattleLose();
      }

      yield return null;
      yield return new WaitForSeconds (this.BattleIntervalTime);
    }

    void BossBattleWin()
    {
      this.PostAchievement ();
      this.ClearAchievementCache ();

      BattleSaveDataManager.Instance.ClearEnemyData ();
      BattleSaveDataManager.Instance.MoveToNextLevelOrArea ();

      StartMonsterBattle ();
    }

    void BossBattleLose()
    {
      this.PostAchievement ();
      this.ClearAchievementCache ();
      BattleSaveDataManager.Instance.MinusSearchPointWhenBattleLose ();

      StartMonsterBattle ();
    }

    IEnumerator FightCoroutine()
    {
      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.BATTLE_ENGAGE);

      this.battleInfoManagerScript.Show ();

      bool _heroFirst = System.Convert.ToBoolean(Random.Range(0, 2));

      AbsCharacterManager _firstManager = null;
      AbsCharacterManager _secondManager = null;
      if (_heroFirst) 
      {
        _firstManager = this.HeroManagerScript;
        _secondManager = this.EnemyManagerScript;
      } 
      else 
      {
        _firstManager = this.EnemyManagerScript;
        _secondManager = this.HeroManagerScript;
      }
        
      while (true) 
      {

        _firstManager.InitTurn (BATTLE_FIGHT_TYPE.ATTACKER);
        _secondManager.InitTurn (BATTLE_FIGHT_TYPE.DEFENDER);

        yield return  _firstManager.OneTurnCoroutine (_secondManager);

        if (_firstManager.AllDead) 
        {
          break;
        }

        if (_secondManager.AllDead) 
        {
          break;
        }

        SwapCharacterManager (ref _firstManager, ref _secondManager);
      }

      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.BATTLE_DONE);

      this.battleInfoManagerScript.Show ();

      yield break;
    }

    BattleInfoManager battleInfoManagerScript;

    IEnumerator monsterBattleCoroutine;
    IEnumerator bossBattleCoroutine;

    BattleAchievementDataFormat achievementCache;
    bool initHeroOnly;

  }
}
