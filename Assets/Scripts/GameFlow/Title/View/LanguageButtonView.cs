using UnityEngine;
using System.Collections;
using Common.UI;
using DataManagement.SaveData;

namespace Title.Views{

  public class LanguageButtonView : ButtonView {

    public SystemLanguage ButtonLanguage;

    // Use this for initialization
    protected override void Start () 
    {
      base.Start ();

      if (this.languageGroupView == null)
        this.languageGroupView = FindObjectOfType<LanguageGroupView> ();

      if(base.buttonScript.onClick.GetPersistentEventCount() == 0)
        base.AddOnClick (() => 
          {
            this.languageGroupView .SetLanguage(this);
          });

      if (ButtonLanguage == ConfigDataManager.Instance.UserLanguage) 
        base.ActivedImage.enabled = true;
      else
        base.ActivedImage.enabled = false;
    }

    LanguageGroupView languageGroupView;

  }
}
