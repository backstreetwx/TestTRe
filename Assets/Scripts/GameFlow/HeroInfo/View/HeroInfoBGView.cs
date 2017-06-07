using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HeroInfo.Views{

  public class HeroInfoBGView : MonoBehaviour {

    public void Init()
    {
      image = GetComponent<Image> ();
    }

    public void SetBackground(Sprite sprite)
    {
      
      image.sprite = sprite;
    }

    Image image;
  }
}
