using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Equipment.Views{

  public class EquipmentIconView : MonoBehaviour {

    // Use this for initialization
    public void Init () 
    {
      image = GetComponent<Image> ();
    }

    public void IconDisplayOrNot(bool mark)
    {
      if (mark) {
        Color _color = image.color;
        _color.a = 1.0f;
        image.color = _color;
      } 
      else if (!mark) 
      {
        Color _color = image.color;
        _color.a = 0.0f;
        image.color = _color;
      }
    }

    public void ShowEquipmentIcon(string path,int id)
    {
      spriteList = Resources.LoadAll<Sprite> (path);
      image.sprite = spriteList[id];

    }

    Image image;
    Sprite[] spriteList;
  }
}
