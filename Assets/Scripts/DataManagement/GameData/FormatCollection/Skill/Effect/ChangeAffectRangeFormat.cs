using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill.Effect;
using Common;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class ChangeAffectRangeFormat : AbsSkillEffectBase 
  {
    public SKILL_AFFECT_RANGE AffectRange;
    public SKILL_AFFECT_TYPE AffectType;
    public List<short> CustomAffectSlotIDList;

    public ChangeAffectRangeFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = ChangeAffectRangeTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);

      this.AffectRange = _row.AffectRange;
      this.AffectType = _row.AffectType;

      this.CustomAffectSlotIDList = new List<short> ();

      if (_row.EnableSlotID_0) {
        this.CustomAffectSlotIDList.Add (0);
      }
      if (_row.EnableSlotID_1) {
        this.CustomAffectSlotIDList.Add (1);
      }
      if (_row.EnableSlotID_2) {
        this.CustomAffectSlotIDList.Add (2);
      }
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      selfFightData.OneTurnFightData.SkillAffectRange = this.AffectRange;
      selfFightData.OneTurnFightData.SkillAffectType = this.AffectType;

      if (this.CustomAffectSlotIDList.Count > 0)
        selfFightData.OneTurnFightData.SkillCustomAffectSlotIDList = this.CustomAffectSlotIDList.CloneEx ();

      switch (this.AffectType) 
      {
      case SKILL_AFFECT_TYPE.CHANGE_HIT_TARGET:
        {
          ChangeHitTarget (selfFightData, targetQueue);
          break;
        }
      default:
        break;
      }

    }

    #endregion

    void ChangeHitTarget(FightDataFormat selfFightData, Queue<AbsCharacterController> targetQueue)
    {
      switch (selfFightData.Type) 
      {
      case CHARACTER_TYPE.HERO:
        {
          if (this.TargetType == SKILL_TARGET_TYPE.OTHER_GROUP) 
          {
            var _manager = GameObject.FindObjectOfType<EnemyManager> ();
            Enqueue (targetQueue, _manager.AliveList);
          }
          break;
        }
      case CHARACTER_TYPE.ENEMY:
        {
          if (this.TargetType == SKILL_TARGET_TYPE.OTHER_GROUP) 
          {
            var _manager = GameObject.FindObjectOfType<HeroManager> ();
            Enqueue (targetQueue, _manager.AliveList);
          }
          break;
        }
      default:
        break;
      }

    }

    void Enqueue(Queue<AbsCharacterController> targetQueue, List<AbsCharacterController> aliveColList, FightDataFormat selfFightData = null)
    {
      switch (this.AffectRange) 
      {
      case SKILL_AFFECT_RANGE.ALL_GROUP:
        {
          targetQueue.Clear ();
          aliveColList.ForEach (col => {
            targetQueue.Enqueue(col);
          });
          break;
        }
      case SKILL_AFFECT_RANGE.EXCLUDE_CURRENT:
        {
          targetQueue.Clear ();
          aliveColList.ForEach (col => {
            if(col.FightData.SlotID != selfFightData.SlotID)
              targetQueue.Enqueue(col);
          });
          break;
        }
      default:
        break;
      }
    }
  }
}
