using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InitHero.Views;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;
using DataManagement.TableClass;
using DataManagement.TableClass.Hero;
using HeroInfo.Controllers;

namespace InitHero.Controllers{

  public class HeroSelecterController : MonoBehaviour {

    public int DefaultIndex = 0;
    public int DBHeroID;
    public int HeroMaxNumber;
    public HeroFigureView HeroFigure;
    // Use this for initialization
    void Start () 
    {
      DBHeroID = DefaultIndex;

      dataHeroList =  HeroTableReader.Instance.DefaultCachedList;
      HeroMaxNumber = dataHeroList.Count;
      //Default Icon
      heroIconData = new HeroIconDataFormat (dataHeroList[0].TexturePath,dataHeroList[0].TextureIconID);

      HeroFigure.Init ();
      HeroFigure.SetImage (heroIconData);
    }

    public void ChangeFigureLeft()
    {
      DBHeroID = (HeroMaxNumber + (DBHeroID - 1) ) % HeroMaxNumber;
      heroIconData.Path = dataHeroList [DBHeroID].TexturePath;
      heroIconData.IconID = dataHeroList [DBHeroID].TextureIconID;
      ChangeFigure (heroIconData);
    }

    public void ChangeFigureRight()
    {
      DBHeroID = (DBHeroID + 1) % HeroMaxNumber;
      heroIconData.Path = dataHeroList [DBHeroID].TexturePath;
      heroIconData.IconID = dataHeroList [DBHeroID].TextureIconID;
      ChangeFigure (heroIconData);
    }

    void ChangeFigure(HeroIconDataFormat heroIconData)
    {
      HeroFigure.SetImage (heroIconData);
    }
    HeroIconDataFormat heroIconData;
    List<HeroTable> dataHeroList;
  }

}
