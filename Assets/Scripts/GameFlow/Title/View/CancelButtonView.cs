using UnityEngine;
using System.Collections;
using Common.UI;
using Title.Controllers;

namespace Title.Views{

  public class CancelButtonView : ButtonView {

   
    void OnEnable () 
    {
      if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
      {
        if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
        {
          base.AddOnClick (FindObjectOfType<TitleController> ().Close); 
        }
      }
    }
  }
}
