using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJEnums.Equipment;

namespace Equipment.Views{

  public class EquipmentAttributeNumView : MonoBehaviour {

    // Use this for initialization
    public void Init (string colorString) 
    {
      selfText = GetComponent<Text> ();
      this.textColorFormat = colorString;
    }

    public void ShowAttributeNum(int num,bool displayWithColor = false)
    {
      if (!displayWithColor) 
      {
        selfText.text = AttributeNumFormat (num);
      }
      else if (displayWithColor) 
      {
        selfText.text = string.Format(this.textColorFormat,AttributeNumFormat (num));
      }
    }


    public void Clear()
    {
      selfText.text = "";
    }

    string AttributeNumFormat(int num)
    {
      return string.Format ("+ {0}",num);
    }

    string textColorFormat;
    Text selfText;
  }
}
