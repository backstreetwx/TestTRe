using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DataManagement;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstStrings;
using Common;

namespace NextJump.Controllers{

  public class HeroLessThan3Manager : MonoBehaviour {

    public string NextSceneName;
    public WindowPopManager PopHeroLessThan3Manager;

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      heroSaveDataList = HeroSaveDataManager.Instance.HeroSaveDataList;
      //if hero number is 1, hero0's slotId = 0, so nextHeroSlotId = 1 
      //if hero number is 2, hero0's slotId = 0, hero1's slotId = 1, so nextHeroSlotId = 2
      this.nextSlotID = heroSaveDataList.Count;
      PopHeroLessThan3Manager = FindObjectOfType<WindowPopManager>();

    }
      
    public void NextJump()
    {
      globalDataManager.SetValue<int> (NextJumpString.NEXT_HERO_SLOTID,this.nextSlotID,NextJumpString.MEMORY_SPACE);
      SceneManager.LoadScene (this.NextSceneName);
    }

    public void Back()
    {
      
      PopHeroLessThan3Manager.Close ();
    }

    int nextSlotID;
    List<HeroSaveDataFormat> heroSaveDataList;
    GlobalDataManager globalDataManager;
  }
}
