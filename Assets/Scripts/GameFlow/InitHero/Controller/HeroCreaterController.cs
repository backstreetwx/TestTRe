using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DataManagement;
using DataManagement.Common;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJConstStrings;
using DataManagement.GameData;
using ConstCollections.PJEnums.Battle;

namespace InitHero.Controllers{
  
  public class HeroCreaterController : MonoBehaviour {

    public PropertyController PropertyController;
    public NameListController NameList;
    public HeroSelecterController HeroSelecter;
    public string NextSceneName;

    public ushort HeroBaseID = 0;

    // Use this for initialization
    void Start () 
    {
      var _heroBase = HeroDataManager.Instance.GetHeroBaseDataByID (HeroBaseID);
      PropertyController.Init (_heroBase);
      NameList.Init ((ushort)_heroBase.NameFormatID);
      userSaveData = new UserSaveDataFormat();
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
    }

    public void SaveHeroAndChangeToBattle()
    {
      bool _temp = UserSaveDataManager.Instance.DataExist;
      if (!_temp) {
        InitUserData ();
      }
      else 
      {
        userSaveData = UserSaveDataManager.Instance.UserData;
      }

      HeroInitAttributeFormat _dataList = PropertyController.HeroAttributes;
      ushort[] nameIdArray = NameList.NameIDArray;
      string nameString = NameList.PresentName;
      int HeroId = HeroSelecter.DBHeroID;
      HeroSaveDataFormat _hero0 = new HeroSaveDataFormat ();

      _hero0.SlotID = 0;
      _hero0.DBHeroID = (ushort)HeroId;
      _hero0.DBNameIDArray = nameIdArray;
      _hero0.NameString = nameString;
      _hero0.Level = _dataList.Level;
      _hero0.EXP = 0;
      _hero0.STR = _dataList.STR;
      _hero0.VIT = _dataList.VIT;
      _hero0.INT = _dataList.INT;
      _hero0.DEX = _dataList.DEX;
      _hero0.STRUp = _dataList.STRUp;
      _hero0.VITUp = _dataList.VITUp;
      _hero0.INTUp = _dataList.INTUp;
      _hero0.DEXUp = _dataList.DEXUp;
      _hero0.SkillPoint = 0;
      _hero0.Active = true;
      var _heroBaseSkillList = HeroDataManager.Instance.GetHeroBaseSkillList (HeroBaseID);
      if (_heroBaseSkillList != null) 
      {
        List<HeroSkillSaveDataFormat> _heroSkillDataList = new List<HeroSkillSaveDataFormat> ();
        foreach (var heroBaseSkill in _heroBaseSkillList) 
        {
          HeroSkillSaveDataFormat _heroSkillSaveData = new HeroSkillSaveDataFormat ();
          _heroSkillSaveData.SlotID = heroBaseSkill.SkillSlotID;
          _heroSkillSaveData.DBSkillID = heroBaseSkill.SKillID;
          _heroSkillDataList.Add (_heroSkillSaveData);
        }
        _hero0.SkillSaveDataList = _heroSkillDataList;
      }

      int? _slotId = globalDataManager.GetNullableValue<int> (NextJumpString.NEXT_HERO_SLOTID,NextJumpString.MEMORY_SPACE);

      if (_slotId != null) 
      {
        _hero0.SlotID = (int)_slotId;
      }
      var _heroSaveDataList = HeroSaveDataManager.Instance.HeroSaveDataList;
      for (int i = 0; i < _heroSaveDataList.Count; i++) 
      {
        if (_heroSaveDataList [i].SlotID != _hero0.SlotID) 
        {
          _heroSaveDataList [i].Active = false;
          HeroSaveDataManager.Instance.Overwrite (_heroSaveDataList [i]);
        }

      }

      if (HeroSaveDataManager.Instance.HeroSaveDataList.Count == 3) 
      {
        //Replace the old one
        //FIXME yangzhi-wang save the history hero data
        HeroSaveDataManager.Instance.Overwrite (_hero0);

      }
      else 
      {
        HeroSaveDataManager.Instance.Add (_hero0);
      }
      globalDataManager.RemoveValue(NextJumpString.NEXT_HERO_SLOTID);
      UserSaveDataManager.Instance.LastestHeroSlotID = _hero0.SlotID;
      SceneManager.LoadScene (NextSceneName);
    }

    void InitUserData()
    {
      userSaveData.UserSaveDataBasic = new UserSaveDataBasicFormat ();
      userSaveData.UserSaveDataBasic.Aura = 0;
      userSaveData.UserSaveDataBasic.DimensionChip = 0;
      userSaveData.UserSaveDataBasic.BattleSaveData = new BattleSaveDataFormat(0,0,false, BATTLE_TYPE.MONSTER_BATTLE);
      userSaveData.HeroSaveDataList = new List<HeroSaveDataFormat> ();
      UserSaveDataManager.Instance.UserData = userSaveData;
    }

    UserSaveDataFormat userSaveData;

    GlobalDataManager globalDataManager;
  }
}
