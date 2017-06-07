using UnityEngine;
using System.Collections;
using HeroInfo.Views;
using NextJump.Views;
using DataManagement.GameData.FormatCollection;


namespace NextJump.Controllers{

  public class HeroFullCanvasHeroController : MonoBehaviour {

    public int OrderId;

    public HeroInfoView HeroName;
    public HeroLevelView HeroLevel;
    public HeroIconView HeroIcon;

    public HeroBeSavedButtonView HeroBeSavedButton;

    public void Init(HeroDataFormat heroData)
    {
      HeroName.Init ();
      HeroLevel.Init ();
      HeroIcon.Init ();
      HeroBeSavedButton.Init ();
      heroFullManager = FindObjectOfType<HeroFullManager> ();
    }

    public void HeroSaveButtonViewShow(Sprite buttonSprite)
    {
      HeroBeSavedButton.SetButtonSprite (buttonSprite);
    }

    public void DisplayInfo (HeroFullCanvasHeroDataFormat dataFormat) 
    {
      
      HeroName.DataStringDisplay (dataFormat.Name);
      HeroLevel.HeroLevelDisplay (dataFormat.Level);
      HeroIcon.SetHeroIcon (dataFormat.HeroIconData);
    }

    public void ChooseHeroToBeReplace()
    {
      heroFullManager.ChooseHero (this.OrderId);
    }

    HeroFullManager heroFullManager;
  }
}
