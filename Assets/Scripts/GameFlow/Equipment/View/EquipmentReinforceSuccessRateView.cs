using UnityEngine;
using System.Collections;
using Common;
using DataManagement.TableClass;
using UnityEngine.UI;

namespace Equipment.Views{

  public class EquipmentReinforceSuccessRateView : MonoBehaviour {

    // Use this for initialization
    public void Init () 
    {
      selfText = GetComponent<Text>();
    }

    public void SetReinforceSuccessMultiString(MultiLangString<StringsTable> multiLang)
    {

      this.multiLang = multiLang;

    }
    public void Display ()
    {
      selfText.text = this.multiLang.ToString ();
    }

    public void SetParam(params object[] args)
    {
      if (this.multiLang != null) 
      {
        this.multiLang.UpdateArgs (args);
      }
    }
    public void Clear()
    {
      selfText.text = "";
    }

    Text selfText;
    MultiLangString<StringsTable> multiLang;
  }
}
