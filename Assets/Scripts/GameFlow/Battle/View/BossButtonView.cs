using UnityEngine;
using System.Collections;
using GameFlow.Battle.Controller;
using UnityEngine.UI;
using DataManagement.GameData;
using ConstCollections.PJEnums.Battle;
using Common.UI;
using Common;

namespace GameFlow.Battle.View
{
  public class BossButtonView : ButtonView 
  {
    public BossButtonController Controller;

    void OnEnable()
    {
      if (this.buttonScript.onClick.GetPersistentEventCount () == 0) {
        base.AddOnClick( this.Controller.InitBossBattle);
      }
    }

    public void SetButtonState(bool enable)
    {
      base.buttonScript.interactable = enable;
    }


  }
}
