using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Skill.Test.FormatCollection{

  [System.Serializable]
  public class TestSkillSaveDataFormat
  {
    public int DBSkillID;
    public int Level;
    public int ParentSkillID;
    public List<int> ChildSkillIDs;
    public string SkillName;
    public string SkillDescription;

    public TestSkillSaveDataFormat()
    {
    }

    public TestSkillSaveDataFormat(int skillId, int parentSkillID,List<int> childSkillIDs,string skillName,string skillDescription)
    {

      this.DBSkillID = skillId;
      this.ParentSkillID = parentSkillID;
      this.ChildSkillIDs = childSkillIDs;
      this.SkillName = skillName;
      this.SkillDescription = skillDescription;
    }

  }
}
