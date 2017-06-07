using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJEnums;
using DataManagement.TableClass;
using Equipment.Controllers;

namespace Equipment.Views{

  public class GetDimensionChipFromEquipmentMessageView : MonoBehaviour {

    public void Init (string format) 
    {
      selfText = GetComponent<Text> ();
      dimensionChipAcquireMessage = format;
    }

    public void AcquireDimensionChipMessage(AcquireDimensionChipDataFormat dimensionChipData)
    {
      selfText.text = string.Format (dimensionChipAcquireMessage,dimensionChipData.NameString.ToString());
    }

    string dimensionChipAcquireMessage;
    Text selfText;

  }
}
