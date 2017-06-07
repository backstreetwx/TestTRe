using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class TrickLearningFormat : AbsSkillEffectBase 
  {
    public TrickLearningFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = TrickLearningTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);
      this.trickLearningCp = _row.Cp_0;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      selfFightData.SkillList.ForEach (skill => {
        
        skill.SkillCondition.TriggerList.ForEach(trigger =>{
          ITrickLearning _iTrickLearning = trigger as ITrickLearning;
          if(_iTrickLearning != null && _iTrickLearning.EnableTrickLearning)
          {
            _iTrickLearning.TrickLearningOffset = skill.Level * this.trickLearningCp;
          }
        });

//        skill.EffectList.ForEach(effect => {
//          effect.EffectCondition.TriggerList.ForEach(trigger =>{
//            ITrickLearning _iTrickLearning = trigger as ITrickLearning;
//            if(_iTrickLearning != null)
//            {
//              _iTrickLearning.TrickLearningOffset = this.trickLearningOffset;
//            }
//          });
//        });

      });
    }

    #endregion

    float trickLearningCp;
  }
}


