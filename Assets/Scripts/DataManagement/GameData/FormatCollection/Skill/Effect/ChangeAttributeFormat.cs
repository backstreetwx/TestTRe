using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;
using Common;
using DataManagement.GameData.FormatCollection.Battle;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Common;
using ConstCollections.PJEnums.Skill;
using ConstCollections.PJEnums.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class ChangeAttributeFormat : AbsSkillEffectBase 
  {
    public ChangeAttributeTable AttributeTable;
    public CommonAttributeFormat OffsetAttributes;

    public ChangeAttributeFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = ChangeAttributeTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);
      this.AttributeTable = _row.CloneEx();
      this.OffsetAttributes = new CommonAttributeFormat (true);
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {

      this.OffsetAttributes.ClearToZero ();

      var _roundOffset = 0;

      _roundOffset = base.root.Level / (int)this.AttributeTable.ATKSp;
      this.OffsetAttributes.ATK = this.AttributeTable.ATKCp + _roundOffset;
      _roundOffset = base.root.Level / (int)this.AttributeTable.DEFSp;
      this.OffsetAttributes.DEF = this.AttributeTable.DEFCp + _roundOffset;

      _roundOffset = base.root.Level / (int)this.AttributeTable.CRISp;
      this.OffsetAttributes.CRI = this.AttributeTable.CRICp + _roundOffset;
      _roundOffset = base.root.Level / (int)this.AttributeTable.AVDSp;
      this.OffsetAttributes.AVD = this.AttributeTable.AVDCp + _roundOffset;
      _roundOffset = base.root.Level / (int)this.AttributeTable.HITSp;
      this.OffsetAttributes.HIT = this.AttributeTable.HITCp + _roundOffset;

      _roundOffset = base.root.Level / (int)this.AttributeTable.ACSp;
      this.OffsetAttributes.AC = this.AttributeTable.ACCp + _roundOffset;

      if (base.TargetType == SKILL_TARGET_TYPE.SELF_GROUP) 
      {
        if (this.AttributeTable.IsAggregate) 
        {
          selfFightData.AttributesAggregateBuff += this.OffsetAttributes;
        } 
        else 
        {
          selfFightData.OneTurnFightData.AttributesBuff += this.OffsetAttributes;
        }

        selfFightData.CalculateFinalAttributes ();
      } 
      else if (base.TargetType == SKILL_TARGET_TYPE.OTHER_GROUP) 
      {
        if (this.AttributeTable.IsAggregate) 
        {
          otherFightData.AttributesAggregateBuff += this.OffsetAttributes;
        } 
        else 
        {
          otherFightData.OneTurnFightData.AttributesBuff += this.OffsetAttributes;
        }

        otherFightData.CalculateFinalAttributes ();

        ShowDefenderInfo (selfFightData, otherFightData);
      }

    }

    #endregion

    void ShowDefenderInfo(FightDataFormat selfFightData, FightDataFormat otherFightData)
    {
      if (this.OffsetAttributes.AC < 0) {
        base.root.BattleInfoManagerScript.EnqueueMessage (
          INFO_FORMAT_LABEL.DEFENDER_AC_DOWN, 
          selfFightData, 
          otherFightData, 
          Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.AC)));
      } else if (this.OffsetAttributes.ATK < 0 && this.OffsetAttributes.DEF < 0) {
        base.root.BattleInfoManagerScript.EnqueueMessage (
          INFO_FORMAT_LABEL.DEFENDER_ATK_DOWN_DEF_DOWN, 
          selfFightData, 
          otherFightData, 
          Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.ATK)), 
          Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.DEF)));
      } else if (this.OffsetAttributes.HIT < 0 && this.OffsetAttributes.AVD < 0) {
        if (this.OffsetAttributes.CRI < 0) {
          base.root.BattleInfoManagerScript.EnqueueMessage (
            INFO_FORMAT_LABEL.DEFENDER_HIT_DOWN_AVD_DOWN_CRI_DOWN, 
            selfFightData, 
            otherFightData, 
            Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.HIT)), 
            Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.AVD)), 
            Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.CRI)));
          return;
        }
        base.root.BattleInfoManagerScript.EnqueueMessage (
          INFO_FORMAT_LABEL.DEFENDER_HIT_DOWN_AVD_DOWN, 
          selfFightData, 
          otherFightData, 
          Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.HIT)), 
          Mathf.Abs(Mathf.FloorToInt(this.OffsetAttributes.AVD)));
      }
    }
  }
}
