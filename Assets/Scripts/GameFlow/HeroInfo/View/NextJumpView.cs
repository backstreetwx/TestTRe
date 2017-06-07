using UnityEngine;
using System.Collections;
using Common.UI;
using HeroInfo.Controllers;

namespace HeroInfo.Views{

  public class NextJumpView : ButtonView {

    void OnEnable () 
    {
      if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
      {
        base.AddOnClick (FindObjectOfType<HeroInfoController> ().NextJump); 
      }
    }
  }
}
