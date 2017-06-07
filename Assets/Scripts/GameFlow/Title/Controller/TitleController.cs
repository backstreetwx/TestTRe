using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Title.Views;
using DataManagement.SaveData;
using DataManagement;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstStrings;
using Common;

namespace Title.Controllers{

  public class TitleController : MonoBehaviour {

    public ButtonRelatedWithDataView ButtonContinue;
    public GameObject CreateCharacterConfirmationRootObject;
    public GameObject BGObject;
    public GameObject CreateCharacterConfirmationPrefab;

    public GameObject SettingsRootObject;

    public string CreateCharacterSceneName;
    public string BattleSceneName;


    [ReadOnly]
    public int WindowCount;

    void Awake()
    {
      
      this.WindowCount = 0;
    }

    // Use this for initialization
    void Start () 
    {
      
      this.systemManager = FindObjectOfType<SystemManager>();
      ButtonContinue.Init ();
      bool _whetherDataExist = UserSaveDataManager.Instance.DataExist;
      ShowButtonOrNot (_whetherDataExist);
    }

    public void GameContinue()
    {
      SceneManager.LoadScene (BattleSceneName);
    }

    public void CreateCharacter()
    {
      bool _whetherDataExist = UserSaveDataManager.Instance.DataExist;
      if (_whetherDataExist) 
      {
        ShowCreateCharacterConfirmationWindow ();
        this.BGObject.SetActive (true);
      }
      else if (!_whetherDataExist) 
      {
        CreateCharacterConfirmed ();
      }
    }

    public void Close()
    {
      this.systemManager.PopObject ();
    }

    public void ShowCreateCharacterConfirmationWindow()
    {
      Object _gameObj = Instantiate (CreateCharacterConfirmationPrefab, CreateCharacterConfirmationRootObject.transform, false);
      this.systemManager.PushObject (_gameObj, OnPopWindow);
      this.WindowCount++;
    }

    public void ShowSettingsWindow(GameObject prefab)
    {
      Object _gameObj = Instantiate (prefab, SettingsRootObject.transform, false);
      this.systemManager.PushObject (_gameObj, OnPopWindow);
      this.WindowCount++;
    }

    public void OnPopWindow()
    {
      this.WindowCount--;
      if (this.WindowCount == 0) 
      {
        if(this.BGObject != null)
          this.BGObject.SetActive (false);
      }
    }
     
    public void CreateCharacterConfirmed()
    {
      UserSaveDataManager.Instance.Clear ();
      SceneManager.LoadScene (CreateCharacterSceneName);
    }

    public void ShowButtonOrNot(bool whetherDataExist)
    {
      if (whetherDataExist) 
      {
        ButtonContinue.SetVisible ();
      } 
      else if (!whetherDataExist)
      {
        ButtonContinue.SetInvisible ();
      }
    }

    SystemManager systemManager;
  }

}
