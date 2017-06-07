using UnityEngine;
using System.Collections;
using Common.UI;
using Title.Controllers;

namespace Title.Views{

  public class TitleBGButtonView : ButtonView {
    
    public GameObject BGButtonObject;

    protected override void Start () 
    {
      base.Start ();
      if (base.buttonScript.onClick.GetPersistentEventCount () == 0) 
      {
        base.AddOnClick (()=>
          {
            FindObjectOfType<TitleController> ().Close();
            BGButtonObject.gameObject.SetActive(false);
          });
      }
    }

  }
}
