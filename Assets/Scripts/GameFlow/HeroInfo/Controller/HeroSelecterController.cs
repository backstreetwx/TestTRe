using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using HeroInfo.Views;
using DataManagement.GameData.FormatCollection;

namespace HeroInfo.Controllers{

  public class HeroSelecterController : MonoBehaviour {

    public HeroSelecterView[] HeroSelecters;
    public HeroIconView[] HeroIcons;

    public void Init () 
    {
      for(int i = 0;i < HeroIcons.Length;i++)
      {
        HeroIcons [i].Init ();
      }

    }

    public void HeroExistOnSlotOrNot(List<HeroIconDataFormat> heroIconDataList)
    {
      for (int i = 0; i < heroIconDataList.Count; i++) 
      {
        if (heroIconDataList [i].IconID > -1) {
          HeroSelecters [i].gameObject.SetActive (true);
          HeroIcons [i].gameObject.SetActive (true);
          HeroIcons [i].SetHeroIcon (heroIconDataList [i]);
        } 
        else 
        {
          HeroSelecters [i].gameObject.SetActive (false);
          HeroIcons [i].gameObject.SetActive (false);
        }
      }
    }

    public void HeroUnSelected()
    {
      for(int i = 0;i < HeroIcons.Length;i++)
      {
        HeroIcons [i].HeroUnSelected();
      }
    }

    public void HeroSelected(int slot)
    {
      HeroIcons [slot].HeroSelected();
    }

  }

}
