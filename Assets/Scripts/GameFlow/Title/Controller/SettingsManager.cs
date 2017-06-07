using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using DataManagement.SaveData;
using Common;
using DataManagement;


namespace Title.Controllers{

  public class SettingsManager : SingletonObject<SettingsManager>  {

    public GameObject LanguageRootObject;
    public string TitleSceneName;

    [ReadOnly]
    public int WindowCount;

    protected override void Awake()
    {
      base.Awake ();
      this.WindowCount = 0;
    }

    void Start()
    {
      this.systemManager = FindObjectOfType<SystemManager>();
    }

    //FIXME : yangzhi-wang Todo on next Version
    public void GameDescription()
    {
    }

    public void GameStaff()
    {
    }

    public void UploadOnfile()
    {
    }

    public void DownloadOnfile()
    {
    }

    public void Back()
    {
      this.Close ();
    }

    public void ShowLanguageWindow(GameObject prefab)
    {
      Object _gameObj = Instantiate (prefab, LanguageRootObject.transform, false);
      this.systemManager.PushObject (_gameObj, OnPopWindow);
      this.WindowCount++;
    }

    public void DeleteDataAndBackToTitle()
    {
      UserSaveDataManager.Instance.Clear ();
      SceneManager.LoadScene (TitleSceneName);
    }

    public void ToggleActiveGameObject(GameObject gameObject)
    {
      gameObject.SetActive (!gameObject.activeSelf);
    }

    public void Close()
    {
      this.systemManager.PopObject ();
    }

    public void OnPopWindow()
    {
      this.WindowCount--;
    }

    SystemManager systemManager;

  }
}
