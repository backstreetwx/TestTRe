using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using HeroInfo.Controllers;

namespace InitHero.Views{

  public class HeroFigureView : MonoBehaviour {

    public void Init()
    {
      image = GetComponent<Image> ();
    }

    public void SetImage(HeroIconDataFormat heroIconData)
    {
      this.spriteList = Resources.LoadAll<Sprite> (heroIconData.Path);
      image.sprite = this.spriteList[heroIconData.IconID];;
    }

    Image image;
    Sprite[] spriteList;
  }
}
