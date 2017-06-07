using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Equipment.Views{

  public class EquipmentInfoView : MonoBehaviour {


    public void Init (string format) 
    {
      selfText = GetComponent<Text> ();
      this.textFormat = format;
    }

    public void DataDisplayImmediately(float data)
    {
      selfText.text = Mathf.FloorToInt (data).ToString ();
    }

    public void DataDisplayWithFormatWhenDifferent(float originalData, float newData)
    {
      if (Mathf.FloorToInt (originalData) != Mathf.FloorToInt (newData)) 
      {
        selfText.text = string.Format (this.textFormat, Mathf.FloorToInt (newData));
      } 
      else 
      {
        selfText.text =  Mathf.FloorToInt (newData).ToString ();
      }

    }

    string textFormat;
    Text selfText;
  }
}
