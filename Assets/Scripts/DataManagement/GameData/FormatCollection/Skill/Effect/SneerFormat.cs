using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Character;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class SneerFormat : AbsSkillEffectBase 
  {
    public SneerFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData){}

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      if (targetQueue.Count != 0 || selfFightData.FightType != BATTLE_FIGHT_TYPE.DEFENDER)
        return;

      switch (selfFightData.Type) 
      {
      case CHARACTER_TYPE.HERO:
        {
          var _manager = GameObject.FindObjectOfType<HeroManager> ();
          this.Enqueue (targetQueue, _manager.AliveList, selfFightData.SlotID);
          break;
        }
      case CHARACTER_TYPE.ENEMY:
        {
          var _manager = GameObject.FindObjectOfType<EnemyManager> ();
          this.Enqueue (targetQueue, _manager.AliveList, selfFightData.SlotID);
          break;
        }
      default:
        break;
      }
    }

    #endregion

    void Enqueue(Queue<AbsCharacterController> targetQueue, List<AbsCharacterController> aliveColList, int targetSlotID)
    {
      var _target = aliveColList.Find (col => {
        return col.SlotID == targetSlotID;
      });

      if (_target == null)
        throw new System.NullReferenceException ();

      targetQueue.Enqueue(_target);
    }
  }
}
