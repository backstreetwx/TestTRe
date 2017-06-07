using UnityEngine;
using System.Collections;
using Common.UI;
using Title.Controllers;


namespace Title.Views{

  public class ConfirmButtonView : ButtonView {

    void OnEnable () 
    {
      if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
      {
        base.AddOnClick (FindObjectOfType<TitleController> ().CreateCharacterConfirmed); 
      }
    }

  }
}
