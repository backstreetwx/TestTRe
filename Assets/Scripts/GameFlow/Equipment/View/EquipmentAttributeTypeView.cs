using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJEnums.Equipment;
using ConstCollections.PJEnums;
using ConstCollections.PJConstStrings;
using DataManagement.TableClass;
using Common;

namespace Equipment.Views{

  public class EquipmentAttributeTypeView : MonoBehaviour {


    // Use this for initialization
    public void Init () 
    {
      selfText = GetComponent<Text> ();
    }

    public void ShowEquipmentAttributeType(MultiLangString<StringsTable> multi)
    {
      this.multLang = multi;
    }

    public void Display ()
    {
      selfText.text = this.multLang.ToString ();;
    }

    public void Clear()
    {
      selfText.text = "";
    }

    MultiLangString<StringsTable> multLang;
    Text selfText;
  }
}
