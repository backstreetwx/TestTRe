using UnityEngine;
using System.Collections;
using Common;
using DataManagement.GameData.FormatCollection.Common;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection;
using System.Linq;
using ConstCollections.PJEnums.Battle;
using ConstCollections.PJEnums.Skill;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstOthers;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.GameData.FormatCollection.Skill.Trigger;
using GameFlow.Battle.Controller;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData
{
  public class SkillDataManager : Singleton<SkillDataManager>
  {
    public static BattleInfoManager InfoManagerInstance{
      get{ 
        if (infoManagerInstance == null) 
        {
          infoManagerInstance = Object.FindObjectOfType<BattleInfoManager> ();
        }
        return infoManagerInstance;
      }
    }

    static BattleInfoManager infoManagerInstance;

    public bool InitiativeSkillDrawBySlot(List<ICommonSkill> initProbabilitySkillList, FightDataFormat selfFightData)
    {
      int[] _probabilityArray = new int[initProbabilitySkillList.Count + 1];
      Dictionary<int, ICommonSkill> _targetSkillMap = new Dictionary<int, ICommonSkill>();
      int _probabilitySum = 0;
      int _index = 0;
      initProbabilitySkillList.ForEach (skill => {

        var _triggerProb = skill.SkillCondition.TriggerList.Find(trigger =>{
          return trigger.Type == SKILL_TRIGGER_TYPE.PROBABILITY;
        }) as ProbabilityFormat;
          
        _triggerProb.IsForcedActive = false;

        // FIXME: yang-zhang if _triggerProb == null -> 100%
        _probabilityArray[_index] = _triggerProb.GetProbability();
        _probabilitySum += _probabilityArray[_index];
        _targetSkillMap[_index] = skill;
        _index++;
      });

      int _indexNormal = _index;
      _probabilityArray [_indexNormal] = 100 - _probabilitySum;
 
      int _targetIndex = PJMath.ProbabilityHelper.CalculateIndex (_probabilityArray);

      if (_targetIndex == _indexNormal)
        return false;

//      Debug.LogWarningFormat ("{0}, _targetSkillIndex = {1}", selfFightData.Type, _targetIndex);

      var _targetSkill = _targetSkillMap [_targetIndex];

      var _trigger = _targetSkill.SkillCondition.TriggerList.Find(trigger =>{
        return trigger.Type == SKILL_TRIGGER_TYPE.PROBABILITY;
      }) as ProbabilityFormat;

      _trigger.IsForcedActive = true;

      selfFightData.OneTurnFightData.AttackPowerType = ATTACK_POWER_TYPE.INITIATIVE_SKILL;

      return true;
    }

    public void CheckSkillCondition(TURN_STATES state, FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      if (selfFightData.OneTurnFightData.AllSkillFailed != null && selfFightData.OneTurnFightData.AllSkillFailed.Value == true)
        return;
      
      var _currentStateSkillList = selfFightData.SkillList.FindAll(skill => {
        if(skill.Level < SkillOthers.LEVEL_MIN)
          return false;
        
        if(skill.SkillCondition.IsActive == true)
          return false;

        return skill.SkillCondition.CheckState(state);
      });

      if (_currentStateSkillList == null)
        return;

      if (state == TURN_STATES.JUDGE_INITIATIVE_SKILL) {
        var _initProbabilitySkillList = _currentStateSkillList.FindAll (skill => {
          if (skill.SkillCondition.IsActive == true)
            return false;

          var _targetTrigger = skill.SkillCondition.TriggerList.Find (trigger => {
            return trigger.Type == SKILL_TRIGGER_TYPE.PROBABILITY && skill.Type == SKILL_TYPE.INITIATIVE;
          });

          return _targetTrigger != null;
        });

        if (_initProbabilitySkillList != null)
          InitiativeSkillDrawBySlot (_initProbabilitySkillList, selfFightData);
      } 

      _currentStateSkillList.ForEach (skill => {
        if(skill.SkillCondition.IsActive)
          return;
        
        if(skill.SkillCondition.CheckSuccess(selfFightData, otherFightData) && skill.Type != SKILL_TYPE.BUFF)
        {
          InfoManagerInstance.EnqueueMessage(INFO_FORMAT_LABEL.ACTIVE_SKILL, selfFightData, otherFightData, skill);
        }
      });
    }


    public bool ActiveEffect(TURN_STATES state, FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      bool _success = false;
      if (selfFightData.OneTurnFightData.AllSkillFailed != null && selfFightData.OneTurnFightData.AllSkillFailed.Value == true)
        return _success;
      
      selfFightData.SkillList.ForEach (skill => {
        if(skill.SkillCondition.IsActive == false)
          return;

        // Not active yet & state
        var _targetList = skill.EffectList.FindAll(effect=>{
          if(effect.EffectCondition.IsActive)
            return false;
          
          return effect.EffectCondition.CheckState(state);
        });

        if(_targetList == null)
          return;

        // Check condition & active effect
        _targetList.ForEach(effect => {
          effect.EffectCondition.CheckSuccess(selfFightData, otherFightData);

          if(effect.EffectCondition.IsActive)
          {
            effect.Active(selfFightData, otherFightData, targetQueue);
            _success = true;
          }
          else
          {
//            Debug.LogFormat("Effect [{0}] didn't active",effect);
          }
        });
          
      });

      return _success;
    }

    public IEnumerator ActiveSkillCoroutine(TURN_STATES state, FightDataFormat selfFightData, FightDataFormat otherFightData = null, Queue<AbsCharacterController> targetQueue = null)
    {
      if (selfFightData.OneTurnFightData.AllSkillFailed != null && selfFightData.OneTurnFightData.AllSkillFailed.Value == true)
        yield break;
      
      CheckSkillCondition (state, selfFightData, otherFightData);
      ActiveEffect (state, selfFightData, otherFightData, targetQueue);
      yield break;
    }

    public void ActiveSkill(TURN_STATES state, FightDataFormat selfFightData, FightDataFormat otherFightData = null, Queue<AbsCharacterController> targetQueue = null)
    {
      if (selfFightData.OneTurnFightData.AllSkillFailed != null && selfFightData.OneTurnFightData.AllSkillFailed.Value == true)
        return;
      
      CheckSkillCondition (state, selfFightData, otherFightData);
      ActiveEffect (state, selfFightData, otherFightData, targetQueue);
    }
  }

}
