using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HeroInfo.Views;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using Skill.Views;


namespace HeroInfo.Controllers{

  public class SkillLayoutDisplayController : MonoBehaviour {

    public SkillLevelLabelView[] SkillLabelList;
    public SkillIconView[] SkillNormalIconList;
    public SkillIconView SkillSpecialIconView;

    public Sprite SkillLockSprite; 

    public void Init () 
    {
      for (int i = 0; i < SkillLabelList.Length; i++) 
      {
        SkillLabelList [i].Init ();
      }
      for (int i = 0; i < SkillNormalIconList.Length; i++) 
      {
        SkillNormalIconList [i].Init ();
      }

      SkillSpecialIconView.Init ();
    }

    public void SetDataForDisplay(HeroDataFormat heroDataFormat)
    {
      skillList = heroDataFormat.SkillList;
      if (skillList != null) 
      {
        for (int i = 0; i < skillList.Count; i++) 
        {
          SkillLabelList [i].SkillLevelDisplay (skillList[i].Level);

          SkillNormalIconList [i].SetSkillIconByPath (skillList[i].TexturePath,skillList[i].TextureIconID);
        }

        //FIXME : yangzhi-wang ,do it next version, load path from csv
        SkillSpecialIconView.SetSkillIconBySprite (SkillLockSprite);
      }

    }

    List<HeroSkillFormat> skillList;
  }
}
