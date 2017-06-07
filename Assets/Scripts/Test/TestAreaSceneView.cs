using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Test{

  public class TestAreaSceneView : MonoBehaviour {

    void Awake () 
    {
      image = GetComponent<Image> ();
    }

    public void SetAreaBG(string path)
    {
      Sprite _sprite = Resources.Load<Sprite> (path);
      image.sprite = _sprite;
    }


    Image image;
  }
}
