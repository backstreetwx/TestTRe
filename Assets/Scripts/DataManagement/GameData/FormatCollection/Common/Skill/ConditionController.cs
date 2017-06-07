using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Battle;
using System.Collections.Generic;
using ConstCollections.PJEnums.Skill;
using System.Linq;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.GameData.FormatCollection.Skill.Trigger;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Common.Skill
{
  [System.Serializable]
  public class ConditionController
  {
    [ReadOnly]
    public List<AbsSkillTriggerBase> TriggerList;

    public bool IsActive{
      get{ return this.active;}
    }

    public ConditionController(ICommonSkill skill, List<TriggerTypeIDMapFormat> TriggerMapList)
    {
      this.root = skill;

      List<AbsSkillTriggerBase> _list = new List<AbsSkillTriggerBase> ();
      TriggerMapList.ForEach (triggerMap => {
        switch (triggerMap.Type) 
        {
        case SKILL_TRIGGER_TYPE.STATES:
          {
            var _trigger = new StatesFormat((this.root as CommonSkillFormat), triggerMap);
            _list.Add(_trigger);
            break;
          }
        case SKILL_TRIGGER_TYPE.PROBABILITY:
          {
            var _trigger = new ProbabilityFormat((this.root as CommonSkillFormat), triggerMap);
            _list.Add(_trigger);
            break;
          }
        case SKILL_TRIGGER_TYPE.SKILL_MUST_FAILED:
          {
            var _trigger = new SkillMustFaildFormat((this.root as CommonSkillFormat), triggerMap);
            _list.Add(_trigger);
            break;
          }
        case SKILL_TRIGGER_TYPE.DEBUFF:
          {
            var _trigger = new DebuffFormat((this.root as CommonSkillFormat), triggerMap);
            _list.Add(_trigger);
            break;
          }
        case SKILL_TRIGGER_TYPE.IS_ATTACKER:
          {
            var _trigger = new BattleFightTypeFormat((this.root as CommonSkillFormat), triggerMap);
            _list.Add(_trigger);
            break;
          }
        case SKILL_TRIGGER_TYPE.IS_DEFENDER:
          {
            var _trigger = new BattleFightTypeFormat((this.root as CommonSkillFormat), triggerMap);
            _list.Add(_trigger);
            break;
          }
        default:
          break;
        }
      });

      if(_list.Count > 0)
        this.TriggerList = _list;
    }

    public bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      if (selfFightData.OneTurnFightData.AllSkillFailed != null && selfFightData.OneTurnFightData.AllSkillFailed.Value == true) 
      {
        this.active = false;
        return this.active;
      }

      this.active = this.TriggerList.All (trigger => {
        return trigger.CheckSuccess(selfFightData, otherFightData);
      });

      return this.active;
    }

    public bool CheckState(TURN_STATES state)
    {
      var _targetTrigger = this.TriggerList.Find(trigger => {
        if(trigger.Type != SKILL_TRIGGER_TYPE.STATES)
          return false;

        (trigger as StatesFormat).CurrentTurnState = state;
        return (trigger as StatesFormat).CheckSuccess(null, null);
      });

      return _targetTrigger != null;
    }

    public void Reset()
    {
      this.active = false;
      this.TriggerList.ForEach (trigger => {
        trigger.Reset();
      });
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson(this, true);
    }

    [SerializeField, ReadOnly]
    bool active;

    [SerializeField]
    ICommonSkill root;
  }
}
