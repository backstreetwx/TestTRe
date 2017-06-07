using UnityEngine;
using System.Collections;
using Common.UI;
using GameFlow.Battle.Controller;

namespace BattleCharacterInfo.Views{

  public class CharacterInfoBGButtonView : ButtonView {

    void OnEnable()
    {
      if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
      {
        base.AddOnClick (FindObjectOfType<BattleBottomManager> ().Close); 
      }
    }

  }
}
