using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.TableClass.Skill;
using ConstCollections.PJEnums.Skill;
using DataManagement.GameData.FormatCollection;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace Skill.Views{

  public class SkillMessageView : MonoBehaviour {

    void OnDisable()
    {
      if (this.multiLang != null)
        this.multiLang.ValuesChangedEvent -= OnMultiLangChanged;
    }

  	// Use this for initialization
    public void Init()
    {
      selfText = GetComponent<Text> ();
    }

    public void SetNewMultiStringAndDisplay(MultiLangString<SkillStringsTable> multiLang)
    {

      if (this.multiLang != null) 
      {
        this.multiLang.ValuesChangedEvent -= OnMultiLangChanged;
      }
      this.multiLang = multiLang;
      this.multiLang.ValuesChangedEvent += OnMultiLangChanged;

      DisplayMessage ();
    }

    public void DisplayMessage()
    {

      selfText.text = this.multiLang.ToString ();
    }

    public void MultiLangParamUpdate(params object[] args)
    {
      if (this.multiLang != null) 
      {
        this.multiLang.UpdateArgs (args);
      }

    }

    void OnMultiLangChanged(string multiLang)
    {
      selfText.text = multiLang;
    }

    Text selfText;
    MultiLangString<SkillStringsTable> multiLang;
  }

}
