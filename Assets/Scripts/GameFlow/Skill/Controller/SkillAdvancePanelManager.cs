using UnityEngine;
using System.Collections;
using DataManagement;
using ConstCollections.PJConstStrings;

namespace Skill.Controllers{

  public class SkillAdvancePanelManager : MonoBehaviour {

    public HeroBasicDataController HeroBasicData;
    public SkillAdvanceManager SkillAdvance;

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
   
      SkillAdvanceCache _skillAdvanceCacheData = globalDataManager.GetValue<SkillAdvanceCache> (SkillString.SKILL_ADVANCE_CACHE,SkillString.MEMORY_SPACE);

      HeroBasicData.Init (_skillAdvanceCacheData.HeroData);

      SkillAdvance.Init (_skillAdvanceCacheData);

      globalDataManager.RemoveValue (SkillString.SKILL_ADVANCE_CACHE,SkillString.MEMORY_SPACE);
    }

    GlobalDataManager globalDataManager;
  }
}
