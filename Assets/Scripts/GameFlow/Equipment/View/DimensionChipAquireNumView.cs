using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ConstCollections.PJEnums;
using DataManagement.TableClass;
using Equipment.Controllers;

namespace Equipment.Views{

  public class DimensionChipAquireNumView : MonoBehaviour {

    public void Init (string dimensionChipAcquireNumberString) 
    {
      selfText = GetComponent<Text> ();
      dimensionChipAcquireNumber = dimensionChipAcquireNumberString;
    }

    public void AcquireDimensionChipNumber(AcquireDimensionChipDataFormat dimensionChipData)
    {
      selfText.text = string.Format (dimensionChipAcquireNumber,dimensionChipData.DimensionChipNum);
    }

    string dimensionChipAcquireNumber;
    Text selfText;
  }
}
