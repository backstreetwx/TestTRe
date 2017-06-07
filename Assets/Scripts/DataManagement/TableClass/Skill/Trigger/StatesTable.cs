using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Battle;

namespace DataManagement.TableClass.Skill.Trigger
{
  [System.Serializable]
  public class StatesTable : AbstractTable 
  {
    public TURN_STATES SelfState;
    public TURN_STATES OtherState;
  }
}
