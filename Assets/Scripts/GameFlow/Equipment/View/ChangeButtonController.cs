using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJEnums;
using DataManagement.TableClass;

namespace Equipment.Views{

  public class ChangeButtonController : MonoBehaviour {

    public STRINGS_LABEL EquipmentExchange;
    public STRINGS_LABEL EquipmentBuild;

    // Use this for initialization
    public void Init () 
    {
      buttonText = GetComponentInChildren<Text>();
      equipmentExchange = StringsTableReader.Instance.GetString (EquipmentExchange);
      equipmentBuild = StringsTableReader.Instance.GetString (EquipmentBuild);
    }

    public void EquipmentExistOrNot(bool isExist)
    {
      if (isExist) 
      {
        buttonText.text = equipmentExchange;
      }
      else if(!isExist)
      {
        buttonText.text = equipmentBuild;
      }
    }


    string equipmentExchange;
    string equipmentBuild; 
    Text buttonText;
  }
}
