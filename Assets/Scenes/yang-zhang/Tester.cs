using UnityEngine;
using System.Collections;
using DataManagement;
using System.Collections.Generic;

// List<T>
using System;
using DataManagement.SaveData;
using DataManagement.GameData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.TableClass.Hero;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData.FormatCollection.Common;
using Common;
using ConstCollections.PJEnums.Battle;
using DataManagement.TableClass;
using ConstCollections.PJEnums;
using DataManagement.TableClass.BattleInfo;
using DataManagement.TableClass.Skill;
using DataManagement.GameData.FormatCollection.Battle;
using DataManagement.TableClass.HangUp;


[Serializable]
public class Serialization<T>
{
  [SerializeField]
  List<T> target;
  public List<T> ToList() { return target; }

  public Serialization(List<T> target)
  {
    this.target = target;
  }
}

[System.Serializable]
public struct CA
{
  //[SerializeField]
  public List<string> TestList;
  public Dictionary<string, int> TestDic;
}

[System.Serializable]
public class CTemp
{
  public int Inner = 9;
}

[System.Serializable]
public class CTest
{
  public CTemp a;
  public CTemp b;
  public CTemp c;
}


public class Tester : MonoBehaviour {
  public Tester2 T2;
  public HeroSaveDataFormat HeroSaveData;
//  public EffectAttackDamageFormat TestEffect0;
//  public TriggerStatesFormat TestEffect0Trigger0;
  public FightDataFormat TestData;
  public System.Action ActionTester;

	// Use this for initialization
	void Start () {
//    Debug.LogFormat ("{0} - {2}", 1, 2, 3);
//    HeroNameTableReader.Instance.GetString (1);
//    HeroData.Attributes.CalculateCharacterAttribute();
//    TestData.Attributes = HeroData.Attributes.CloneEx ();

//    TestData.SkillList[0].SkillCondition = new ConditionController();
//    TestData.SkillList [0].SkillCondition.TriggerList = new List<ITrigger> ();

    //_timeTrigger.SelfActiveState = TURN_STATES.DECIDE_INITIATIVE_SKILL;

//    TestData.SkillList[0] = new HeroSkillFormat(HeroSaveData.SkillSaveDataList[0]);
//
//
//    SkillDataManager.Instance.CheckSkillCondition (TURN_STATES.DECIDE_INITIATIVE_SKILL, TestData);
//    SkillDataManager.Instance.ActiveEffect (TURN_STATES.CALCULATE_ATTACK_POWER, TestData, null);
    //Debug.Log (TestData.SkillList [0].SkillCondition);

//    StartCoroutine (ParentCoroutine ());
//
//    ActionTester += T2.Output;
//

//    BattleStringFormatTableReader.Instance.DefaultCachedList.ForEach (item => {
//      Debug.Log(JsonUtility.ToJson(item,true));
//    });

//    SkillStringsTableReader.Instance.DefaultCachedList.ForEach (item => {
//      Debug.Log(JsonUtility.ToJson(item,true));
//    });

    HangUpConstTableReader.Instance.DefaultCachedList.ForEach (item => {
      Debug.Log(JsonUtility.ToJson(item,true));
    });

    HangUpRewardTableReader.Instance.DefaultCachedList.ForEach (item => {
      Debug.Log(JsonUtility.ToJson(item,true));
    });

    HangUpStringFormatTableReader.Instance.DefaultCachedList.ForEach (item => {
      Debug.Log(JsonUtility.ToJson(item,true));
    });

    //var _test = new MultiLangString<StringsTable> (StringsTableReader.Instance.FindID (STRINGS_LABEL.AUDIO_BLOCK), StringsTableReader.Instance);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void InvokeAction()
  {
    ActionTester.Invoke ();
  }

  public IEnumerator ParentCoroutine()
  {
    Debug.Log ("ParentCoroutine Start");

    yield return SubCoroutine ();

    Debug.Log ("ParentCoroutine End");
    yield break;
  }

  public IEnumerator SubCoroutine()
  {
    Debug.Log ("SubCoroutine Start");

    Debug.Log ("SubCoroutine End");
    yield return null;
  }

  public void SaveHeroData()
  {
    /*
    CTest _test = new CTest ();
    _test.b = null;
    _test.a = new CTemp();
    string _str = JsonUtility.ToJson (_test);
    Debug.Log (_str);
    _test = JsonUtility.FromJson<CTest>(_str);
    Debug.Log (JsonUtility.ToJson(_test));
    */

    //Debug.Log (_test.TestList [1]);
//    HeroDataFormat data = new HeroDataFormat();
//    data.HistoryID = 0;
//    data.TableID = 123;
//    data.STR = 10;
//    data.VIT = 11;
//    data.INT = 12;
//    data.SPD = 13;
//    UserData.Instance.SaveHeroData(0, data);


    var _userSaveData = new UserSaveDataFormat();
    _userSaveData.UserSaveDataBasic = new UserSaveDataBasicFormat ();
    _userSaveData.UserSaveDataBasic.Aura = 0;
    _userSaveData.UserSaveDataBasic.DimensionChip = 0;
    _userSaveData.HeroSaveDataList = new List<HeroSaveDataFormat> ();

    HeroSaveDataFormat _hero0 = new HeroSaveDataFormat ();
    _hero0.DBHeroID = 0;
//    _hero0.DBNameIDArray = 11;
    _hero0.EXP = 0;
    _hero0.Level = 1;
    _hero0.SlotID = 0;
    _hero0.INT = 123;
    _hero0.DEX = 456;
    _hero0.VIT = 333;
    _hero0.STR = 999;

    _userSaveData.HeroSaveDataList.Add (_hero0);

    UserSaveDataManager.Instance.UserData = _userSaveData;


//    UserDataManager.Instance.Aura = 9999;
  }

  public void LoadHeroData(int slot)
  {
    var _hero = HeroDataManager.Instance.GetHeroData (slot);

    Debug.Log (JsonUtility.ToJson(_hero, true));

//    Debug.Log (UserDataManager.Instance.UserData);
//    Debug.Log (UserDataManager.Instance.DataExsit);
  }

  public void Clear()
  {
    UserSaveDataManager.Instance.Clear ();
    Debug.Log (null);
    //PlayerPrefs.DeleteAll ();
  }
}
