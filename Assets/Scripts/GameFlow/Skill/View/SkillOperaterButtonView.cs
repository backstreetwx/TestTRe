using UnityEngine;
using System.Collections;
using Common.UI;
using Skill.Controllers;

namespace Skill.Views{

  public class SkillOperaterButtonView : ButtonView {

    void OnEnable () 
    {
      if(base.buttonScript.onClick.GetPersistentEventCount() == 0)
      {
        base.AddOnClick (()=>
          {
            FindObjectOfType<SkillController> ().SkillOperation();
          });
      }
    }
  }

}
