using UnityEngine;
using System.Collections;
using Common.UI;

namespace Equipment.Views{

  public class GetDimensionChipFromEquipmentButtonView : ButtonView {

    void OnEnable () 
    {
      if(base.buttonScript.onClick.GetPersistentEventCount() == 0)
      {
        base.AddOnClick (FindObjectOfType<PopEquipmentBuildManager> ().Close);
      }
    }
  }
}
