using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Common.Skill;
using System.Collections.Generic;
using DataManagement.TableClass.Skill.Trigger;
using DataManagement.GameData.FormatCollection.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Trigger
{
  [System.Serializable]
  public class ProbabilityFormat : AbsSkillTriggerBase, ITrickLearning
  {
    [ReadOnly]
    public float Cp0;
    [ReadOnly]
    public float Cp1;

    [ReadOnly]
    public float SkillLevel;
    [ReadOnly]
    public bool? IsForcedActive;

    #region ITrickLearning implementation

    public bool EnableTrickLearning {
      get {
        return this.enableTrickLearning;
      }
      set {
        this.enableTrickLearning = value;
      }
    }

    public float TrickLearningOffset {
      get {
        return this.trickLearningOffset;
      }
      set {
        this.trickLearningOffset = value;
      }
    }

    #endregion

    public ProbabilityFormat(ICommonSkill skill, TriggerTypeIDMapFormat triggerMap): base(skill, triggerMap)
    {
      if (triggerMap.TriggerID < 0)
        return;

      var _dbData = ProbabilityTableReader.Instance.FindDefaultFirst((ushort)triggerMap.TriggerID);
      this.Cp0 = _dbData.Cp_0;
      this.Cp1 = _dbData.Cp_1;
      this.enableTrickLearning = _dbData.EnableTrickLearning;
      this.SkillLevel = this.root.Level;

    }

    public override bool CheckSuccess (FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      if (this.IsForcedActive != null)
        return this.IsForcedActive.Value;

      return PJMath.ProbabilityHelper.TrySuccess(GetProbability ());
    }

    public override void Reset ()
    {
      this.IsForcedActive = null;
    }

    public int GetProbability ()
    {
      float _value = Cp0 + SkillLevel * Cp1 + this.trickLearningOffset;
      return Mathf.FloorToInt(_value);
    }

    [SerializeField, ReadOnly]
    public bool enableTrickLearning;
    [SerializeField, ReadOnly]
    public float trickLearningOffset;
  }
}
