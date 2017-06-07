using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData.FormatCollection.Common;
using Common;
using System.Linq;
using ConstCollections.PJEnums.Battle;
using DataManagement.GameData.FormatCollection.Battle.Dot;

namespace DataManagement.GameData.FormatCollection.Battle
{
  [System.Serializable]
  public class FightDataFormat
  {
    public int SlotID;
    public CHARACTER_TYPE Type;
    public BATTLE_FIGHT_TYPE FightType;

    public List<ICommonSkill> SkillList;
    public List<CommonEquipmentFormat> EquipmentList;

    public BattleDotManagerFormat DotManager;

    public OneTurnFightDataFormat OneTurnFightData;

    public event System.Action<CommonAttributeFormat> FinalAttributesChangedEvent = delegate(CommonAttributeFormat obj) {};

    public CommonAttributeFormat AttributeOriginCache
    {
      get
      { 
        return this.attributeOriginCache;
      }
      set
      {
        this.attributeOriginCache = value.CloneEx ();
        this.CalculateFinalAttributes ();
      }
    }

    public CommonAttributeFormat AttributesAggregateBuff
    {
      get
      { 
        return this.attributesAggregateBuff;
      }
      set
      {
        this.attributesAggregateBuff = value.CloneEx ();
        this.CalculateFinalAttributes ();
      }
    }

    public CommonAttributeFormat FinalAttributesCache{
      get
      {
        return this.finalAttributesCache;
      }
    }

    public FightDataFormat()
    {
      this.attributeOriginCache = new CommonAttributeFormat (true);
      this.attributesAggregateBuff = new CommonAttributeFormat (true);
      this.finalAttributesCache = new CommonAttributeFormat(true);
      this.DotManager = new BattleDotManagerFormat ();
      this.OneTurnFightData = new OneTurnFightDataFormat ();

      this.CalculateFinalAttributes ();
    }

    public FightDataFormat(HeroAttributeFormat heroAttributes)
    {
      this.attributeOriginCache = heroAttributes.CloneEx();
      this.SlotID = heroAttributes.SlotID;
      this.Type = CHARACTER_TYPE.HERO;
    }

    public FightDataFormat(HeroDataFormat hero)
    {
      this.attributeOriginCache = new CommonAttributeFormat (true);
      this.attributesAggregateBuff = new CommonAttributeFormat (true);
      this.finalAttributesCache = new CommonAttributeFormat(true);
      this.DotManager = new BattleDotManagerFormat ();
      this.OneTurnFightData = new OneTurnFightDataFormat ();
      this.InitFight (hero);
    }

    public FightDataFormat(EnemyDataFormat enemy)
    {
      this.attributeOriginCache = new CommonAttributeFormat (true);
      this.attributesAggregateBuff = new CommonAttributeFormat (true);
      this.finalAttributesCache = new CommonAttributeFormat(true);
      this.DotManager = new BattleDotManagerFormat ();
      this.OneTurnFightData = new OneTurnFightDataFormat ();
      this.InitFight (enemy);
    }

    public void Clear()
    {
      this.attributeOriginCache.ClearToZero ();
      this.attributesAggregateBuff.ClearToZero ();
      this.finalAttributesCache.ClearToZero ();
      this.DotManager.Clear();
      this.OneTurnFightData.Clear ();
    }

    public void InitFight(HeroDataFormat hero)
    {
      this.Clear ();
      var _attribute = (hero.Attributes as HeroAttributeFormat).CloneEx();
      this.SlotID = _attribute.SlotID;
      this.Type = CHARACTER_TYPE.HERO;
      this.attributeOriginCache = _attribute;
    }

    public void InitTurn(HeroDataFormat hero, BATTLE_FIGHT_TYPE fightType)
    {
      this.FightType = fightType;

      var _skill = hero.SkillList.CloneEx ();
      this.SkillList = _skill.Cast<ICommonSkill> ().ToList ();
      this.SkillList.ForEach (skill => skill.Reset ());

      var _equipt = hero.EquipmentList.CloneEx ();
      this.EquipmentList = _equipt.Cast<CommonEquipmentFormat>().ToList ();

      this.OneTurnFightData.Clear();

      this.CalculateFinalAttributes ();
    }

    public void InitFight(EnemyDataFormat enemy)
    {
      this.Clear ();
      var _attribute = (enemy.Attributes as EnemyAttributeFormat).CloneEx();
      this.SlotID = _attribute.SlotID;
      this.Type = CHARACTER_TYPE.ENEMY;
      this.attributeOriginCache = _attribute;

      if(enemy.AttributesAggregateBuff != null)
        this.attributesAggregateBuff = enemy.AttributesAggregateBuff.CloneEx();
    }

    public void InitTurn(EnemyDataFormat enemy, BATTLE_FIGHT_TYPE fightType)
    {
      this.FightType = fightType;

      var _skill = enemy.SkillList.CloneEx ();
      this.SkillList = _skill.Cast<ICommonSkill> ().ToList ();
      this.SkillList.ForEach (skill => skill.Reset ());

      var _equipt = enemy.EquipmentList.CloneEx ();
      this.EquipmentList = _equipt.Cast<CommonEquipmentFormat>().ToList ();

      this.OneTurnFightData.Clear();

      this.CalculateFinalAttributes ();
    }

    public CommonAttributeFormat CalculateFinalAttributes()
    {
      if (this.Type == CHARACTER_TYPE.HERO) 
      {
        var _heroAttributes = this.attributeOriginCache as HeroAttributeFormat;
        var _attributesWithEquipment = HeroDataManager.Instance.CalculateAttributesWithEquipments (_heroAttributes, this.EquipmentList);
        this.finalAttributesCache = _attributesWithEquipment + this.attributesAggregateBuff + this.OneTurnFightData.AttributesBuff;
      } 
      else 
      {
        this.finalAttributesCache = this.attributeOriginCache + this.attributesAggregateBuff + this.OneTurnFightData.AttributesBuff;
      }

      this.finalAttributesCache.HP = Mathf.Clamp (this.finalAttributesCache.HP, 0, this.finalAttributesCache.HPMax);

      this.FinalAttributesChangedEvent.Invoke (this.finalAttributesCache);

      return this.finalAttributesCache;
    }

    public void Notify()
    {
      this.FinalAttributesChangedEvent.Invoke (this.finalAttributesCache);
    }

    public void GetDamage(int damage, FightDataFormat attacker)
    {
      this.OneTurnFightData.CalculateDamageFinal (damage);

      SkillDataManager.Instance.ActiveSkill (ConstCollections.PJEnums.Battle.TURN_STATES.OUTPUT_DAMAGE, this, attacker);

      this.attributesAggregateBuff.HP -= this.OneTurnFightData.GetDamageFinal;
      this.CalculateFinalAttributes ();
    }

    public bool GetDamageByDot()
    {
      if (!this.DotManager.DotExist)
        return false;

      this.DotManager.Active (this);
      this.CalculateFinalAttributes ();
      return true;
    }

    public bool DeadCheck()
    {
      if (this.FinalAttributesCache.HP <= 0) 
      {
        this.OneTurnFightData.IsDead = true;
      } 
      else 
      {
        this.OneTurnFightData.IsDead = false;
      }

      return this.OneTurnFightData.IsDead.Value;
    }

    public bool DeadCheck(FightDataFormat attacker)
    {
      SkillDataManager.Instance.ActiveSkill (ConstCollections.PJEnums.Battle.TURN_STATES.JUDGE_DEAD, this, attacker);

      if (this.OneTurnFightData.IsDead != null) 
      {
        return this.OneTurnFightData.IsDead.Value;
      }

      if (this.FinalAttributesCache.HP <= 0) 
      {
        this.OneTurnFightData.IsDead = true;
      } 
      else 
      {
        this.OneTurnFightData.IsDead = false;
      }

      return this.OneTurnFightData.IsDead.Value;
    }

    [ReadOnly, SerializeField]
    CommonAttributeFormat finalAttributesCache;
    [ReadOnly, SerializeField]
    CommonAttributeFormat attributeOriginCache;
    [ReadOnly, SerializeField]
    CommonAttributeFormat attributesAggregateBuff;
  }
}
