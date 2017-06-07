using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Equipment.Views{

  public class EquipmentBuildButtonView : MonoBehaviour {

    // Use this for initialization
    public void Init () 
    {
      selfButton = GetComponent<Button> ();
    }

    public void BuildButtonClickAbleOrNot(bool clickAble)
    {
      if (clickAble) 
      {
        selfButton.interactable = true;
      }
      else if(!clickAble)
      {
        selfButton.interactable = false;
      }
    }

    Button selfButton; 
  } 
}
