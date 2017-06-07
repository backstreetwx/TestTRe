using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using ConstCollections.PJEnums.Character;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class MustHitFormat : AbsSkillEffectBase 
  {
    public MustHitFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData){}

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      selfFightData.OneTurnFightData.IsHitSuccess = true;

      switch (selfFightData.Type) 
      {
      case CHARACTER_TYPE.ENEMY:
        {
          var name = (selfFightData.AttributeOriginCache as EnemyAttributeFormat).NameString;
          Debug.LogWarningFormat ("[Enemy] {0} : MUST_HIT actived", name);
          break;
        }
      case CHARACTER_TYPE.HERO:
        {
          var name = (selfFightData.AttributeOriginCache as HeroAttributeFormat).NameString;
          Debug.LogWarningFormat ("[Hero] {0} : MUST_HIT actived", name);
          break;
        }
      default:
        break;
      }

    }

    #endregion
  }
}
