using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;

namespace Skill.Controllers{

  public class SkillAdvanceManager : MonoBehaviour {

    public SkillAdvanceController BasicSkill;
    public SkillAdvanceController AdvanceSkill0;
    public SkillAdvanceController AdvanceSkill1;

    public void Init (SkillAdvanceCache skillData) 
    {
      this.skillAdvanceData = skillData;
      this.heroData = this.skillAdvanceData.HeroData;
      BasicSkill.Init (this.heroData,this.skillAdvanceData.HeroSkillList[0]);
      AdvanceSkill0.Init (this.heroData,this.skillAdvanceData.HeroSkillList[1]);
      AdvanceSkill1.Init (this.heroData,this.skillAdvanceData.HeroSkillList[2]);

      BasicSkill.SkillInfoDisplay ();
      AdvanceSkill0.SkillInfoDisplay ();
      AdvanceSkill1.SkillInfoDisplay ();

  	}

    HeroDataFormat heroData;
    SkillAdvanceCache skillAdvanceData;
  }
}
