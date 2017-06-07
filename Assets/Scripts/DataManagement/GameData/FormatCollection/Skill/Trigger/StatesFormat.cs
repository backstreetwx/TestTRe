using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Battle;
using ConstCollections.PJEnums.Character;
using System.Collections.Generic;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill;
using System.Linq;
using DataManagement.TableClass.Skill.Trigger;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Trigger
{
  [System.Serializable]
  public class StatesFormat : AbsSkillTriggerBase
  {
    //    public int CurrentCount;
    //    public int PersistenceCount;
    [ReadOnly]
    public TURN_STATES CurrentTurnState;
    [ReadOnly]
    public TURN_STATES SelfActiveState;
    [ReadOnly]
    public TURN_STATES OtherActiveState;
    [ReadOnly]
    public List<EXCEPTION_TYPE> CurrentExceptionList;
    [ReadOnly]
    public List<EXCEPTION_TYPE> InterruptCoditionList;

    public StatesFormat(ICommonSkill skill, TriggerTypeIDMapFormat triggerMap): base(skill, triggerMap)
    {
      if (triggerMap.TriggerID < 0)
        return;

      var _triggerStateRow = StatesTableReader.Instance.FindDefaultFirst((ushort)triggerMap.TriggerID);
      this.SelfActiveState = _triggerStateRow.SelfState;
      this.OtherActiveState = _triggerStateRow.OtherState;
      this.InterruptCoditionList = null ?? new List<EXCEPTION_TYPE>();
      this.CurrentExceptionList = null ?? new List<EXCEPTION_TYPE> ();
    }

    public override void Reset ()
    {
      this.CurrentTurnState = TURN_STATES.NONE;
    }

    public override bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      if (CheckInterrupt ())
        return false;

      bool _result = false;
      if (this.SelfActiveState != TURN_STATES.NONE) 
      {
        if (this.SelfActiveState == this.CurrentTurnState) 
        {
          _result = true;
        }
      }
      //
      //      if (this.OtherActiveState != TURN_STATES.NONE) 
      //      {
      //        if (this.OtherActiveState == skill.TurnState) 
      //        {
      //          _result = true;
      //        }
      //      }

      return _result;
    }

    public bool CheckInterrupt ()
    {
      return this.CurrentExceptionList.Any (_curExp => {
        return this.InterruptCoditionList.Any(_interruptCod => {
          if(_curExp == EXCEPTION_TYPE.NONE || _interruptCod == EXCEPTION_TYPE.NONE)
            return false;

          return _curExp == _interruptCod;
        });
      });
    }
  }
}
