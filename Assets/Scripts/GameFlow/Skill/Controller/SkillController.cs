using UnityEngine;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using Skill.Test.FormatCollection;
using Skill.Views;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Common;
using DataManagement.TableClass.Skill;
using DataManagement;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums;
using ConstCollections.PJConstOthers;
using ConstCollections.PJEnums.Skill;
using System.Linq;

namespace Skill.Controllers{

  public class SkillController : MonoBehaviour {

    public int SlotId;

    public PopSkillCanvasManager PopSkillManager;
    public GameObject SkillAdvancePrefab;

    public int SkillMaxLevel = SkillOthers.LEVEL_MAX;
    public SkillMessageView SkillName;
    public SkillLevelView SkillLevel;
    public SkillMessageView SkillDescription;
    public SkillOperaterView SkillOperaterButtonView;
    public SkillIconView SkillIconViewScript;

    public Sprite SkillUpdateIdleSprite;
    public Sprite SkillUpdatePressedSprite;

    public Sprite SkillAdvanceIdleSprite;
    public Sprite SkillAdvancePressedSprite;

    public SKILL_STRINGS_LABEL SkillNameLabel;
    public SKILL_STRINGS_LABEL SkillDescriptionLabel;

    void OnEnable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

    public void Init(int point)
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();

      SkillName.Init ();
      SkillLevel.Init ();
      SkillDescription.Init ();
      SkillOperaterButtonView.Init ();
      SkillIconViewScript.Init ();

      WhetherThereIsDataOnSlot (point);

      SkillPointZeroOrNot (point);
    }

    public void SkillPointZeroOrNot(int point)
    {
      
      SetButtonClickableOrNot (point);
    }

    public void SetButtonClickableOrNot(int point)
    {

      if (point > 0) 
      {
        SkillOperaterButtonView.ButtonClickableOrNot (true);
      } 
      else 
      {
        SkillOperaterButtonView.ButtonClickableOrNot (false);
      }

    }

    public void WhetherThereIsDataOnSlot(int point)
    {
      if (point < 0) 
      {
        this.gameObject.SetActive (false);
      }
    }

    public void InitDisplayAndLoadData(HeroDataFormat heroData)
    {
      this.selfHeroData = heroData.CloneEx();

      //hero jumped
      if (!heroData.Attributes.Active)
        SkillOperaterButtonView.gameObject.SetActive (false);

      for (int i = 0; i < this.selfHeroData.SkillList.Count; i++) 
      {
        if (this.selfHeroData.SkillList [i].SlotID == this.SlotId)
          this.selfHeroSkillData = this.selfHeroData.SkillList [i];
      }

      var _multiSkillName = SkillStringsTableReader.Instance.GetMultiLangStringWithoutParam (this.selfHeroSkillData.DBSkillID,SkillNameLabel);
      var _multiSkillDescription = SkillStringsTableReader.Instance.GetMultiLangStringWithParam (this.selfHeroSkillData.DBSkillID,this.selfHeroSkillData.Level,SkillDescriptionLabel);

      SkillName.SetNewMultiStringAndDisplay (_multiSkillName);
      SkillDescription.SetNewMultiStringAndDisplay (_multiSkillDescription);
      SkillIconViewScript.SetSkillIconByPath (this.selfHeroSkillData.TexturePath,this.selfHeroSkillData.TextureIconID);
      SkillLevelDisplay (this.selfHeroSkillData.Level);

    }


    public void SkillLevelDisplay(int level)
    {
      if (level < SkillMaxLevel) 
      {
        SkillOperaterButtonView.SetButtonSprites (SkillUpdateIdleSprite,SkillUpdatePressedSprite);
      } 
      else if(level == SkillMaxLevel)
      {
        var _heroSkill = HeroDataManager.Instance.GetSkill (this.selfHeroData.Attributes.SlotID,this.selfHeroSkillData.SlotID);
        if (_heroSkill.ChildrenIDList == null) 
        {
          SkillOperaterButtonView.ButtonVisibleOrNot (false);
        } 
        else 
        {
          SkillOperaterButtonView.SetButtonSprites (SkillAdvanceIdleSprite,SkillAdvancePressedSprite);
        }

      }
      SkillLevel.SkillLevelDisplay (level);
    }

    public void SkillOperation()
    {
      
      if (this.selfHeroSkillData.Level < SkillMaxLevel) 
      {
        
        this.selfHeroData.Attributes.SkillPoint -= 1;
        this.selfHeroData.SkillList [this.SlotId].Level += 1;

        HeroSaveDataManager.Instance.Overwrite (this.selfHeroData);

      }
      else
      {
        List<HeroSkillFormat> _heroSkillList = new List<HeroSkillFormat> ();
        _heroSkillList.Add (this.selfHeroSkillData);

        List<ushort> _childSkillIdList = this.selfHeroSkillData.ChildrenIDList;

        if (_childSkillIdList != null) 
        {
          for (int i = 0; i < _childSkillIdList.Count; i++) 
          {
            HeroSkillFormat _heroSkillData = new HeroSkillFormat (new HeroSkillSaveDataFormat(this.selfHeroSkillData.SlotID,_childSkillIdList [i],0));
            _heroSkillList.Add (_heroSkillData);
          }
        }

        SkillAdvanceCache _skillAdvanceData = new SkillAdvanceCache (this.selfHeroData,_heroSkillList);
        globalDataManager.SetValue<SkillAdvanceCache> (SkillString.SKILL_ADVANCE_CACHE, _skillAdvanceData,SkillString.MEMORY_SPACE);
        PopSkillManager.ShowWindow (SkillAdvancePrefab);
      }
    }

    void OnHeroCacheChanged(int slotId,HeroDataFormat heroData)
    {
      if (this.selfHeroData == null)
        return;
      if (this.selfHeroData.Attributes.SlotID != slotId)
        return;

      this.selfHeroData = heroData.CloneEx ();

      for (int i = 0; i < this.selfHeroData.SkillList.Count; i++) 
      {
        if (this.selfHeroData.SkillList [i].SlotID == this.SlotId) 
        {
          // skillID changed, new multiLang
          if (this.selfHeroData.SkillList [i].DBSkillID != this.selfHeroSkillData.DBSkillID) 
          {
            var _multiSkillName = SkillStringsTableReader.Instance.GetMultiLangStringWithoutParam (this.selfHeroData.SkillList [i].DBSkillID, SkillNameLabel);
            var _multiSkillDescription = SkillStringsTableReader.Instance.GetMultiLangStringWithParam (this.selfHeroData.SkillList [i].DBSkillID,this.selfHeroData.SkillList [i].Level, SkillDescriptionLabel);
            SkillName.SetNewMultiStringAndDisplay (_multiSkillName);
            SkillDescription.SetNewMultiStringAndDisplay (_multiSkillDescription);
            SkillIconViewScript.SetSkillIconByPath (this.selfHeroData.SkillList [i].TexturePath,this.selfHeroData.SkillList [i].TextureIconID);
          }
          // skill data changed, do not need new multiLang
          else if(this.selfHeroData.SkillList [i].DBSkillID == this.selfHeroSkillData.DBSkillID)
          {
            SkillName.DisplayMessage ();
            var _descriptionParam = SkillStringsTableReader.Instance.GetSkillDescriptionParameter (this.selfHeroData.SkillList [i].DBSkillID,this.selfHeroData.SkillList [i].Level);
            var _parameterArray = _descriptionParam.Cast<object> ().ToArray();

            SkillDescription.MultiLangParamUpdate (_parameterArray);
          }
          this.selfHeroSkillData = this.selfHeroData.SkillList [i];

        }
          
      }

      SkillLevelDisplay (this.selfHeroSkillData.Level);
    }

    HeroDataFormat selfHeroData;
    HeroSkillFormat selfHeroSkillData;
    GlobalDataManager globalDataManager;
  }

  [System.Serializable]
  public class SkillAdvanceCache
  {
    public HeroDataFormat HeroData;
    public List<HeroSkillFormat> HeroSkillList;

    public SkillAdvanceCache()
    {}

    public SkillAdvanceCache(HeroDataFormat heroData ,List<HeroSkillFormat> heroSkillList)
    {
      this.HeroData = heroData;
      this.HeroSkillList = heroSkillList;
    }

  }
}
