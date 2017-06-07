using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Common.Skill
{
  [System.Serializable]
  public abstract class  AbsSkillTriggerBase
  {
    public SKILL_TRIGGER_TYPE Type {get {return this.type;}}

    public AbsSkillTriggerBase(ICommonSkill root, TriggerTypeIDMapFormat triggerMap)
    {
      this.root = root;
      this.type = triggerMap.Type;
    }

    public abstract bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData);

    public abstract void Reset ();

    public override string ToString ()
    {
      return JsonUtility.ToJson(this, true);
    }

    [SerializeField]
    protected ICommonSkill root;

    [SerializeField, ReadOnly]
    protected SKILL_TRIGGER_TYPE type;
  }
}
