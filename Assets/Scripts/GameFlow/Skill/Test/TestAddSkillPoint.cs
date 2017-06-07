using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Skill.Controllers;
using Common;
using Skill.Test;
using Skill.Test.FormatCollection;
using DataManagement.SaveData.FormatCollection;
using DataManagement.SaveData;

namespace Skill.Test{

  public class TestAddSkillPoint : MonoBehaviour {

    public int SlotID;


    public void AddSkillPoint()
    {
      heroSaveData = HeroSaveDataManager.Instance.GetSaveData(SlotID);
      heroSaveData.SkillPoint += 1;

      HeroSaveDataManager.Instance.Overwrite (this.heroSaveData);

    }

    HeroSaveDataFormat heroSaveData;
  }
}
