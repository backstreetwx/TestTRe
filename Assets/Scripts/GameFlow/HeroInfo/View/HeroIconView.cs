using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.GameData.FormatCollection;
using HeroInfo.Controllers;

namespace HeroInfo.Views{
  
  public class HeroIconView : MonoBehaviour {

    public void Init()
    {
      image = GetComponent<Image> ();
      colorUnSelected = new Color (0.5f,0.5f,0.5f);
      colorSelected = new Color (1f,1f,1f);
    }


    public void SetHeroIcon(HeroIconDataFormat heroIconDataFormat)
    {

      this.spriteList = Resources.LoadAll<Sprite> (heroIconDataFormat.Path);
      image.sprite = this.spriteList[heroIconDataFormat.IconID];
    }

    public void HeroSelected()
    {
      image.color = colorSelected;
    }

    public void HeroUnSelected()
    {
      image.color = colorUnSelected;
    }

    Color colorUnSelected;
    Color colorSelected;
    Image image;
    Sprite[] spriteList;
  }
  
}
