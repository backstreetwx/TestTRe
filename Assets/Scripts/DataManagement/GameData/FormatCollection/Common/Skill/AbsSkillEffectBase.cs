using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Common.Skill
{
  [System.Serializable]
  public abstract class AbsSkillEffectBase
  {
    [ReadOnly]
    public ConditionController EffectCondition;
    [ReadOnly]
    public SKILL_TARGET_TYPE TargetType;

    public AbsSkillEffectBase(ICommonSkill root, SkillEffectTable dbData)
    {
      this.root = root;
      this.TargetType = dbData.TargetType;

      InitEffectCondition (this.root, dbData);
    }

    public virtual void InitEffectCondition(ICommonSkill root, SkillEffectTable dbData)
    {
      if (dbData == null)
        return;
      
      // Init skill triggers
      var _dbEffectTriggerList = EffectTriggerTableReader.Instance.FindDefault(dbData.SkillID, dbData.EffectID);
      if (_dbEffectTriggerList != null) 
      {
        List<TriggerTypeIDMapFormat> _skillTriggerMapList = new List<TriggerTypeIDMapFormat> ();
        _dbEffectTriggerList.ForEach (skillTrigger => {
          _skillTriggerMapList.Add(new TriggerTypeIDMapFormat(skillTrigger));
        });

        this.EffectCondition = new ConditionController (root, _skillTriggerMapList);
      }
    }

    public virtual void Reset ()
    {
      this.EffectCondition.Reset();
    }

    public abstract void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue);

    [SerializeField]
    protected ICommonSkill root;
  }
}
