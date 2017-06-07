using UnityEngine;
using System.Collections;
using Common;
using DataManagement.Common;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using System.Collections.Generic;

namespace DataManagement.SaveData
{
  public class HeroSaveDataManager : Singleton<HeroSaveDataManager> 
  {

    public event System.Action<List<HeroSaveDataFormat>> HeroSaveDataListChangedEvent = delegate(List<HeroSaveDataFormat> obj) {};

    public bool DataExist
    {
      get
      { 
        if (!UserSaveDataManager.Instance.DataExist) 
          return false;
        
        return UserSaveDataManager.Instance.UserData.HeroSaveDataList != null &&
          UserSaveDataManager.Instance.UserData.HeroSaveDataList.Count > 0;
      }
    }

    public List<HeroSaveDataFormat> HeroSaveDataList
    {
      get
      { 
        if (!UserSaveDataManager.Instance.DataExist)
          return null;

        return UserSaveDataManager.Instance.UserData.HeroSaveDataList;
      }
    }

    public HeroSaveDataManager()
    {
      if (!UserSaveDataManager.Instance.DataExist)
      {
        UserSaveDataManager.Instance.InitUserData ();
      }
    }

    public void Save() 
    {
      UserSaveDataManager.Instance.WriteToPlayerPrefs ();
      // This method will be called by Unity automatically when application exiting
      PlayerPrefs.Save ();
    }

    public void Add(HeroDataFormat heroGameData, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
      {
        UserSaveDataManager.Instance.InitUserData (true);
      }

      HeroSaveDataFormat _heroSaveData = new HeroSaveDataFormat (heroGameData);
      Add (_heroSaveData, write);
    }

    public void Add(HeroSaveDataFormat heroSaveData, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
      {
        UserSaveDataManager.Instance.InitUserData (true);
      }
      
      UserSaveDataManager.Instance.UserData.HeroSaveDataList.Add (heroSaveData.CloneEx());
      if(write)
        UserSaveDataManager.Instance.WriteToPlayerPrefs ();

      this.HeroSaveDataListChangedEvent.Invoke (UserSaveDataManager.Instance.UserData.HeroSaveDataList);
    }

    public void Add(HeroDataFormat[] heroGameDataList, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
      {
        UserSaveDataManager.Instance.InitUserData (true);
      }

      HeroSaveDataFormat[] _heroSaveDataList = new HeroSaveDataFormat[heroGameDataList.Length];
      for (int i = 0; i < _heroSaveDataList.Length; i++) 
      {
        _heroSaveDataList [i] = new HeroSaveDataFormat (heroGameDataList [i]);
      }

      Add (_heroSaveDataList, write);
    }

    public void Add(HeroSaveDataFormat[] heroSaveDataList, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
      {
        UserSaveDataManager.Instance.InitUserData (true);
      }

      UserSaveDataManager.Instance.UserData.HeroSaveDataList.AddRange (heroSaveDataList.CloneEx());
      if(write)
        UserSaveDataManager.Instance.WriteToPlayerPrefs ();
      
      this.HeroSaveDataListChangedEvent.Invoke (UserSaveDataManager.Instance.UserData.HeroSaveDataList);
    }


    public bool Overwrite(int slotID, int skillPoint, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
        return false;

      if (UserSaveDataManager.Instance.UserData.HeroSaveDataList == null ||
        UserSaveDataManager.Instance.UserData.HeroSaveDataList.Count == 0)
        return false;

      int _targetIndex = UserSaveDataManager.Instance.UserData.HeroSaveDataList.FindIndex (item => {
        if(item.SlotID == slotID)
          return true;
        return false;
      });

      if (_targetIndex == -1) 
      {
        Debug.LogErrorFormat ("slotID : {0} is not exsit!", slotID);
        return false;
      }

      UserSaveDataManager.Instance.UserData.HeroSaveDataList [_targetIndex].SkillPoint = skillPoint;
      if(write)
        UserSaveDataManager.Instance.WriteToPlayerPrefs ();

      this.HeroSaveDataListChangedEvent.Invoke (UserSaveDataManager.Instance.UserData.HeroSaveDataList);
      return true;
    }


    public bool Overwrite(HeroDataFormat heroGameData, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
        return false;

      HeroSaveDataFormat _heroSaveData = new HeroSaveDataFormat (heroGameData);
      return Overwrite (_heroSaveData, write);
    }

    public bool Overwrite(HeroSaveDataFormat heroSaveData, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
        return false;

      if (UserSaveDataManager.Instance.UserData.HeroSaveDataList == null ||
         UserSaveDataManager.Instance.UserData.HeroSaveDataList.Count == 0)
        return false;

      int _targetIndex = UserSaveDataManager.Instance.UserData.HeroSaveDataList.FindIndex (item => {
        if(item.SlotID == heroSaveData.SlotID)
          return true;
        return false;
      });

      if (_targetIndex == -1) 
      {
        Debug.LogErrorFormat ("slotID : {0} is not exsit!", heroSaveData.SlotID);
        return false;
      }

      UserSaveDataManager.Instance.UserData.HeroSaveDataList [_targetIndex] = heroSaveData.CloneEx();
      if(write)
        UserSaveDataManager.Instance.WriteToPlayerPrefs ();

      this.HeroSaveDataListChangedEvent.Invoke (UserSaveDataManager.Instance.UserData.HeroSaveDataList);
      return true;
    }

    public bool Overwrite(HeroDataFormat[] heroGameDataList, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
        return false;

      HeroSaveDataFormat[] _heroSaveDataList = new HeroSaveDataFormat[heroGameDataList.Length];
      for (int i = 0; i < _heroSaveDataList.Length; i++) 
      {
        _heroSaveDataList [i] = new HeroSaveDataFormat (heroGameDataList [i]);
      }
        
      return Overwrite(_heroSaveDataList, write);
    }

    public bool Overwrite(HeroSaveDataFormat[] heroSaveDataList, bool write = true)
    {
      if (!UserSaveDataManager.Instance.DataExist)
        return false;
      
      for (int i = 0; i < heroSaveDataList.Length; i++) 
      {
        if (!Overwrite (heroSaveDataList [i], false))
          return false;
      }

      if(write)
        UserSaveDataManager.Instance.WriteToPlayerPrefs ();

      this.HeroSaveDataListChangedEvent.Invoke (UserSaveDataManager.Instance.UserData.HeroSaveDataList);
      return true;
    }
      
    public HeroSaveDataFormat GetSaveData(int slotID)
    {
      if (!UserSaveDataManager.Instance.DataExist)
        return null;

      var _target = UserSaveDataManager.Instance.UserData.HeroSaveDataList.Find (data => {
        return data.SlotID == slotID;
      });

      if (_target == null) 
      {
        Debug.LogErrorFormat ("slotID : {0} is not exsit!", slotID);
      }
      return _target.CloneEx();
    }

  }
}
