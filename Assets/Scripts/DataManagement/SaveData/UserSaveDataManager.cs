using UnityEngine;
using System.Collections;
using Common;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;

namespace DataManagement.SaveData
{
  public class UserSaveDataManager : Singleton<UserSaveDataManager> 
  {
    public static readonly string STRING_NONE = "NONE";

    public event System.Action<UserSaveDataBasicFormat> UserSaveDataBasicEvent = delegate(UserSaveDataBasicFormat obj) {};


    public bool DataExist
    {
      get
      { 
        return this.dataExist;
      }
    }

    public UserSaveDataFormat UserData
    {
      get
      { 
        return this.userData;
      }
      set
      {
        this.userData = value.CloneEx ();
        WriteToPlayerPrefs ();
      }
    }

    public double TimespanSeconds
    {
      get
      { 
        return this.userData.TimespanSeconds;
      }
      set
      {
        if (!this.dataExist)
          InitUserData (true);

        this.userData.TimespanSeconds= value;
        WriteToPlayerPrefs ();
      }
    }

    public int Aura
    {
      get
      { 
        return this.userData.UserSaveDataBasic.Aura;
      }
      set
      {
        if (!this.dataExist)
          InitUserData (true);
        
        this.userData.UserSaveDataBasic.Aura = value;
        this.UserSaveDataBasicEvent.Invoke (this.userData.UserSaveDataBasic);
        WriteToPlayerPrefs ();
      }
    }

    public int DimensionChip
    {
      get
      { 
        return this.userData.UserSaveDataBasic.DimensionChip;
      }
      set
      {
        if (!this.dataExist)
          InitUserData (true);
        
        this.userData.UserSaveDataBasic.DimensionChip = value;
        this.UserSaveDataBasicEvent.Invoke (this.userData.UserSaveDataBasic);
        WriteToPlayerPrefs ();
      }
    }

    public int SearchProgress
    {
      get
      { 
        return this.userData.UserSaveDataBasic.SearchProgress;
      }
      set
      {
        if (!this.dataExist)
          InitUserData (true);

        this.userData.UserSaveDataBasic.SearchProgress = Mathf.Clamp(value, 0, 100);

        this.UserSaveDataBasicEvent.Invoke (this.userData.UserSaveDataBasic);
        WriteToPlayerPrefs ();
      }
    }
      
    public int LastestHeroSlotID
    {
      get
      { 
        return this.userData.UserSaveDataBasic.LastestHeroSlotID;
      }
      set
      {
        if (!this.dataExist)
          InitUserData (true);

        this.userData.UserSaveDataBasic.LastestHeroSlotID = value;

        this.UserSaveDataBasicEvent.Invoke (this.userData.UserSaveDataBasic);
        WriteToPlayerPrefs ();
      }
    }

    public UserSaveDataBasicFormat UserBasic
    {
      get
      { 
        return this.userData.UserSaveDataBasic;
      }
      set
      {
        if (!this.dataExist)
          InitUserData (true);
        
        this.userData.UserSaveDataBasic = value.CloneEx();
        this.UserSaveDataBasicEvent.Invoke (this.userData.UserSaveDataBasic);
        WriteToPlayerPrefs ();
      }
    }

    public UserSaveDataManager()
    {
      InitUserData ();
    }
      
    public void InitUserData(bool enableCreateEmpty = false)
    {
      this.userData = null;
      this.dataExist = false;

      // Check data file
      string _userJson = PlayerPrefs.GetString(KEY_USER, STRING_NONE);
      if (_userJson == STRING_NONE) 
      {
        if (enableCreateEmpty) 
        {
          this.userData = new UserSaveDataFormat ();
          WriteToPlayerPrefs ();
        } 
        else 
        {
          this.dataExist = false;
        }
        return;
      } 
        
      this.userData = JsonUtility.FromJson<UserSaveDataFormat> (_userJson);
      this.dataExist = true;
    }

    public void Save() 
    {
      // This method will be called by Unity automatically when application exiting
      PlayerPrefs.Save ();
    }

    public void Clear()
    {
      this.userData = null;//new UserSaveDataFormat ();
      PlayerPrefs.DeleteKey(KEY_USER);
      this.dataExist = false;
    }

    public void WriteToPlayerPrefs()
    {
//      if (this.userData == null) 
//      {
//        PlayerPrefs.SetString (KEY_USER, STRING_NONE);
//        this.dataExsit = false;
//        return;
//      }

      PlayerPrefs.SetString (KEY_USER, JsonUtility.ToJson (this.userData));
      this.dataExist = true;
    }

    #region PRIVATE_METHOD

    #endregion


    #region PRIVATE_MEMBER
    static readonly string KEY_USER = "KEY_USER";
    bool dataExist;
    UserSaveDataFormat userData;
    #endregion
  }
}
