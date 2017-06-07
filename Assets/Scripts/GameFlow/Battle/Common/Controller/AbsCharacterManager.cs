using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData;
using ConstCollections.PJEnums.Battle;
using GameFlow.Battle.Controller;
using DataManagement.GameData.FormatCollection;
using System.Linq;
using DataManagement.GameData.FormatCollection.Battle;

namespace GameFlow.Battle.Common.Controller
{
  public abstract class AbsCharacterManager : MonoBehaviour 
  {
    [Range(0.0F, 10.0F)]
    public float TurnIntervalTime = 0.0F;

    public GameObject CharacterPrefab;
    public Transform[] SlotTransformList;
    public List<AbsCharacterController> ControllerList;
    public CHARACTER_TYPE CharacterType ;

    public abstract bool AllDead{ get;}
    public abstract List<AbsCharacterController> AliveList{ get; }

    public virtual List<FightDataFormat> AliveFightDataList
    { 
      get
      { 
        return this.AliveList.Select (item => item.FightData).ToList ();
      }
    }

    public virtual BATTLE_FIGHT_TYPE FightType
    {
      get
      {
        return this.fightType;
      }

      set
      {
        this.fightType = value;
        this.ControllerList.ForEach (item => {
          item.FightData.FightType = this.fightType;
        });
      }
    }

    protected virtual void Awake()
    {
      this.fightType = BATTLE_FIGHT_TYPE.NONE;
      this.CharacterType = CHARACTER_TYPE.NONE;
    }

    protected virtual void Start()
    {
      this.battleInfoManagerScript = FindObjectOfType<BattleInfoManager> ();
    }

    public virtual void InitFight()
    {
      this.ControllerList.ForEach (col => {
        col.gameObject.SetActive(false);
      });
    }

    public virtual void InitTurn(BATTLE_FIGHT_TYPE fightType)
    {
      this.ControllerList.ForEach (col => col.InitTurn (fightType));
    }

    public virtual IEnumerator OneTurnCoroutine (AbsCharacterManager otherManager)
    {
      if (this.AllDead || otherManager.AllDead)
        yield break;

      List<AbsCharacterController> _selfAliveList = this.AliveList;
      List<AbsCharacterController> _otherAliveList = otherManager.AliveList;

      Queue<AbsCharacterController> _targetQueue = new Queue<AbsCharacterController> ();

      foreach (var _self in _selfAliveList) 
      {
        yield return _self.GetDamageByDotCoroutine ();
        if (_self.State == STATES.DEAD) 
        {
          this.battleInfoManagerScript.Show();
          yield return new WaitForSeconds (this.TurnIntervalTime);
          continue;
        }
          

        SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_SPECIAL_SKILL, _self.FightData);

        SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_INITIATIVE_SKILL, _self.FightData);

        SkillDataManager.Instance.ActiveSkill (TURN_STATES.DECIDE_ATTACK_TARGET, _self.FightData, null, _targetQueue);

        // Check Sneer
        _otherAliveList.ForEach (_other => {
          SkillDataManager.Instance.ActiveSkill (TURN_STATES.DECIDE_ATTACK_TARGET, _other.FightData, null, _targetQueue);
        });

        SkillDataManager.Instance.ActiveSkill (TURN_STATES.JUDGE_ASSIST_ATTACK, _self.FightData);

        if (_targetQueue.Count == 0) 
        {
          // Normal single target
          int _targetIndex = Random.Range(0, _otherAliveList.Count);

          _targetQueue.Enqueue (_otherAliveList [_targetIndex]);
        }

        while (_targetQueue.Count > 0) 
        {
          var _target = _targetQueue.Dequeue ();

          if(_target.State != STATES.DEAD)
            yield return _self.AttackOtherCoroutine (_target, _targetQueue);
        }

        this.battleInfoManagerScript.Show();

        yield return new WaitForSeconds (this.TurnIntervalTime);

        _otherAliveList = otherManager.AliveList;
        if (_otherAliveList == null)
          break;
      }

      yield return new WaitForEndOfFrame();
    }

    protected BattleInfoManager battleInfoManagerScript;
    [ReadOnly, SerializeField]
    protected BATTLE_FIGHT_TYPE fightType;
  }
}
