using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection;
using System.Linq;
using ConstCollections.PJEnums.Battle;
using ConstCollections.PJEnums.Character;
using Common;
using ConstCollections.PJEnums.Equipment;
using DataManagement.TableClass.Equipment;
using DataManagement.GameData.FormatCollection.Common;

namespace DataManagement.SaveData.FormatCollection
{
  // User.json
  [System.Serializable]
  public class UserSaveDataFormat
  {

    public UserSaveDataBasicFormat UserSaveDataBasic;
    public List<HeroSaveDataFormat> HeroSaveDataList;
    public double TimespanSeconds;

    public UserSaveDataFormat()
    {
      this.UserSaveDataBasic = new UserSaveDataBasicFormat ();
      this.HeroSaveDataList = new List<HeroSaveDataFormat>();
      this.TimespanSeconds = 0;
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  [System.Serializable]
  public class UserSaveDataBasicFormat
  {
    public int Aura;
    public int DimensionChip;
    public BattleSaveDataFormat BattleSaveData; 
    public int SearchProgress;
    public int LastestHeroSlotID;

    public UserSaveDataBasicFormat()
    {
      this.Aura = 0;
      this.DimensionChip = 0;
      this.BattleSaveData = new BattleSaveDataFormat ();
      this.SearchProgress = 0;
      this.LastestHeroSlotID = 0;
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  [System.Serializable]
  public class BattleSaveDataFormat
  {
    public short Area;
    public short Level;
    public bool EnableBossBattle;
    public BATTLE_TYPE CurrentBattleType;
    public List<EnemySaveDataFormat> BossBattleEnemyList;
    // FIXME: yang-zhang boss data

    public BattleSaveDataFormat()
    {
      this.Area = -1;
      this.Level = -1;
    }

    public BattleSaveDataFormat(short area, short level, bool enableBossBattle, BATTLE_TYPE battleType, List<EnemySaveDataFormat> enemyList = null)
    {
      this.Area = area;
      this.Level = level;
      this.EnableBossBattle = enableBossBattle;
      this.CurrentBattleType = battleType;

      if (enemyList == null)
        return;
      
      switch (this.CurrentBattleType) 
      {
      case BATTLE_TYPE.BOSS_BATTLE:
        this.BossBattleEnemyList = enemyList.CloneEx();
        break;
      default:
        break;
      }
    }
  }

  [System.Serializable]
  public class EnemySaveDataFormat
  {
    public ushort EnemyID;
    public ENEMY_TYPE Type;
//    public int HP;
    public CommonAttributeFormat AttributesAggregateBuff;

    public EnemySaveDataFormat(EnemyAttributeFormat attributes, CommonAttributeFormat attributesAggregateBuff)
    {
      this.EnemyID = attributes.DBEnemyID;
      this.Type = attributes.EnemeyType;
      this.AttributesAggregateBuff = attributesAggregateBuff.CloneEx ();
//      this.HP = attributes.HP;
    }
  }

  [System.Serializable]
  public class HeroSaveDataFormat
  {
    //public HeroBasicAttributeFormat HeroBasicAttributes;
    public int SlotID;
    public ushort DBHeroID;
    public ushort[] DBNameIDArray;
    public string NameString;
    public int Level;
    public int EXP;
    public float STR;
    public float VIT;
    public float INT;
    public float DEX;
    public float STRUp;
    public float VITUp;
    public float INTUp;
    public float DEXUp;
    public int SkillPoint;
    public bool Active;
    public List<HeroSkillSaveDataFormat> SkillSaveDataList;
    public List<HeroEquipmentSaveDataFormat> EquipmentSaveDataList;

    public HeroSaveDataFormat()
    {
      //this.HeroBasicAttributes = new HeroBasicAttributeFormat ();
      this.SlotID = -1;
      this.DBHeroID = 0;
      this.DBNameIDArray = new ushort[3];
      this.NameString = "";
      this.Level = 0;
      this.EXP = -1;
      this.STR = -1;
      this.VIT = -1;
      this.INT = -1;
      this.DEX = -1;
      this.SkillPoint = -1;
      this.Active = false;
      this.SkillSaveDataList = new List<HeroSkillSaveDataFormat>();
      this.EquipmentSaveDataList = new List<HeroEquipmentSaveDataFormat>();
    }

    public HeroSaveDataFormat(HeroDataFormat heroGameData)
    {
      this.SlotID = heroGameData.Attributes.SlotID;
      this.DBHeroID = heroGameData.Attributes.DBHeroID;
      this.DBNameIDArray = heroGameData.Attributes.DBNameIDArray;
      this.NameString = heroGameData.Attributes.NameString;
      this.Level = heroGameData.Attributes.Level;

      this.EXP = heroGameData.Attributes.EXP;

      this.STR = heroGameData.Attributes.STR;
      this.VIT = heroGameData.Attributes.VIT;
      this.INT = heroGameData.Attributes.INT;
      this.DEX = heroGameData.Attributes.DEX;

      this.STRUp = heroGameData.Attributes.STRUp;
      this.VITUp = heroGameData.Attributes.VITUp;
      this.INTUp = heroGameData.Attributes.INTUp;
      this.DEXUp = heroGameData.Attributes.DEXUp;

      this.SkillPoint = heroGameData.Attributes.SkillPoint;
      this.Active = heroGameData.Attributes.Active;
      this.SkillSaveDataList = new List<HeroSkillSaveDataFormat>();
      if (heroGameData.SkillList != null && heroGameData.SkillList.Count > 0) 
      {
        heroGameData.SkillList.ForEach (skill => {
          HeroSkillSaveDataFormat _skillSaveData = new HeroSkillSaveDataFormat(skill);
          this.SkillSaveDataList.Add(_skillSaveData);
        });
      }

      this.EquipmentSaveDataList = new List<HeroEquipmentSaveDataFormat>();
      if (heroGameData.EquipmentList != null && heroGameData.EquipmentList.Count > 0) 
      {
        heroGameData.EquipmentList.ForEach (equip => {
          HeroEquipmentSaveDataFormat _equipSaveData = new HeroEquipmentSaveDataFormat(equip);
          this.EquipmentSaveDataList.Add(_equipSaveData);
        });
      }
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  [System.Serializable]
  public class HeroSkillSaveDataFormat
  {
    public int SlotID;
    public ushort DBSkillID;
    public int Level;

    public HeroSkillSaveDataFormat()
    {
      this.SlotID = -1;
      this.DBSkillID = 0;
      this.Level = 0;
    }

    public HeroSkillSaveDataFormat(int slotID,ushort dBSkillID, int level)
    {
      this.SlotID = slotID;
      this.DBSkillID = dBSkillID;
      this.Level = level;
    }

    public HeroSkillSaveDataFormat(HeroSkillFormat skillGameData)
    {
      this.SlotID = skillGameData.SlotID;
      this.DBSkillID = skillGameData.DBSkillID;
      this.Level = skillGameData.Level;
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  [System.Serializable]
  public class HeroEquipmentSaveDataFormat
  {
    
    public int DBEquipmentID;
    public int ReinforcementLevel;
    public List<EquipmentAttribute> EquipmentAttributeBaseList;
    public List<EquipmentAttribute> EquipmentAttributeOffsetList;

    public HeroEquipmentSaveDataFormat()
    {
      this.DBEquipmentID = -1;
      this.ReinforcementLevel = -1;
      this.EquipmentAttributeBaseList = new List<EquipmentAttribute> ();
      this.EquipmentAttributeOffsetList = new List<EquipmentAttribute> ();
    }

    public HeroEquipmentSaveDataFormat(ushort dBEquipmentID, int reinforcementLevel,List<EquipmentAttribute> equipmentBaseAttributes,List<EquipmentAttribute> equipmentOffsetAttributes)
    {
      this.DBEquipmentID = dBEquipmentID;
      this.ReinforcementLevel = reinforcementLevel;
      if(equipmentBaseAttributes != null)
        this.EquipmentAttributeBaseList = equipmentBaseAttributes.CloneEx();
      if(equipmentOffsetAttributes != null)
        this.EquipmentAttributeOffsetList = equipmentOffsetAttributes.CloneEx();
    }


    public HeroEquipmentSaveDataFormat(HeroEquipmentFormat equipmentGameData)
    {
      this.DBEquipmentID = equipmentGameData.DBEquipmentID;
      this.ReinforcementLevel = equipmentGameData.ReinforcementLevel;
      this.EquipmentAttributeBaseList = equipmentGameData.EquipmentAttributeBaseList.CloneEx();
      this.EquipmentAttributeOffsetList = equipmentGameData.EquipmentAttributeOffsetList.CloneEx();
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  [System.Serializable]
  public class EquipmentAttribute
  {
    public ATTRIBUTE_TYPE AttributeType;
    public int Attribute;

    public EquipmentAttribute()
    {
      this.AttributeType = ATTRIBUTE_TYPE.NONE;
      this.Attribute = 0;
    }

    public EquipmentAttribute(ATTRIBUTE_TYPE attributeType,int attribute)
    {
      this.AttributeType = attributeType;
      this.Attribute = attribute;
    }
  }

}
