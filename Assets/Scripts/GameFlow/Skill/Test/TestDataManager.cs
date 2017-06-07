using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skill.Test.FormatCollection;
using Common;

namespace Skill.Test{

  public class TestDataManager : Singleton<TestDataManager> {

    public event System.Action<TestSkillPanelDataFormat> TestDataChangedEvent;
    public event System.Action<int> TestSkillPointChangedEvent;
    public event System.Action<List<TestSkillDataFormat>> TestSkillDataListChangedEvent;
    public event System.Action<TestSkillDataFormat> TestSkillDataChangedEvent;

    public bool DataExist
    {
      get
      {
        return this.dataExist;
      }
    }

    public void InitTestData(TestSkillPanelDataFormat testSkillPanelData)
    {
      this.testPanelData = new TestSkillPanelDataFormat ();
      this.testPanelData = testSkillPanelData;
      this.testSkillData = new TestSkillDataFormat ();
      dataExist = true;
    }

    public TestSkillDataFormat TestSkillData
    {
      get
      {
        return this.testSkillData;
      }
      set
      {
        this.testSkillData = value.CloneEx();
        this.TestSkillDataChangedEvent.Invoke (this.testSkillData);
        WriteToPlayerPrefs ();
      }
    }


    public TestSkillPanelDataFormat TestSkillPanelData
    {
      get
      {
        return this.testPanelData;
      }
      set
      {
        this.testPanelData = value.CloneEx();
        this.TestDataChangedEvent.Invoke (this.testPanelData);
        WriteToPlayerPrefs ();
      }
    }

    public int SkillPoint
    {
      get
      {
        return this.testPanelData.SkillPoint;
      }
      set
      {
        this.testPanelData.SkillPoint = value;
        this.TestSkillPointChangedEvent.Invoke (testPanelData.SkillPoint);
        WriteToPlayerPrefs ();
      }
    }

    public TestSkillDataFormat GetTestSkillData(int slot)
    {
      TestSkillDataFormat _testSkillData = new TestSkillDataFormat ();
      TestSkillDataFormat[] _testSkillDataList = GetTestSkillDataList();
      for (int i = 0; i < _testSkillDataList.Length; i++) 
      {
        if (_testSkillDataList [i].SlotID == slot)
          _testSkillData = _testSkillDataList [i].CloneEx ();
      }

      return _testSkillData;
    }

    public TestSkillDataFormat[] GetTestSkillDataList()
    {
      return testPanelData.TestSkillInformationFormatList.CloneEx().ToArray();
    }

    public void OverwriteTestSkillData(TestSkillDataFormat testSkillData)
    {
      
      int _targetIndex = testSkillData.SlotID;
      if (_targetIndex >= 0) 
      {
        testPanelData.TestSkillInformationFormatList [_targetIndex] = testSkillData.CloneEx ();
        WriteToPlayerPrefs ();
        this.TestSkillDataListChangedEvent.Invoke (testPanelData.TestSkillInformationFormatList);
      }

    }

    public TestSkillAdvanceCache TestSkillAdvanceData
    {
      get
      {
        return this.testSkillAdvanceData;
      }
      set
      {
        this.testSkillAdvanceData = value;
      }
    }


    public void WriteToPlayerPrefs()
    {
      PlayerPrefs.SetString (KEY_TEST, JsonUtility.ToJson (this.testPanelData));
    }

    #region PRIVATE_MEMBER
    static readonly string KEY_TEST = "KEY_TEST";
    TestSkillPanelDataFormat testPanelData;
    TestSkillDataFormat testSkillData;
    TestSkillAdvanceCache testSkillAdvanceData = new TestSkillAdvanceCache ();
    bool dataExist = false;
    #endregion

  }

  [System.Serializable]
  public class TestSkillAdvanceCache
  {
    public string Name;
    public int SkillPoint;
    public int SlotID;
    public List<TestSkillDataFormat> TestSkillAdvanceList;

    public TestSkillAdvanceCache()
    {}

    public TestSkillAdvanceCache(string name,int skillPoint,int slotId,List<TestSkillDataFormat> testSkillAdvanceList)
    {
      this.Name = name;
      this.SkillPoint = skillPoint;
      this.SlotID = slotId;
      this.TestSkillAdvanceList = testSkillAdvanceList;
    }

  }

}
