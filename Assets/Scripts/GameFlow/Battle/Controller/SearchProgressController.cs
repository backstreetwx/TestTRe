using UnityEngine;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using UnityEngine.Events;
using DataManagement.GameData;
using UnityEngine.UI;
using Common.Controller;
using GameFlow.Battle.Common.View;
using ConstCollections.PJEnums.Battle;

namespace GameFlow.Battle.Controller
{
  public class SearchProgressController : MonoBehaviour
  {
    public CommonTextController SearchProgressTextController;
    public CommonTextController BossTextController;
    public SearchProgressView SearchProgressViewScript;

    void OnEnable () 
    {
      // Add listener to UserData
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnSearchProgressChanged;
      SearchProgressTextController.MultiLangString.BindArgs (UserSaveDataManager.Instance.SearchProgress);
    }
      
    void Start()
    {
      this.SearchProgressViewScript.SearchProgress = UserSaveDataManager.Instance.SearchProgress;
      this.SetText (UserSaveDataManager.Instance.SearchProgress);
    }

    void OnDisable() 
    {
      // Remove listener from UserData
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnSearchProgressChanged;
    }

    void SetText(int searchProgress = 0)
    {
      if (searchProgress >= BattleDataManager.Instance.BattleDataCache.SearchPointForBoss) 
      {
        this.SearchProgressTextController.gameObject.SetActive (false);
        this.BossTextController.gameObject.SetActive (true);
        return;
      }

      this.SearchProgressTextController.gameObject.SetActive (true);
      this.SearchProgressTextController.MultiLangString.UpdateArgs(searchProgress);

      this.BossTextController.gameObject.SetActive (false);
    }

    void OnSearchProgressChanged(UserSaveDataBasicFormat userSaveDataBasic)
    {
      this.SearchProgressViewScript.SearchProgress = userSaveDataBasic.SearchProgress;
      this.SetText (userSaveDataBasic.SearchProgress);
    }
  }
}
