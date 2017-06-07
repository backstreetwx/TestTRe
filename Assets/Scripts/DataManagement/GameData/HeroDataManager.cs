using UnityEngine;
using System.Collections;
using Common;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using System.Collections.Generic;
using DataManagement.TableClass.Hero;
using ConstCollections.PJEnums.Equipment;
using DataManagement.GameData.FormatCollection.Common;

namespace DataManagement.GameData
{
  public class HeroDataManager : Singleton<HeroDataManager> 
  {

    public event System.Action<List<HeroDataFormat>> HeroDataCacheListChangedEvent = delegate(List<HeroDataFormat> obj) {};
    public event System.Action<int, HeroDataFormat> HeroDataCacheChangedEvent = delegate(int slotID, HeroDataFormat obj) {};

    public List<HeroDataFormat> HeroDataCacheList
    {
      get
      { 
        if (this.heroDataCacheList == null) 
          this.heroDataCacheList = GetHeroDataList ();
        if (!addListener) 
        {
          HeroSaveDataManager.Instance.HeroSaveDataListChangedEvent += HeroCacheUpdate;
          addListener = true;
        }
          
        return this.heroDataCacheList;
      }
    }

    public HeroDataFormat GetHeroData(int slotID)
    {
      if (UserSaveDataManager.Instance == null)
        UserSaveDataManager.Instance.InitUserData ();
      
      HeroSaveDataFormat _heroSaveData = UserSaveDataManager.Instance.UserData.HeroSaveDataList.Find (item => {
        return item.SlotID == slotID;
      });

      if (_heroSaveData == null)
        return null;

      HeroDataFormat _heroData = new HeroDataFormat (_heroSaveData);
      return _heroData;
    }

    public List<HeroDataFormat> GetHeroDataList()
    {
      if (UserSaveDataManager.Instance == null)
        UserSaveDataManager.Instance.InitUserData ();
      
      int _heroCount = UserSaveDataManager.Instance.UserData.HeroSaveDataList.Count;
      if (_heroCount == 0)
        return null;
      
      List<HeroDataFormat> _list = new List<HeroDataFormat> ();
      UserSaveDataManager.Instance.UserData.HeroSaveDataList.ForEach(heroSaveData => {
        HeroDataFormat _hero = new HeroDataFormat(heroSaveData);
        _list.Add(_hero);
      });

      return _list;
    }

    public HeroAttributeFormat GetAttributes(int slotID)
    {
      if (UserSaveDataManager.Instance == null)
        UserSaveDataManager.Instance.InitUserData ();
      
      HeroSaveDataFormat _heroSaveData = UserSaveDataManager.Instance.UserData.HeroSaveDataList.Find (item => {
        return item.SlotID == slotID;
      });

      if (_heroSaveData == null)
        return null;

      HeroAttributeFormat _attribute = new HeroAttributeFormat (_heroSaveData);
      return _attribute;
    }

    public HeroSkillFormat GetSkill(int heroSlotID, int skillSlotID)
    {
      if (UserSaveDataManager.Instance == null)
        UserSaveDataManager.Instance.InitUserData ();
      
      HeroDataFormat _hero = GetHeroData (heroSlotID);

      if (_hero == null)
        return null;

      HeroSkillFormat _skill = _hero.SkillList.Find (item => {
        return item.SlotID == skillSlotID;
      });

      return _skill;
    }

    public HeroEquipmentFormat GetEquipment(int heroSlotID, EQUIPMENT_TYPE equipmentType)
    {
      if (UserSaveDataManager.Instance == null)
        UserSaveDataManager.Instance.InitUserData ();

      HeroDataFormat _hero = GetHeroData (heroSlotID);

      if (_hero == null)
        return null;

      HeroEquipmentFormat _equipment = _hero.EquipmentList.Find (item => {
        return item.EquipmentType == equipmentType;
      });

      return _equipment;
    }

    public HeroBaseTable GetHeroBaseDataByID(ushort id)
    {
      var _heroBase = HeroBaseTableReader.Instance.FindDefaultFirst (id);
      return _heroBase;
    }

    public List<HeroBaseSkillTable> GetHeroBaseSkillList(ushort heroBaseID)
    {
      var _heroBaseSkillList = HeroBaseSkillTableReader.Instance.FindDefaultByHeroBaseID (heroBaseID);
      return _heroBaseSkillList;
    }

    public HeroAttributeFormat CalculateAttributesWithEquipments(HeroAttributeFormat attributesOrigin, List<CommonEquipmentFormat> equipmentList, bool heroBaseAttributesOnly = false)
    {
      if (equipmentList == null || equipmentList.Count == 0)
        return attributesOrigin.CloneEx();

      var _attributesWithEquipments = new HeroAttributeFormat (true);

      HeroAttributeFormat _heroEquiptmentBuff = new HeroAttributeFormat (true);
      equipmentList.ForEach (item => {
        var _attributesBuff =  (item.AttributesBuff as HeroAttributeFormat);
        _heroEquiptmentBuff += _attributesBuff;
      });

      _attributesWithEquipments.AddBaseOnly (attributesOrigin).AddBaseOnly (_heroEquiptmentBuff);
      _attributesWithEquipments.AddLevelOnly (attributesOrigin);
      if (heroBaseAttributesOnly)
        return _attributesWithEquipments;
      
      _attributesWithEquipments.CalculateCommonAttribute ();
      _attributesWithEquipments.AddCommonOnly (_heroEquiptmentBuff);

      return _attributesWithEquipments;
    }

    public void ActiveHeroEXPUp(int exp, bool writeToSavedata = true, System.Action<HeroAttributeFormat> onGotEXP = null, System.Action<HeroAttributeFormat> onLevelUp = null, System.Action<HeroAttributeFormat> onAllLevelUp = null)
    {
      this.HeroDataCacheList.ForEach (heroData => {
        heroData.EXPUp(exp, writeToSavedata, onGotEXP, onGotEXP, onAllLevelUp);
      });
    }

    void HeroCacheUpdate(List<HeroSaveDataFormat> newHeroSaveDataList)
    {
      List<HeroDataFormat> _list = new List<HeroDataFormat> ();
        newHeroSaveDataList.ForEach(heroSaveData => {
          HeroDataFormat _hero = new HeroDataFormat(heroSaveData);
            _list.Add(_hero);
          });
      
      this.heroDataCacheList = _list;

      this.heroDataCacheList.ForEach (cache => {
        this.HeroDataCacheChangedEvent.Invoke (cache.Attributes.SlotID, cache);
      });
      this.HeroDataCacheListChangedEvent.Invoke (this.heroDataCacheList);
    }

    bool addListener = false;
    List<HeroDataFormat> heroDataCacheList;
  }
}
