using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;
using DataManagement.SaveData.FormatCollection;
using DataManagement.TableClass.Enemy;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData.FormatCollection.Common;
using DataManagement.GameData.FormatCollection.Common.Skill;
using DataManagement.TableClass;

namespace DataManagement.GameData.FormatCollection
{
  [System.Serializable]
  public class EnemyDataFormat
  {
    public EnemyAttributeFormat Attributes;
    public CommonAttributeFormat AttributesAggregateBuff;
    [ReadOnly]
    public List<EnemySkillFormat> SkillList;
    [ReadOnly]
    public List<EnemyEquipmentFormat> EquipmentList;
    public EnemyAnimationFormat AnimationInfo;

    public EnemyDataFormat(int slotID, MonsterTable dbData, EnemySaveDataFormat enemySaveData = null)
    {
      this.Attributes = new EnemyAttributeFormat (slotID, dbData);

      if (enemySaveData != null) 
      {
        this.AttributesAggregateBuff = enemySaveData.AttributesAggregateBuff.CloneEx ();
      }

      this.AnimationInfo = new EnemyAnimationFormat (dbData);

      var _skillDataList = MonsterSkillTableReader.Instance.FindDefaultByMonsterID (dbData.ID);
      if (_skillDataList != null) 
      {
        this.SkillList = new List<EnemySkillFormat> ();
        _skillDataList.ForEach (_skillData => {
          this.SkillList.Add(new EnemySkillFormat(_skillData));
        });
      }

      this.EquipmentList = new List<EnemyEquipmentFormat> ();
    }

    public EnemyDataFormat(int slotID, BossTable dbData, EnemySaveDataFormat enemySaveData = null)
    {
      this.Attributes = new EnemyAttributeFormat (slotID, dbData);

      if (enemySaveData != null) 
      {
        this.AttributesAggregateBuff = enemySaveData.AttributesAggregateBuff.CloneEx ();
      }

      this.AnimationInfo = new EnemyAnimationFormat (dbData);

      var _skillDataList = BossSkillTableReader.Instance.FindDefaultByMonsterID (dbData.ID);
      if (_skillDataList != null) 
      {
        this.SkillList = new List<EnemySkillFormat> ();
        _skillDataList.ForEach (_skillData => {
          this.SkillList.Add(new EnemySkillFormat(_skillData));
        });
      }

      this.EquipmentList = new List<EnemyEquipmentFormat> ();
    }
  }

  [System.Serializable]
  public class SortEnemyDataBySlotID : IComparer<EnemyDataFormat>
  {
    #region IComparer implementation
    public int Compare (EnemyDataFormat x, EnemyDataFormat y)
    {
      return x.Attributes.SlotID.CompareTo (y.Attributes.SlotID);
    }
    #endregion
  }


  [System.Serializable]
  public class EnemyAttributeFormat : CommonAttributeFormat
  {
    public ENEMY_TYPE EnemeyType;
    public int SlotID;
    public ushort DBEnemyID;
    public int DBNameID;

    public int EXPOutput;
    public int AuraOutput;
    public int DimensionChipOutput;
    public int DimensionChipOutputProbability;

    public IMultiLangString<AbsMultiLanguageTable> NameString
    {
      get
      {
        if (this.nameString != null)
          return this.nameString;

        switch (this.EnemeyType) 
        {
        case ENEMY_TYPE.MONSTER:
          {
            var _strObj =  new MultiLangString<MonsterNameTable> (this.DBEnemyID, MonsterNameTableReader.Instance);
            this.nameString = _strObj;
            break;
          }
        case ENEMY_TYPE.BOSS:
          {
            var _strObj = new MultiLangString<BossNameTable> (this.DBEnemyID, BossNameTableReader.Instance);
            this.nameString = _strObj;
            break;
          }
        default :
          {
            this.nameString = null;
            break;
          }
        }

        return this.nameString;
      }
    }

    public EnemyAttributeFormat(int slotID, MonsterTable dbData, int? currentHP = null)
    {
      this.EnemeyType = ENEMY_TYPE.MONSTER;
      this.SlotID = slotID;

      this.DBEnemyID = dbData.ID;
      this.DBNameID = dbData.MonsterNameID;

      this.EXPOutput = dbData.EXPOutput;
      this.AuraOutput = dbData.AuraOutput;
      this.DimensionChipOutput = dbData.DimensionChipOutput;
      this.DimensionChipOutputProbability = dbData.DimensionChipOutputProbability;

      this.Level = dbData.Level;
      this.HPMax = dbData.HPMax;
      if(currentHP != null)
        this.HP = currentHP.Value;
      else
        this.HP = this.HPMax;
      this.RES = dbData.RES;
      this.ATK = dbData.ATK;
      this.MAG = dbData.MAG;
      this.DEF = dbData.DEF;
      this.AC = dbData.AC;
      this.CRI = dbData.CRI;
      this.PEN = dbData.PEN;
      this.HIT = dbData.HIT;
      this.AVD = dbData.AVD;

    }

    public EnemyAttributeFormat(int slotID, BossTable dbData)
    {
      this.EnemeyType = ENEMY_TYPE.BOSS;
      this.SlotID = slotID;

      this.DBEnemyID = dbData.ID;
      this.DBNameID = dbData.BossNameID;

      this.EXPOutput = dbData.EXPOutput;
      this.AuraOutput = dbData.AuraOutput;
      this.DimensionChipOutput = dbData.DimensionChipOutput;
      this.DimensionChipOutputProbability = dbData.DimensionChipOutputProbability;

      this.Level = dbData.Level;
      this.HPMax = dbData.HPMax;
      this.HP = this.HPMax;
      this.RES = dbData.RES;
      this.ATK = dbData.ATK;
      this.MAG = dbData.MAG;
      this.DEF = dbData.DEF;
      this.AC = dbData.AC;
      this.CRI = dbData.CRI;
      this.PEN = dbData.PEN;
      this.HIT = dbData.HIT;
      this.AVD = dbData.AVD;
 
    }

    IMultiLangString<AbsMultiLanguageTable> nameString;
  }

  [System.Serializable]
  public class EnemySkillFormat : CommonSkillFormat
  {
    public EnemySkillFormat(MonsterSkillTable dbData):
    base(dbData.SKillID,dbData.SkillSlotID,dbData.SkillLevel)
    {
      
    }

    public EnemySkillFormat(BossSkillTable dbData):
    base(dbData.SKillID,dbData.SkillSlotID,dbData.SkillLevel)
    {

    }

  }


  [System.Serializable]
  public class EnemyEquipmentFormat : CommonEquipmentFormat
  {
  }

  [System.Serializable]
  public class EnemyAnimationFormat : CommonAnimationFormat
  {
    public EnemyAnimationFormat(MonsterTable dbData)
    {
      base.TexturePath = dbData.TexturePath;
      base.IconID = dbData.TextureIconID;
      base.IdleID = dbData.TextureIdleID;
      base.AttackID = dbData.TextureAttackID;
      base.GetDamageID = dbData.TextureGetDamageID;
      base.DeadID = dbData.TextureDeadID;
    }

    public EnemyAnimationFormat(BossTable dbData)
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
