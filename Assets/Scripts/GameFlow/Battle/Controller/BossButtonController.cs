using UnityEngine;
using System.Collections;
using DataManagement.GameData;
using UnityEngine.UI;
using Common;
using ConstCollections.PJEnums.Battle;
using UnityEngine.Events;
using GameFlow.Battle.View;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;

namespace GameFlow.Battle.Controller
{
  public class BossButtonController : MonoBehaviour {

    public BossButtonView ButtonView;

    void OnEnable () 
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnSearchProgressChanged;
      OnSearchProgressChanged (UserSaveDataManager.Instance.UserBasic);
    }

    void OnDisable() 
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnSearchProgressChanged;
    }

    public void InitBossBattle()
    {
      this.ButtonView.SetButtonState (false);

      var _col = FindObjectOfType<BattleManager> ();
      _col.StartBossBattle ();
    }

    public void OnSearchProgressChanged(UserSaveDataBasicFormat userSaveDataBasic)
    {
      this.progress = userSaveDataBasic.SearchProgress;

      if (this.progress >= BattleDataManager.Instance.BattleDataCache.SearchPointForBoss) 
      {
        if (BattleDataManager.Instance.BattleType == BATTLE_TYPE.BOSS_BATTLE)
          this.ButtonView.SetButtonState (false);
        else
          this.ButtonView.SetButtonState (true);
      } 
      else 
      {
        this.ButtonView.SetButtonState (false);
      }
    }

    [SerializeField, ReadOnly]
    int progress;
  }
}
