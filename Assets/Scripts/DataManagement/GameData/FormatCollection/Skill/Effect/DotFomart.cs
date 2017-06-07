using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using DataManagement.TableClass.Skill.Effect;
using ConstCollections.PJEnums.Character;
using System.Collections.Generic;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Battle;

namespace DataManagement.GameData.FormatCollection.Skill.Effect
{
  [System.Serializable]
  public class DotFomart : AbsSkillEffectBase 
  {
    public float Damp;
    public float INTCp;
    public float INTSp;
    public float ATKCp;
    public float ATKSp;
    public float PoisonCp;
    public float Power;

    public DOT_TYPE Type;

    public DotFomart(ICommonSkill skill, SkillEffectTable dbData):base(skill, dbData)
    {
      var _row = DotTableReader.Instance.FindDefaultUnique((ushort)dbData.EffectTableID);
      this.Type = _row.DotType; 
      this.Damp = _row.Damp;
      this.INTCp = _row.INTCp;
      this.INTSp = _row.INTSp;
      this.ATKCp = _row.ATKCp;
      this.ATKSp = _row.ATKSp;
      this.PoisonCp = _row.PoisonCp;
    }

    #region ISkillEffect implementation

    public override void Active (FightDataFormat selfFightData, FightDataFormat otherFightData, Queue<AbsCharacterController> targetQueue)
    {
      
      switch (this.Type) 
      {
      case DOT_TYPE.BURN:
        {
          var _dotPoisonPower = 0.0F;
          var _posion = selfFightData.DotManager.DotList.Find(dot => dot.Type == DOT_TYPE.POISON);
          if (_posion != null)
            _dotPoisonPower = _posion.Power;

          float _VIT = 0;

          if (selfFightData.Type == CHARACTER_TYPE.HERO) 
          {
            var _heroAttributes = selfFightData.AttributeOriginCache as HeroAttributeFormat;
            var _attributesWithEquipment = HeroDataManager.Instance.CalculateAttributesWithEquipments (_heroAttributes, selfFightData.EquipmentList);

            _VIT = _attributesWithEquipment.VIT;
          }
          
          this.Power = _VIT * (this.INTCp + base.root.Level * this.INTSp) + selfFightData.FinalAttributesCache.ATK * (this.ATKCp + base.root.Level * this.ATKSp) + _dotPoisonPower * this.PoisonCp;

          otherFightData.DotManager.Add(new DataManagement.GameData.FormatCollection.Battle.Dot.BattleDotFormat(this.Type, this.Power, this.Damp));

          base.root.BattleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.SKILL_DOT_BURN_ATTACH, null, otherFightData, Mathf.FloorToInt(this.Power));
          break;
        }
      case DOT_TYPE.POISON:
        {
          float _VIT = 0;

          if (selfFightData.Type == CHARACTER_TYPE.HERO) 
          {
            var _heroAttributes = selfFightData.AttributeOriginCache as HeroAttributeFormat;
            var _attributesWithEquipment = HeroDataManager.Instance.CalculateAttributesWithEquipments (_heroAttributes, selfFightData.EquipmentList);

            _VIT = _attributesWithEquipment.VIT;
          }

          var _power = _VIT * (this.INTCp + base.root.Level * this.INTSp) + selfFightData.FinalAttributesCache.ATK * (this.ATKCp + base.root.Level * this.ATKSp);

          otherFightData.DotManager.Add(new DataManagement.GameData.FormatCollection.Battle.Dot.BattleDotFormat(this.Type, _power, this.Damp));
          break;
        }
      default:
        break;
      }
    }

    #endregion
  }
}
