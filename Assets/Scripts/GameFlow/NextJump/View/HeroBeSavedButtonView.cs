using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NextJump.Controllers;


namespace NextJump.Views{

  public class HeroBeSavedButtonView : MonoBehaviour {

    public void Init()
    {
      selfText = GetComponentInChildren<Text> ();
      selfImage = GetComponent<Image> ();
    }


    public void SetButtonSprite(Sprite sprite)
    {
      selfImage.sprite = sprite;

    }

    Image selfImage;
    Text selfText;

  }
}
