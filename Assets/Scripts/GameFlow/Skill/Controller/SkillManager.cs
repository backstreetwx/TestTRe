using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using Skill.Test.FormatCollection;
using Common;

namespace Skill.Controllers{

  public class SkillManager : MonoBehaviour {

    public int SkillNumber = 4;

    public SkillController[] SkillControllers;

    void OnEnable () 
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }


    // Use this for initialization
    public void Init (HeroDataFormat data) 
    {
      
      heroData = data.CloneEx();

      int[] _points = new int[SkillNumber];
      for(int i = 0; i < SkillNumber; i++)
      {
        _points [i] = -1;
      }

      int dataLength = heroData.SkillList.Count;

      for (int i = 0; i < dataLength; i++) 
      {
        _points [i] = heroData.Attributes.SkillPoint;
      }

      for(int i = 0; i < SkillControllers.Length; i++)
      {
        SkillControllers [i].Init (_points[i]);
      }


      SkillDataListUpdate (heroData);

      SkillPointZeroOrNot (heroData.Attributes.SkillPoint);
    }

    public void SkillPointZeroOrNot(int point)
    {
      for (int i = 0; i < heroData.SkillList.Count; i++) 
      {
        SkillControllers [i].SkillPointZeroOrNot (point);
      }
    }

    public void SkillDataListUpdate(HeroDataFormat heroData)
    {

      for (int i = 0; i < heroData.SkillList.Count; i++) 
      {
        SkillControllers [i].InitDisplayAndLoadData (heroData);
      }

    }

    public void ResetSkillPoint()
    {
      //TODO : yangzhi-wang , Reset skill point
    }

    void OnHeroCacheChanged(int slotId, HeroDataFormat heroData)
    {

      if (this.heroData == null)
        return;
      if (this.heroData.Attributes.SlotID != slotId)
        return;

      this.heroData = heroData.CloneEx ();
      SkillPointZeroOrNot (this.heroData.Attributes.SkillPoint);

    }

    HeroDataFormat heroData;
  }

}
