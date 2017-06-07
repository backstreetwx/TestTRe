using UnityEngine;
using System.Collections;
using DataManagement;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstStrings;


namespace Skill.Controllers{

  public class SkillPanelManager : MonoBehaviour {

    public HeroBasicDataController HeroBasicData;
    public SkillManager SkillManager;

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      int? _slotId = globalDataManager.GetNullableValue<int> (SkillString.SKILL_SLOT_ID,SkillString.MEMORY_SPACE);

      if (_slotId != null) 
      {
        
        var _heroDataList = HeroDataManager.Instance.HeroDataCacheList;
        for (int i = 0; i < _heroDataList.Count; i++) 
        {
          if (_heroDataList[i].Attributes.SlotID == (int)_slotId)
            heroData = _heroDataList[i];
        }


        HeroBasicData.Init (heroData);
        SkillManager.Init (heroData);

      }

    }
    GlobalDataManager globalDataManager;
    HeroSaveDataFormat heroSaveData;
    HeroDataFormat heroData;
  }
}
