using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skill.Test.FormatCollection;
using Common;


namespace Skill.Test{

  public class TestSkillSaveDataManager : Singleton<TestSkillSaveDataManager>  {

    public void InitTestSaveData(TestSkillSaveDataFormat[] testSkillSaveDataList)
    {
      dataExist = true;
      this.testSkillSaveDataList = new TestSkillSaveDataFormat[testSkillSaveDataList.Length];
      this.testSkillSaveDataList = testSkillSaveDataList;
    }

    public bool DataExist
    {
      get
      {
        return this.dataExist;
      }
    }


    public TestSkillSaveDataFormat[] GetChildSkillSaveDataList(TestSkillDataFormat testSkillData)
    {
      List<TestSkillSaveDataFormat> _testSkillSaveDataList = new List<TestSkillSaveDataFormat> ();
      for (int i = 0; i < testSkillData.TestSkillSaveData.ChildSkillIDs.Count; i++) 
      {
        _testSkillSaveDataList.Add (GetTestSkillSaveData(testSkillData.TestSkillSaveData.ChildSkillIDs[i]));
      }

      return _testSkillSaveDataList.CloneEx().ToArray();

    }

    public TestSkillSaveDataFormat GetTestSkillSaveData(int skillID)
    {
      TestSkillSaveDataFormat _testSkillSaveData = new TestSkillSaveDataFormat ();
      for (int i = 0; i<testSkillSaveDataList.Length; i++) 
      {
        if (testSkillSaveDataList [i].DBSkillID == skillID)
          _testSkillSaveData = testSkillSaveDataList [i];

      }

      return _testSkillSaveData;

    }


    public void WriteToPlayerPrefs()
    {
      PlayerPrefs.SetString (KEY_TEST_SAVE, JsonUtility.ToJson (this.testSkillSaveDataList));
    }



    #region PRIVATE_MEMBER
    static readonly string KEY_TEST_SAVE = "KEY_TEST_SAVE";
    bool dataExist = false;
    TestSkillSaveDataFormat[] testSkillSaveDataList;
    #endregion
  }
}
