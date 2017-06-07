using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.TableClass.Equipment;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums;
using DataManagement.TableClass;
using Common;
using ConstCollections.PJConstOthers;

namespace Equipment.Views{

  public class EquipmentReinforceLevelSelectButtonView : MonoBehaviour {


    // Use this for initialization
    public void Init (MultiLangString<StringsTable> multi) 
    {
      selfButton = GetComponent<Button> ();
      selfImage = GetComponent<Image> ();
      buttonText = GetComponentInChildren<Text> ();

      this.multiLangString = multi;
    }

    public void Display()
    {
      if (this.multiLangString != null) 
      {
        buttonText.text = this.multiLangString.ToString ();
      }
    }

    public void ButtonClickAbleOrNot(bool clickAble)
    {
      selfButton.interactable = clickAble;
    }

    public void SetSprite(Sprite sprite)
    {
      selfImage.sprite = sprite;
    }

    MultiLangString<StringsTable> multiLangString;
    Text buttonText;
    Image selfImage;
    Button selfButton;
  }
}
