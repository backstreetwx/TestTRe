using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skill.Views;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using Common;


namespace Skill.Controllers{

  public class HeroBasicDataController : MonoBehaviour {

    public SkillInfoView SkillPointView;

    void OnEnable () 
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

    // Use this for initialization
    public void Init (HeroDataFormat heroData) 
    {
      this.selfHeroData = heroData.CloneEx();
      SkillPointView.Init ();
      SetHeroSkillPoint (this.selfHeroData.Attributes.SkillPoint);

    }

    public void SetHeroSkillPoint(int point)
    {
      SkillPointView.ShowInfo (point.ToString ());
    }

    void OnHeroCacheChanged(int slotId, HeroDataFormat heroData)
    {
      if (this.selfHeroData == null)
        return;
      if (this.selfHeroData.Attributes.SlotID != slotId)
        return;

      this.selfHeroData = heroData.CloneEx ();
      SetHeroSkillPoint (this.selfHeroData.Attributes.SkillPoint);

    }

    HeroDataFormat selfHeroData;
  }

}
