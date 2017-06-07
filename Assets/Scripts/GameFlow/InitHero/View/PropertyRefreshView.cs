using UnityEngine;
using System.Collections;
using Common.UI;
using InitHero.Controllers;

namespace InitHero.Views{

  public class PropertyRefreshView : ButtonView {

    void OnEnable () 
    {
      if(base.buttonScript.onClick.GetPersistentEventCount() == 0)
        base.AddOnClick (FindObjectOfType<PropertyController> ().GetAndShowRandomProperty); 
    }
  }
}
