using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.TableClass;
using ConstCollections.PJEnums;

namespace HeroInfo.Views{

  public class HeroLevelView : MonoBehaviour {

    public STRINGS_LABEL Label;

    public void Init () 
    {
      selfText = GetComponent<Text> ();
      this.systemLanguage = ConfigDataManager.Instance.UserLanguage;
      format = StringsTableReader.Instance.GetString (Label,this.systemLanguage);
    }
  	
    public void HeroLevelDisplay(int level)
    {
      selfText.text = string.Format (format,level);
    }
  	
    string format;
    Text selfText;
    SystemLanguage systemLanguage;
  }
}
