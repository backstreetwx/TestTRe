using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJEnums;
using DataManagement.TableClass;
using ConstCollections.PJEnums.Equipment;
using ConstCollections.PJConstStrings;

namespace Equipment.Views{

  public class EquipmentDestroyDescriptionView : MonoBehaviour {

    public void Init (string descriptionString) 
    {
      selfText = GetComponent<Text> ();

      selfText.text = descriptionString;
    }
  	
    Text selfText;
  }
}
