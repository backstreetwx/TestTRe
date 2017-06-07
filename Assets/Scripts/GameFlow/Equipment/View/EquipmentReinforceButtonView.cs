using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Equipment.Views{

  public class EquipmentReinforceButtonView : MonoBehaviour {

    public void Init () 
    {
      reinforceButton = GetComponent<Button> ();
    }

    public void ReinforceButtonClickAbleOrNot(bool clickAble)
    {
      reinforceButton.interactable = clickAble;
    }

    Button reinforceButton;
  }
}
