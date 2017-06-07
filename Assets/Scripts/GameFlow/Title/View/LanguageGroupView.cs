using UnityEngine;
using System.Collections;
using DataManagement;
using Common.UI;
using DataManagement.SaveData;

namespace Title.Views{

  public class LanguageGroupView : ButtonGroupView {

    public void SetLanguage(LanguageButtonView activeView)
    {
      ConfigDataManager.Instance.UserLanguage = activeView.ButtonLanguage;
      this.SelectExclusive (activeView);
    }
  }
}
