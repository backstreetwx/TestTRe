using UnityEngine;
using System.Collections;
using HeroInfo.Views;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using System.Collections.Generic;
using HeroInfo.Test;

namespace HeroInfo.Controllers{

  public class BasicAttributeDisplayController : MonoBehaviour {

    public HeroInfoView NameView;
    public HeroInfoView EXPView;
    public HeroInfoView SkillPointView;
    public HeroInfoView STRView;
    public HeroInfoView INTView;
    public HeroInfoView VITView;
    public HeroInfoView DEXView;
    public HeroLevelView LevelView;

    public void Init()
    {

      NameView.Init();
      LevelView.Init();
      EXPView.Init();
      SkillPointView.Init();
      STRView.Init();
      INTView.Init();
      VITView.Init();
      DEXView.Init ();
      LevelView.Init();
    }

    public void SetDataForDisplay(HeroDataFormat heroDataFormat)
    {
      
      HeroAttributeFormat _hero = heroDataFormat.Attributes;
      NameView.DataStringDisplay (_hero.NameString);
      LevelView.HeroLevelDisplay (_hero.Level);
      EXPView.DataStringDisplay (EXPBeforeNextLevel(_hero.EXP,_hero.EXPMax));
      SkillPointView.DataIntDisplay (_hero.SkillPoint);
      //Get attribute after calculated by equipment
      HeroAttributeFormat _heroFinal = heroDataFormat.AttributesWithEquipments;
      STRView.DataIntDisplay (_heroFinal.STR);
      INTView.DataIntDisplay (_heroFinal.INT);
      VITView.DataIntDisplay (_heroFinal.VIT);
      DEXView.DataIntDisplay (_heroFinal.DEX);
    }


    string EXPBeforeNextLevel(int expNow, int expMax)
    {
      return (expMax - expNow).ToString();
    }
  }
}
