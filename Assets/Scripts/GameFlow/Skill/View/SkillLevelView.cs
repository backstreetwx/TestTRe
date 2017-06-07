using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.TableClass;
using ConstCollections.PJEnums;

namespace Skill.Views{

  public class SkillLevelView : MonoBehaviour {

    //FIXME : read from DataManager
    public int SkillMaxLevel = 10;
    public STRINGS_LABEL LevelMarkingLabel;
    public STRINGS_LABEL LevelMaxLabel;
    public STRINGS_LABEL SkillUnacquired;
    // Use this for initialization
    public void Init () 
    {
      selfText = GetComponent<Text> ();
      this.systemLanguage = ConfigDataManager.Instance.UserLanguage;
      format = StringsTableReader.Instance.GetString (LevelMarkingLabel,this.systemLanguage);
      levelMax = StringsTableReader.Instance.GetString (LevelMaxLabel,this.systemLanguage);
      skillUnacquired = StringsTableReader.Instance.GetString (SkillUnacquired);
    }

    public void SkillLevelDisplay(int level)
    {
      if (level == 0) 
      {
        selfText.text = skillUnacquired;
      }
      else if (level < SkillMaxLevel) 
      {
        selfText.text = string.Format (format, level);
      } 
      else 
      {
        selfText.text = levelMax;
      }

    }
    string format;
    string levelMax;
    string skillUnacquired;
    Text selfText;
    SystemLanguage systemLanguage;
  }
}
