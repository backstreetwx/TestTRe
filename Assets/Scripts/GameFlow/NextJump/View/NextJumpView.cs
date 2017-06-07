using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace NextJump.Views{

  public class NextJumpView : MonoBehaviour {

    public void Init () 
    {
      selfButton = GetComponent<Button> ();
      SetButtonClickableOrNot (false);
    }


    public void SetButtonClickableOrNot(bool temp)
    {
      if (temp) 
      {
        selfButton.interactable = true;
      } 
      else
      {
        selfButton.interactable = false;
      }
    }

    Button selfButton;

  }
}
