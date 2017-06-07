using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;
using System;
using DataManagement.TableClass.Hero;
using DataManagement.GameData.FormatCollection.Common;
using DataManagement.TableClass.Skill;
using System.Linq;
using ConstCollections.PJEnums.Character;
using ConstCollections.PJConstOthers;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Equipment;
using DataManagement.TableClass.Equipment;

namespace DataManagement.GameData.FormatCollection
{
  [System.Serializable]
  public class HeroDataFormat 
  {
    public HeroAttributeFormat Attributes;
    [ReadOnly]
    public List<HeroSkillFormat> SkillList;
    [ReadOnly]
    public List<HeroEquipmentFormat> EquipmentList;
    public HeroAnimationFormat AnimationInfo;

    public HeroAttributeFormat AttributesWithEquipments
    {
      get
      {
        if (this.attributesWithEquipments == null) 
        {
          this.attributesWithEquipments = HeroDataManager.Instance.CalculateAttributesWithEquipments(this.Attributes, this.EquipmentList.Cast<CommonEquipmentFormat>().ToList());
        }

        return this.attributesWithEquipments;
      }
    }

//    public HeroDataFormat()
//    {
//      this.Attributes = new HeroAttributeFormat();
//      this.SkillList = new List<HeroSkillFormat>();
//      this.EquipmentList = new List<HeroEquipmentFormat>();
//      this.AnimationInfo = new HeroAnimationFormat ();
//    }

    public HeroDataFormat(HeroAttributeFormat attributes, List<HeroSkillFormat> skillList, List<HeroEquipmentFormat> equipmentList, HeroAnimationFormat animation)
    {
      this.Attributes = attributes.CloneEx();
      this.SkillList = skillList.CloneEx ();
      this.EquipmentList = equipmentList.CloneEx ();
      this.AnimationInfo = animation.CloneEx ();
    }

    public HeroDataFormat (HeroSaveDataFormat heroSaveData)
    {
      HeroAttributeFormat _attributes = new HeroAttributeFormat (heroSaveData);

      List<HeroSkillFormat> _skillList = new List<HeroSkillFormat> ();
      heroSaveData.SkillSaveDataList.ForEach (item => {
        HeroSkillFormat _skill = new HeroSkillFormat(item);
        _skillList.Add(_skill);
      });


      List<HeroEquipmentFormat> _equipmentList = new List<HeroEquipmentFormat> ();
      heroSaveData.EquipmentSaveDataList.ForEach (item => {
        HeroEquipmentFormat _equip = new HeroEquipmentFormat(item);
        _equipmentList.Add(_equip);
      });

      this.Attributes = _attributes;
      this.SkillList = _skillList;
      this.EquipmentList = _equipmentList;

      var _data = HeroTableReader.Instance.FindDefaultUnique(heroSaveData.DBHeroID);;
      this.AnimationInfo = new HeroAnimationFormat (_data);
    }

    public bool EXPUp(int exp, bool writeToSavedata = true, System.Action<HeroAttributeFormat> onGotEXP = null, System.Action<HeroAttributeFormat> onLevelUp = null, System.Action<HeroAttributeFormat> onAllLevelUp = null)
    {
      Debug.LogFormat("Hero[{0}]:{1} got exp = {2}", this.Attributes.SlotID, this.Attributes.NameString, exp);
      this.Attributes.ExpUp (exp, onGotEXP, onLevelUp, onAllLevelUp);
      if(writeToSavedata)
        return HeroSaveDataManager.Instance.Overwrite (this);
      
      return true;
    }

    HeroAttributeFormat attributesWithEquipments;
  }

  [System.Serializable]
  public class SortHeroDataBySlotID : IComparer<HeroDataFormat>
  {
    #region IComparer implementation
    public int Compare (HeroDataFormat x, HeroDataFormat y)
    {
      return x.Attributes.SlotID.CompareTo (y.Attributes.SlotID);
    }
    #endregion
  }

  [System.Serializable]
  public class HeroAttributeFormat : CommonAttributeFormat
  {
    public static readonly float EXP_MAX_BASE_DUMMY = 9;
    public int SlotID;
    public ushort DBHeroID;
    public ushort[] DBNameIDArray;
    public string NameString;

    public int EXP;
    public int EXPMax;

    public float STR;// Stregth
    public float VIT;// Physique 
    public float INT;// Intelligence
    public float DEX;// Speed

    public float STRUp;
    public float VITUp;
    public float INTUp;
    public float DEXUp;

    public int SkillPoint;
    public bool Active;

    public HeroAttributeFormat()
    {
      this.SlotID = -1;
      this.DBHeroID = 0;
      this.DBNameIDArray = new ushort[3];
      this.NameString = "";
      this.SkillPoint = 0;
      this.Active = false;
      // other float members will init by 0(default value)
    }

    public HeroAttributeFormat(bool clearToZero) : base(clearToZero)
    {
      if (clearToZero) 
      {
        this.ClearToZero ();
        return;
      }

      this.DBHeroID = 0;
      this.DBNameIDArray = new ushort[3];
      this.NameString = "";
      this.SkillPoint = 0;
      this.Active = false;
    }

    public HeroAttributeFormat(CommonAttributeFormat commonAttributes)
    {
      base.Level = commonAttributes.Level;
      base.HP = commonAttributes.HP;
      base.HPMax = commonAttributes.HPMax;
      base.RES = commonAttributes.RES;
      base.ATK = commonAttributes.ATK;
      base.MAG = commonAttributes.MAG;
      base.DEF = commonAttributes.DEF;
      base.AC = commonAttributes.AC;
      base.CRI = commonAttributes.CRI;
      base.PEN = commonAttributes.PEN;
      base.HIT = commonAttributes.HIT;
      base.AVD = commonAttributes.AVD;

      this.DBHeroID = 0;
      this.DBNameIDArray = new ushort[3];
      this.NameString = "";
      this.SkillPoint = 0;
      this.Active = false;
    }

    public HeroAttributeFormat(HeroSaveDataFormat heroSaveData, bool hpFull = true)
    {
      this.SlotID = heroSaveData.SlotID;
      this.DBHeroID = heroSaveData.DBHeroID;
      this.DBNameIDArray = heroSaveData.DBNameIDArray.CloneEx();
      this.NameString = heroSaveData.NameString;
      base.Level = heroSaveData.Level;

      this.EXP = heroSaveData.EXP;
      this.EXPMax = CalculateExpMax (base.Level);

      this.STR = heroSaveData.STR;
      this.VIT = heroSaveData.VIT;
      this.INT = heroSaveData.INT;
      this.DEX = heroSaveData.DEX;

      this.STRUp = heroSaveData.STRUp;
      this.VITUp = heroSaveData.VITUp;
      this.INTUp = heroSaveData.INTUp;
      this.DEXUp = heroSaveData.DEXUp;

      CalculateCommonAttribute (hpFull);

      this.SkillPoint = heroSaveData.SkillPoint;
      this.Active = heroSaveData.Active;
    }

    public override void ClearToZero()
    {
      base.ClearToZero ();

      this.SlotID = 0;
      this.DBHeroID = 0;
      this.DBNameIDArray = new ushort[3];
      this.NameString = "";

      this.EXP = 0;
      this.EXPMax = 0;

      this.STR = 0;
      this.VIT = 0;
      this.INT = 0;
      this.DEX = 0;

      this.STRUp = 0;
      this.VITUp = 0;
      this.INTUp = 0;
      this.DEXUp = 0;

      this.SkillPoint = 0;
      this.Active = false;
    }

    public static HeroAttributeFormat operator + (HeroAttributeFormat c1, HeroAttributeFormat c2) 
    {
      var _sum = c1.Add (c2);

      if (_sum is HeroAttributeFormat)
        return _sum as HeroAttributeFormat;
      else
        return new HeroAttributeFormat (_sum);
    }

    public HeroAttributeFormat AddLevelOnly(HeroAttributeFormat c)
    {
      base.Level += c.Level;

      return this;
    }

    public HeroAttributeFormat AddBaseOnly(HeroAttributeFormat c2)
    {
      this.STR += c2.STR;
      this.VIT += c2.VIT;
      this.INT += c2.INT;
      this.DEX += c2.DEX;

      return this;
    }

    public HeroAttributeFormat AddCommonOnly(HeroAttributeFormat c2)
    {
      base.HPMax += c2.HPMax;
      base.HPMax = Mathf.FloorToInt (base.HPMax);
      base.HP += c2.HP;
      base.HP = Mathf.FloorToInt (base.HP);
      base.RES += c2.RES;
      base.ATK += c2.ATK;
      base.MAG += c2.MAG;
      base.DEF += c2.DEF;
      base.AC += c2.AC;
      base.CRI += c2.CRI;
      base.PEN += c2.PEN;
      base.HIT += c2.HIT;
      base.AVD += c2.AVD;
      base.Level += c2.Level;
      return this;
    }

    public int CalculateExpMax(int level)
    {
      return Mathf.FloorToInt(Mathf.Pow(level,3) + HeroAttributeFormat.EXP_MAX_BASE_DUMMY);
    }
      
    public void CalculateCommonAttribute(bool hpFull = true)
    {
      var _constValue = HeroAttributeConstTableReader.Instance.GetCachedValue ();

      base.HPMax = Mathf.FloorToInt(this.VIT * _constValue.HPMax);
      if(hpFull)
        base.HP = base.HPMax;
      base.RES = this.INT * _constValue.RES;
      base.ATK = this.STR * _constValue.ATK;
      base.MAG = this.INT ;
      base.DEF = this.VIT * _constValue.DEF;
      base.AC = 0.0F;
      base.CRI = this.DEX * _constValue.CRI;
      base.PEN = this.STR * _constValue.PEN;
      base.HIT = this.DEX * _constValue.HIT;
      base.AVD = this.DEX * _constValue.AVD;
    }

    public void ExpUp(int exp, System.Action<HeroAttributeFormat> onGotEXP = null, System.Action<HeroAttributeFormat> onLevelUp = null, System.Action<HeroAttributeFormat> onAllLevelUp = null)
    {
      this.EXP += exp;

      if (onGotEXP != null)
        onGotEXP.Invoke (this);

      if (this.EXP < this.EXPMax) 
      {
        return;
      }

      int _currentExpMax = this.EXPMax;
      bool _levelUped = false;
      while (this.EXP >= _currentExpMax) 
      {
        this.EXP -= _currentExpMax;
        this.Level++;
        _currentExpMax = CalculateExpMax (this.Level);
        this.EXPMax = _currentExpMax;
        this.STR += this.STRUp;
        this.VIT += this.VITUp;
        this.INT += this.INTUp;
        this.DEX += this.DEXUp;
        this.SkillPoint += SkillOthers.SKILL_POINT_UP;
        _levelUped = true;

        if(onLevelUp != null)
          onLevelUp.Invoke (this);
      }

      if (_levelUped && onAllLevelUp != null)
        onAllLevelUp.Invoke (this);
    } 

    protected override CommonAttributeFormat Add(CommonAttributeFormat c2)
    {
      var _sumCommon = base.Add (c2);

      if (this is HeroAttributeFormat) 
      {
        var _sumHero = new HeroAttributeFormat (_sumCommon);

        var _thisHero = this as HeroAttributeFormat;
        _sumHero.STR = _thisHero.STR;
        _sumHero.VIT = _thisHero.VIT;
        _sumHero.INT = _thisHero.INT;
        _sumHero.DEX = _thisHero.DEX;
          
        if (c2 is HeroAttributeFormat) 
        {
          var _c2Hero = c2 as HeroAttributeFormat;
          _sumHero.STR += _c2Hero.STR;
          _sumHero.VIT += _c2Hero.VIT;
          _sumHero.INT += _c2Hero.INT;
          _sumHero.DEX += _c2Hero.DEX;
        }
        return _sumHero;
      } 
      else if (c2 is HeroAttributeFormat) 
      {
        var _sumHero = new HeroAttributeFormat (_sumCommon);

        var _c2Hero = c2 as HeroAttributeFormat;
        _sumHero.STR = _c2Hero.STR;
        _sumHero.VIT = _c2Hero.VIT;
        _sumHero.INT = _c2Hero.INT;
        _sumHero.DEX = _c2Hero.DEX;

        if (this is HeroAttributeFormat) 
        {
          var _thisHero = this as HeroAttributeFormat;
          _sumHero.STR += _thisHero.STR;
          _sumHero.VIT += _thisHero.VIT;
          _sumHero.INT += _thisHero.INT;
          _sumHero.DEX += _thisHero.DEX;
        }
        return _sumHero;
      } 

      return _sumCommon;
    }
  }

  [System.Serializable]
  public class HeroSkillFormat : CommonSkillFormat
  {
    [ReadOnly]
    public List<ushort> ChildrenIDList;

    public HeroSkillFormat(HeroSkillSaveDataFormat saveData):
    base(saveData.DBSkillID, saveData.SlotID, saveData.Level)
    {
      var _childrenRows = SkillTableReader.Instance.DefaultCachedList.FindAll (row => {
        return row.ParentID == (short)saveData.DBSkillID;
      });

      if (_childrenRows.Count > 0) 
      {
        this.ChildrenIDList = new List<ushort> ();
        _childrenRows.ForEach (row => {
          this.ChildrenIDList.Add(row.ID);
        });
      }
    }
  }
    
  [System.Serializable]
  public class HeroEquipmentFormat : CommonEquipmentFormat
  {
    public int ReinforcementLevel;
    public int DimensionChipOutput;
    public int DimensionChipOutputProbability;
    public int Weights;
    public List<EquipmentAttribute> EquipmentAttributeBaseList;
    public List<EquipmentAttribute> EquipmentAttributeOffsetList;

    public override CommonAttributeFormat AttributesBuff
    {
      get
      {
        if (this.heroAttributeBuff == null) 
        {
          this.heroAttributeBuff = new HeroAttributeFormat (true);
          CalculateAttributesBuff (this.EquipmentAttributeBaseList);
          CalculateAttributesBuff (this.EquipmentAttributeOffsetList);
        }

        return this.heroAttributeBuff;
      }

    }

    public HeroEquipmentFormat(HeroEquipmentSaveDataFormat equipmentSaveData)
    {
      
      base.DBEquipmentID = equipmentSaveData.DBEquipmentID;
      var _dbEquipment = EquipmentTableReader.Instance.FindDefaultUnique ((ushort)equipmentSaveData.DBEquipmentID);
      base.QualityGrade = _dbEquipment.QualityGrade;
      base.EquipmentType = _dbEquipment.EquipmentType;
      base.TexturePath = _dbEquipment.TexturePath;
      base.TextureIconID = _dbEquipment.TextureIconID;
      base.NameString = new MultiLangString<EquipmentStringsTable> ((ushort)base.DBEquipmentID,EquipmentStringsTableReader.Instance);
      this.ReinforcementLevel = equipmentSaveData.ReinforcementLevel;
      this.DimensionChipOutput = _dbEquipment.DimensionChipOutput;
      this.DimensionChipOutputProbability = _dbEquipment.DimensionChipOutputProbability;
      this.Weights = _dbEquipment.Weights;

      this.EquipmentAttributeBaseList = equipmentSaveData.EquipmentAttributeBaseList.CloneEx ();
      this.EquipmentAttributeOffsetList = equipmentSaveData.EquipmentAttributeOffsetList.CloneEx ();
    }

    public HeroEquipmentFormat(EquipmentTable equipmentTable,List<EquipmentAttribute> equipmentAttributeBaseList,List<EquipmentAttribute> equipmentAttributeOffsetList)
    {

      base.DBEquipmentID = (int)equipmentTable.ID;
      base.QualityGrade = equipmentTable.QualityGrade;
      base.EquipmentType = equipmentTable.EquipmentType;
      base.TexturePath = equipmentTable.TexturePath;
      base.TextureIconID = equipmentTable.TextureIconID;
      base.NameString = new MultiLangString<EquipmentStringsTable> (equipmentTable.ID,EquipmentStringsTableReader.Instance);;
      this.ReinforcementLevel = 0;
      this.DimensionChipOutput = equipmentTable.DimensionChipOutput;
      this.DimensionChipOutputProbability = equipmentTable.DimensionChipOutputProbability;
      this.Weights = equipmentTable.Weights;

      this.EquipmentAttributeBaseList = equipmentAttributeBaseList.CloneEx ();
      this.EquipmentAttributeOffsetList = equipmentAttributeOffsetList.CloneEx ();
    }

    void CalculateAttributesBuff(List<EquipmentAttribute> _equipmentAttribueList)
    {
      // get one equipment Attribute list
      for(int j = 0 ;j<_equipmentAttribueList.Count;j++)
      {
        switch(_equipmentAttribueList[j].AttributeType)
        {
        case ATTRIBUTE_TYPE.STR:
          this.heroAttributeBuff.STR += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.VIT:
          this.heroAttributeBuff.VIT += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.INT:
          this.heroAttributeBuff.INT += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.DEX:
          this.heroAttributeBuff.DEX += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.HPMax:
          this.heroAttributeBuff.HPMax += _equipmentAttribueList [j].Attribute;
          this.heroAttributeBuff.HP += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.RES:
          this.heroAttributeBuff.RES += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.ATK:
          this.heroAttributeBuff.ATK += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.MAG:
          this.heroAttributeBuff.MAG += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.DEF:
          this.heroAttributeBuff.DEF += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.AC:
          this.heroAttributeBuff.AC += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.CRI:
          this.heroAttributeBuff.CRI += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.PEN:
          this.heroAttributeBuff.PEN += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.HIT:
          this.heroAttributeBuff.HIT += _equipmentAttribueList [j].Attribute;
          break;
        case ATTRIBUTE_TYPE.AVD:
          this.heroAttributeBuff.AVD += _equipmentAttribueList [j].Attribute;
          break;
        }
      }
    }

    HeroAttributeFormat heroAttributeBuff;
  }

  [System.Serializable]
  public class HeroAnimationFormat : CommonAnimationFormat
  {
    public HeroAnimationFormat(HeroTable dbData)
    {
      base.TexturePath = dbData.TexturePath;
      base.IconID = dbData.TextureIconID;
      base.IdleID = dbData.TextureIdleID;
      base.AttackID = dbData.TextureAttackID;
      base.GetDamageID = dbData.TextureGetDamageID;
      base.DeadID = dbData.TextureDeadID;
    }
  }
}
