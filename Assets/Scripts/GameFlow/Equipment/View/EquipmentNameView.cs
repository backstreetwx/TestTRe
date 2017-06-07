using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Common;
using DataManagement.TableClass.Equipment;
using DataManagement.SaveData;
using ConstCollections.PJEnums;
using DataManagement.TableClass;

namespace Equipment.Views{ 

  public class EquipmentNameView : MonoBehaviour {

    public STRINGS_LABEL NoEquipment;

    void OnEnable()
    {
      ConfigDataManager.Instance.UserLanguageChangedEvents += OnLanguageChanged;
    }

    void OnDisable()
    {
      ConfigDataManager.Instance.UserLanguageChangedEvents -= OnLanguageChanged;
    }


    public void Init (string noEquipmentString) 
    {
      selfText = GetComponent<Text> ();
      noEquipment = noEquipmentString;
    }

    public void ShowEquipmentName(MultiLangString<EquipmentStringsTable> multiLang)
    {

      this.muiltiLang = multiLang;
      Display ();
    }

    public void NoEquipmentMessage()
    {
      selfText.text = noEquipment;
    }

    void Display()
    {
      selfText.text = this.muiltiLang.ToString ();
    }

    void OnLanguageChanged()
    {
      Display ();
    }
    string noEquipment;
    Text selfText;
    MultiLangString<EquipmentStringsTable> muiltiLang;
  }
}
