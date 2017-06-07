using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Skill;
using ConstCollections.PJEnums;
using DataManagement.TableClass.Skill.Trigger;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Trigger
{
  [System.Serializable]
  public class SkillMustFaildFormat : AbsSkillTriggerBase
  {
    public float Delta_ATK_MAG;
    public COMPARE_TYPE CompareType;
    public int Probability;

    public SkillMustFaildFormat(ICommonSkill skill, TriggerTypeIDMapFormat triggerMap): base(skill, triggerMap)
    {
      if (triggerMap.TriggerID < 0)
        return;

      var _dbData = SkillMustFailedTableReader.Instance.FindDefaultFirst((ushort)triggerMap.TriggerID);
      this.Delta_ATK_MAG = _dbData.Delta_ATK_MAG;
      this.CompareType = _dbData.CompareType;
      this.Probability = _dbData.Probability;
    }

    #region implemented abstract members of SkillTriggerBase

    public override bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      switch (this.CompareType) 
      {
      case COMPARE_TYPE.GREATER:
        {
          var _attribute = selfFightData.FinalAttributesCache;
          var _delta = _attribute.ATK - _attribute.MAG;
          if (_delta > this.Delta_ATK_MAG) 
          {
            return PJMath.ProbabilityHelper.TrySuccess(this.Probability);
          }
          return false;
        }
      default:
        {
          return false;
        }
      }
    }

    public override void Reset ()
    {
      
    }

    #endregion

  }
}
