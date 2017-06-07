using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DataManagement.Common;
using DataManagement;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstStrings;

public class CreateHeroController : MonoBehaviour {

  void Start()
  {
    userSaveData = new UserSaveDataFormat();
    InitUserData ();
  }

  public void SaveHeroData()
  {
    HeroSaveDataFormat _hero0 = new HeroSaveDataFormat ();
    _hero0.DBHeroID = 0;
//    _hero0.DBNameIDArray = 11;
    _hero0.EXP = 0;
    _hero0.Level = 1;
    _hero0.SlotID = 0;
    _hero0.INT = 14;
    _hero0.DEX = 12;
    _hero0.VIT = 12;
    _hero0.STR = 12;
    userSaveData.HeroSaveDataList.Add (_hero0);
    UserSaveDataManager.Instance.UserData = userSaveData;
  }

  void InitUserData()
  {
    userSaveData.UserSaveDataBasic = new UserSaveDataBasicFormat ();
    userSaveData.UserSaveDataBasic.Aura = 0;
    userSaveData.UserSaveDataBasic.DimensionChip = 0;
    userSaveData.HeroSaveDataList = new List<HeroSaveDataFormat> ();
  }

  UserSaveDataFormat userSaveData;
}
