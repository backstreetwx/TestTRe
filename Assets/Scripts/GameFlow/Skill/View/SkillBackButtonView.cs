using UnityEngine;
using Common.UI;
using System.Collections;
using NextJump.Controllers;


namespace Skill.Views{

  public class SkillBackButtonView : ButtonView {

    void OnEnable () 
    {
      if(base.buttonScript.onClick.GetPersistentEventCount() == 0)
      {
        base.AddOnClick (FindObjectOfType<WindowPopManager> ().Close);
      }
    }
  	
  }
}
