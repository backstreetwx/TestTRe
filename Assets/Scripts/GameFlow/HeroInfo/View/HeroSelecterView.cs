using UnityEngine;
using Common.UI;
using System.Collections;
using HeroInfo.Controllers;

namespace HeroInfo.Views{

  public class HeroSelecterView : ButtonView {

    void OnEnable () 
    {
      if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
      {
        base.AddOnClick (FindObjectOfType<HeroInfoController> ().NextJump); 
      }
    }
  }

}
