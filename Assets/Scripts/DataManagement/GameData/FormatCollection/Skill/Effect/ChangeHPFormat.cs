using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.TableClass.Skill.Effect;
using DataManagement.GameData.FormatCollection.Battle;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Character;
using ConstCollections.PJEnums.Skill;
using ConstCollections.PJEnums.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class ChangeHPFormat : AbsSkillEffectBase 
  {
    public float INTCp;
    public float INTSp;

    public float HPMaxCp;
    public float HPMaxSp;

    public float NormCp;
    public float NormSp;

    public int OffsetHP;

    public ChangeHPFormat(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = ChangeHPTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);
      this.INTCp = _row.INTCp;
      this.INTSp = _row.INTSp;
      this.HPMaxCp = _row.HPMaxCp;
      this.HPMaxSp = _row.HPMaxSp;
      this.NormCp = _row.NormCp;
      this.NormSp = _row.NormSp;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      this.OffsetHP = 0;

      if (base.TargetType == SKILL_TARGET_TYPE.SELF_GROUP) 
      {
        this.SetOffsetHP (selfFightData);

        selfFightData.AttributesAggregateBuff.HP += this.OffsetHP;
        selfFightData.CalculateFinalAttributes ();

        if (this.OffsetHP > 0) 
        {
          base.root.BattleInfoManagerScript.EnqueueMessage (
            INFO_FORMAT_LABEL.HP_UP, 
            selfFightData, null, 
            this.OffsetHP);
        }
      } 
      else if (base.TargetType == SKILL_TARGET_TYPE.OTHER_GROUP) 
      {
        this.SetOffsetHP (otherFightData);

        otherFightData.AttributesAggregateBuff.HP += this.OffsetHP;
        otherFightData.CalculateFinalAttributes ();

        if (this.OffsetHP > 0) 
        {
          base.root.BattleInfoManagerScript.EnqueueMessage (
            INFO_FORMAT_LABEL.HP_UP, 
            null, otherFightData, 
            this.OffsetHP);
        }
      }


    }

    #endregion

    void SetOffsetHP(FightDataFormat fightData)
    {
      if (fightData.Type == CHARACTER_TYPE.HERO) 
      {
        var _heroAttributes = fightData.AttributeOriginCache as HeroAttributeFormat;
        var _attributesWithEquipment = HeroDataManager.Instance.CalculateAttributesWithEquipments (_heroAttributes, fightData.EquipmentList);

        var _INT = _attributesWithEquipment.INT;

        fightData.EquipmentList.ForEach (item => {
          var _attributeOffset = (item.AttributesBuff as HeroAttributeFormat);
          if (_attributeOffset != null)
            _INT += (item.AttributesBuff as HeroAttributeFormat).INT;
        });

        var _HPMax = fightData.FinalAttributesCache.HPMax;

        var _HPOffset = _INT * (this.INTCp + base.root.Level * this.INTSp) + _HPMax * (this.HPMaxCp + base.root.Level * this.HPMaxSp) + this.NormCp + base.root.Level * this.NormSp;

        this.OffsetHP = Mathf.FloorToInt (_HPOffset);
      } else if (fightData.Type == CHARACTER_TYPE.ENEMY) {
        var _INT = 0.0F;
        var _HPMax = fightData.FinalAttributesCache.HPMax;

        var _HPOffset = _INT * (this.INTCp + base.root.Level * this.INTSp) + _HPMax * (this.HPMaxCp + base.root.Level * this.HPMaxSp) + this.NormCp + base.root.Level * this.NormSp;

        this.OffsetHP = Mathf.FloorToInt (_HPOffset);
      }

    }
  }
}
