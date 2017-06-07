using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skill.Views;
using ConstCollections.PJConstOthers;
using DataManagement.GameData.FormatCollection;
using GameFlow.Battle.View;
using DataManagement.GameData.FormatCollection.Common.Skill;
using ConstCollections.PJEnums.Skill;
using DataManagement.TableClass.Skill;

namespace BattleCharacterInfo.Controllers{

  public class BattleCharacterSkillController : MonoBehaviour {

    public int SkillMaxLevel = SkillOthers.LEVEL_MAX;
    public SkillMessageView SkillName;
    public SkillLevelView SkillLevel;
    public SkillMessageView SkillDescription;
    public SkillIconView SkillIconViewScript;
    public int SkillSlotId;

    public SKILL_STRINGS_LABEL SkillNameLabel;
    public SKILL_STRINGS_LABEL SkillDescriptionLabel;

    public void Init (int point) 
    {
      SkillName.Init ();
      SkillLevel.Init ();
      SkillDescription.Init ();
      SkillIconViewScript.Init ();
      WhetherThereIsDataOnSlot (point);
    }
    public void WhetherThereIsDataOnSlot(int point)
    {
      if (point < 0) 
      {
        this.gameObject.SetActive (false);
      }
    }

    public void DisplaySkillInfo(List<CommonSkillFormat> skillList)
    {

      for (int i = 0; i < skillList.Count; i++) 
      {
        if (skillList [i].SlotID == this.SkillSlotId)
          this.skillData = skillList [i];
      }

      var _multiSkillName = SkillStringsTableReader.Instance.GetMultiLangStringWithoutParam (this.skillData.DBSkillID,SkillNameLabel);
      var _multiSkillDescription = SkillStringsTableReader.Instance.GetMultiLangStringWithParam (this.skillData.DBSkillID,this.skillData.Level,SkillDescriptionLabel);

      SkillName.SetNewMultiStringAndDisplay (_multiSkillName);
      SkillDescription.SetNewMultiStringAndDisplay (_multiSkillDescription);
      SkillIconViewScript.SetSkillIconByPath (this.skillData.TexturePath,this.skillData.TextureIconID);
      SkillLevel.SkillLevelDisplay (this.skillData.Level);

    }

    CommonSkillFormat skillData;

  }
}
