using UnityEngine;
using System.Collections;
using HeroInfo.Views;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using System.Collections.Generic;

namespace HeroInfo.Controllers{

  public class CharacterAttributeDisplayController : MonoBehaviour {

    public HeroInfoView HPView;
    public HeroInfoView RESView;
    public HeroInfoView ATKView;
    public HeroInfoView MAGView;
    public HeroInfoView DEFView;
    public HeroInfoView ACView;
    public HeroInfoView CRIView;
    public HeroInfoView PENView;
    public HeroInfoView HITView;
    public HeroInfoView AVDView;
     
    public void Init()
    {
      HPView.Init ();
      RESView.Init ();
      ATKView.Init ();
      MAGView.Init ();
      DEFView.Init ();
      ACView.Init ();
      CRIView.Init ();
      PENView.Init ();
      HITView.Init ();
      AVDView.Init ();
    }

    public void SetDataForDisplay(HeroDataFormat heroDataFormat)
    {
      //Get attribute after calculated by equipment
      HeroAttributeFormat heroAttributeFinal = heroDataFormat.AttributesWithEquipments;
      HPView.DataIntDisplay (heroAttributeFinal.HPMax);
      RESView.DataIntDisplay (heroAttributeFinal.RES);
      ATKView.DataIntDisplay (heroAttributeFinal.ATK);
      MAGView.DataIntDisplay (heroAttributeFinal.MAG);
      DEFView.DataIntDisplay (heroAttributeFinal.DEF);
      ACView.DataIntDisplay (heroAttributeFinal.AC);
      CRIView.DataIntDisplay (heroAttributeFinal.CRI);
      PENView.DataIntDisplay (heroAttributeFinal.PEN);
      HITView.DataIntDisplay (heroAttributeFinal.HIT);
      AVDView.DataIntDisplay (heroAttributeFinal.AVD);
    }

  }
}
