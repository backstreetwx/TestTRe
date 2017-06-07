using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Test{

  public class ImageView : MonoBehaviour {

    public void Init()
    {
      image = GetComponent<Image> ();
    }

    public void SetSkillIcon(string path,int id)
    {
      sprite = Resources.Load<Sprite> (path+"_"+id);
      image.sprite = sprite;
    }

    Sprite sprite;
    Image image;
  }
}
