using UnityEngine;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using Common;
using DataManagement.TableClass.BattleInfo;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;

namespace PJDebug
{
  public class SaveDataDeugger : SingletonObject<SaveDataDeugger> 
  {
    public int OffsetAura = 100;
    public int OffsetDimesionChip = 100;
    public short OffsetLevel = 1;
    public short OffsetArea = 1;
    public short Level = 0;
    public short Area = 0;

    public SLOT_ID HeroSlotID;
    public int OffsetEXP = 100;
    public int OffsetSkillPoint = 100;
    public DebugHeroAttributeFormat OffsetHeroAttribute;

    [ReadOnly]
    public int CurrentLevel;
    [ReadOnly]
    public int CurrentArea;


    void OnEnable()
    {
      BattleSaveDataManager.Instance.BattleSaveDataChangedEvent += UpdateBattleSaveData;
    }

    public void AddAura()
    {
      UserSaveDataManager.Instance.Aura += this.OffsetAura;
    }

    public void AddDimesionChip()
    {
      UserSaveDataManager.Instance.DimensionChip += this.OffsetDimesionChip;
    }

    public void SetLevel()
    {
      var _oldData = BattleSaveDataManager.Instance.BattleSaveData.CloneEx ();
      _oldData.Level = this.Level;

      var _areaCount = BattleAreaTableReader.Instance.GetAreaCount ();
      var _levelCount = BattleAreaLevelTableReader.Instance.GetLevelCount (_oldData.Area);

      _oldData.Level = (short)Mathf.Min (_oldData.Level, _levelCount - 1);

      BattleSaveDataManager.Instance.Overwrite (_oldData);
    }

    public void SetArea()
    {
      var _oldData = BattleSaveDataManager.Instance.BattleSaveData.CloneEx ();
      _oldData.Area = this.Area;

      var _areaCount = BattleAreaTableReader.Instance.GetAreaCount ();
      var _levelCount = BattleAreaLevelTableReader.Instance.GetLevelCount (_oldData.Area);

      _oldData.Area = (short)Mathf.Min (_oldData.Area, _areaCount - 1);

      BattleSaveDataManager.Instance.Overwrite (_oldData);
    }

    public void AddLevel()
    {
      var _oldData = BattleSaveDataManager.Instance.BattleSaveData.CloneEx ();
      _oldData.Level += this.OffsetLevel;

      var _areaCount = BattleAreaTableReader.Instance.GetAreaCount ();
      var _levelCount = BattleAreaLevelTableReader.Instance.GetLevelCount (_oldData.Area);

      if (_oldData.Level > _levelCount - 1) 
      {
        _oldData.Level = 0;
        _oldData.Area += 1;
        if (_oldData.Area > _areaCount - 1) 
        {
          _oldData.Area = (short)(_areaCount - 1);
        }
      }
      BattleSaveDataManager.Instance.Overwrite (_oldData);
    }

    public void AddArea()
    {
      var _oldData = BattleSaveDataManager.Instance.BattleSaveData.CloneEx ();
      _oldData.Area += this.OffsetArea;

      var _areaCount = BattleAreaTableReader.Instance.GetAreaCount ();
      var _levelCount = BattleAreaLevelTableReader.Instance.GetLevelCount (_oldData.Area);

      _oldData.Area = (short)Mathf.Min (_oldData.Area, _areaCount - 1);

      BattleSaveDataManager.Instance.Overwrite (_oldData);
    }

    public void AddActiveHeroEXP()
    {
      HeroDataManager.Instance.ActiveHeroEXPUp (this.OffsetEXP);
    }

    public void AddHeroEXP()
    {
      var _heroSaveData =  HeroSaveDataManager.Instance.GetSaveData ((int)this.HeroSlotID);
      if (_heroSaveData != null) 
      {
        HeroDataFormat _heroData = new HeroDataFormat (_heroSaveData);
        _heroData.EXPUp (this.OffsetEXP, true, this.OnGotEXP);
      }
    }

    public void AddSkillPoint()
    {
      var _heroSaveData =  HeroSaveDataManager.Instance.GetSaveData ((int)this.HeroSlotID);
      if (_heroSaveData != null) 
      {
        HeroSaveDataManager.Instance.Overwrite((int)this.HeroSlotID, _heroSaveData.SkillPoint + this.OffsetSkillPoint);
      }
    }

    public void AddHeroAttribute()
    {
      var _heroSaveData =  HeroSaveDataManager.Instance.GetSaveData ((int)this.HeroSlotID);
      if (_heroSaveData != null) 
      {
        _heroSaveData += this.OffsetHeroAttribute;
        HeroSaveDataManager.Instance.Overwrite (_heroSaveData);
      }
    }

    void OnGotEXP(HeroAttributeFormat heroAttribute)
    {
      Debug.LogWarningFormat ("[Debug] {0} got exp[{1}]",heroAttribute.NameString, this.OffsetEXP);
    }

    void UpdateBattleSaveData(BattleSaveDataFormat data)
    {
      this.CurrentArea = data.Area;
      this.CurrentLevel = data.Level;
    }
  }

  public enum SLOT_ID
  {
    ID_0,
    ID_1,
    ID_2
  }

  [System.Serializable]
  public class DebugHeroAttributeFormat
  {
    public float STR = 100;// Stregth
    public float VIT = 100;// Physique 
    public float INT = 100;// Intelligence
    public float DEX = 100;// Speed

    public float STRUp;
    public float VITUp;
    public float INTUp;
    public float DEXUp;

    public static HeroSaveDataFormat operator +(HeroSaveDataFormat c1, DebugHeroAttributeFormat c2) 
    {
      HeroSaveDataFormat _var = c1.CloneEx();
      _var.STR += c2.STR;
      _var.VIT += c2.VIT;
      _var.INT += c2.INT;
      _var.DEX += c2.DEX;
      _var.STRUp += c2.STRUp;
      _var.VITUp += c2.VITUp;
      _var.INTUp += c2.INTUp;
      _var.DEXUp += c2.DEXUp;

      return _var;
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }
}

