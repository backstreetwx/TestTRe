using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skill.Views;
using Common;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData;
using ConstCollections.PJConstOthers;
using DataManagement.TableClass.Skill;
using ConstCollections.PJEnums.Skill;

namespace Skill.Controllers{

  public class SkillAdvanceController : MonoBehaviour {

    public PopSkillCanvasManager PopSkillManager;

    public HeroSkillFormat NewHeroSkillData;
    public HeroSkillFormat HeroSkillDataForDisplay;

    public SkillMessageView SkillName;
    public SkillLevelView SkillLevel;
    public SkillMessageView SkillDescription;
    public SkillIconView SkillIconViewScript;

    public SKILL_STRINGS_LABEL SkillNameLabel;
    public SKILL_STRINGS_LABEL SkillDescriptionLabel;


    public int LevelMax = SkillOthers.LEVEL_MAX;

    void OnEnable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

    public void Init(HeroDataFormat heroData,HeroSkillFormat skillData)
    {
      this.selfHeroData = heroData.CloneEx();
      this.NewHeroSkillData = skillData;

      //if the Skill is the Upgrade one,
      //Show Level = 1
      int _levelForDisplay = this.NewHeroSkillData.Level;
      if (_levelForDisplay == 0)
        _levelForDisplay += 1;
      
      this.HeroSkillDataForDisplay = new HeroSkillFormat (new HeroSkillSaveDataFormat(this.NewHeroSkillData.SlotID,this.NewHeroSkillData.DBSkillID,_levelForDisplay));


      SkillName.Init ();
      SkillLevel.Init ();
      SkillDescription.Init ();
      SkillIconViewScript.Init ();
      PopSkillManager = FindObjectOfType<PopSkillCanvasManager>();
    }

    public void SkillInfoDisplay()
    {

      var _multiSkillName = SkillStringsTableReader.Instance.GetMultiLangStringWithoutParam (this.HeroSkillDataForDisplay.DBSkillID,SkillNameLabel);
      var _multiSkillDescription = SkillStringsTableReader.Instance.GetMultiLangStringWithParam (this.HeroSkillDataForDisplay.DBSkillID,this.HeroSkillDataForDisplay.Level,SkillDescriptionLabel);

      SkillName.SetNewMultiStringAndDisplay (_multiSkillName);
      SkillDescription.SetNewMultiStringAndDisplay (_multiSkillDescription);
      SkillIconViewScript.SetSkillIconByPath (this.HeroSkillDataForDisplay.TexturePath,this.HeroSkillDataForDisplay.TextureIconID);
      SkillLevelDisplay (this.HeroSkillDataForDisplay.Level);
    }

    public void SkillLevelDisplay(int level)
    {
      SkillLevel.SkillLevelDisplay (level);
    }

    public void SkillChange()
    {
      
      this.NewHeroSkillData.Level += 1;

      for(int i = 0;i<this.selfHeroData.SkillList.Count; i++)
      {
        if (this.selfHeroData.SkillList [i].SlotID == this.NewHeroSkillData.SlotID)
          this.selfHeroData.SkillList [i] = this.NewHeroSkillData;
      }
      this.selfHeroData.Attributes.SkillPoint -= 1;
      HeroSaveDataManager.Instance.Overwrite (this.selfHeroData);

      PopSkillManager.Close ();

    }

    void OnHeroCacheChanged(int slotId,HeroDataFormat heroData)
    {
      if (this.selfHeroData == null)
        return;
      if (this.selfHeroData.Attributes.SlotID != slotId)
        return;

      this.selfHeroData = heroData.CloneEx();
      
    }

    HeroDataFormat selfHeroData;

  }
}
