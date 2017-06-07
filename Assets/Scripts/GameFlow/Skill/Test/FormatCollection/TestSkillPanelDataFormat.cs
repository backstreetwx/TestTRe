using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Skill.Test.FormatCollection{

  [System.Serializable]
  public class TestSkillPanelDataFormat
  {
    public int SlotID;
    public int DBHeroID;
    public string Name;
    public int SkillPoint;
    public List<TestSkillDataFormat> TestSkillInformationFormatList;

    public TestSkillPanelDataFormat()
    {}

    public TestSkillPanelDataFormat(int slotID,int dbHeroID,string name, int skillPoint,List<TestSkillDataFormat> testSkillInformationFormatList)
    {
      this.SlotID = slotID;
      this.DBHeroID = dbHeroID;
      this.Name = name;
      this.SkillPoint = skillPoint;
      this.TestSkillInformationFormatList = testSkillInformationFormatList;
    }

  }
}
