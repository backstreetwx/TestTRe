using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Equipment.Views{

  public class ReinforceButtonView : MonoBehaviour {

    // Use this for initialization
    public void Init () 
    {
      selfImage = GetComponent<Image> ();
      selfButton = GetComponent<Button> ();
    }

    public void ButtonDisplayOrNot(bool mark)
    {
      if (mark) 
      {
        selfButton.gameObject.SetActive(true);
      } 
      else if (!mark) 
      {
        selfButton.gameObject.SetActive(false);
      }
    }


    Image selfImage;
    Button selfButton;
  }
}
