using UnityEngine;
using System.Collections;
using Common.UI;
using InitHero.Controllers;

namespace InitHero.Views{

  public class NameRefreshView : ButtonView 
  {
    void OnEnable () 
    {
      if(base.buttonScript.onClick.GetPersistentEventCount() == 0)
        base.AddOnClick (FindObjectOfType<NameListController> ().GetAndShowRandomName); 
  	}
  }

}
