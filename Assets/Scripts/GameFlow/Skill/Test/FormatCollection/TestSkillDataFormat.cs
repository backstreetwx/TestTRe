using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Skill.Test.FormatCollection{

  [System.Serializable]
  public class TestSkillDataFormat
  {
    public int SlotID;
    public TestSkillSaveDataFormat TestSkillSaveData;

    public TestSkillDataFormat()
    {
    }

    public TestSkillDataFormat(int slotId,TestSkillSaveDataFormat testSkillSaveData)
    {
      this.SlotID = slotId;
      this.TestSkillSaveData = testSkillSaveData;
    }

  }

}
