using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Globalization;
using DataManagement.SaveData;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData.FormatCollection.Battle;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.Battle;
using DataManagement.GameData.FormatCollection.Common.HangUp;
using DataManagement.TableClass.HangUp;
using ConstCollections.PJConstStrings;

namespace GameFlow.HangUp.Controller
{
  public class HangUpManager : MonoBehaviour 
  {
    public GameObject RewardPrefab;
    public GameObject ConnectionErrorPrefab;

    [ReadOnly]
    public string TimeURL = "http://www.microsoft.com";
    [ReadOnly]
    public int BaseYear = 2017;
    [ReadOnly]
    public int BaseMonth = 1;
    [ReadOnly]
    public int BaseDay = 1;

    [ReadOnly]
    public BattleDataFormat BattleData;
    [ReadOnly]
    public HangUpConstTable ConstValues;

    public DateTime? UTCTimePrev;
    public DateTime? UTCTime;

    [ReadOnly]
    public bool IsGameOver;

    // Use this for initialization
    IEnumerator Start () 
    {
      this.IsGameOver = false;
      this.battleInfoManagerScript = FindObjectOfType<BattleInfoManager> ();
      this.popManager = FindObjectOfType<PopWindowManager> ();

      this.BattleData = BattleDataManager.Instance.BattleDataCache;
      this.ConstValues = HangUpConstTableReader.Instance.DefaultCachedList [0];

      yield return LoadRewardCoroutine ();
      //yield return SaveTimeCoroutine ();
    }

    void OnApplicationQuit()
    {
      SaveTime ();
    }
      
    public IEnumerator LoadRewardCoroutine()
    {
      var _UTCTime = this.LoadNistUTCTime();

      if (_UTCTime == null) 
      {
        yield return FailedCoroutine ();
        yield break;
      }

      this.UTCTimePrev = _UTCTime.Value;

      var _timeSpan = this.UTCTimePrev.Value.Subtract (new DateTime(this.BaseYear,this.BaseMonth,this.BaseDay));
      Debug.Log (_timeSpan);
      Debug.Log (_timeSpan.TotalSeconds);

      var _oldTime = UserSaveDataManager.Instance.TimespanSeconds;
      if (_oldTime > 0) 
      {
        var _deltaSeconds = _timeSpan.TotalSeconds - _oldTime;
        Debug.Log ("_deltaSeconds : " + _deltaSeconds);

        var _data = GetHangupReward (_deltaSeconds);
        Debug.Log (_data);

        if (this.RewardPrefab != null) 
        {
          this.popManager.ShowWindow (this.RewardPrefab);
        }
      } 

      UserSaveDataManager.Instance.TimespanSeconds = _timeSpan.TotalSeconds;
      Debug.Log ("Init TimespanSeconds : " + UserSaveDataManager.Instance.TimespanSeconds);

      yield break;
    }

    HangUpRewardFormat GetHangupReward(double deltaSeconds)
    {
      var _reward = new HangUpRewardFormat (deltaSeconds, this.BattleData.Area, this.BattleData.Level, this.ConstValues);

      var _globalData = FindObjectOfType<DataManagement.GlobalDataManager> ();
      _globalData.SetValue<double> (HangUpString.DELTA_TIME_SECONDS, deltaSeconds, HangUpString.MEMORY_SPACE);
      _globalData.SetValue<HangUpRewardFormat> (HangUpString.REWARD, _reward, HangUpString.MEMORY_SPACE);

      HeroDataManager.Instance.HeroDataCacheList.ForEach (hero => {
        hero.EXPUp(_reward.GotEXP);
      });

      UserSaveDataManager.Instance.Aura += _reward.GotAura;
      UserSaveDataManager.Instance.DimensionChip += _reward.GotDimensionChip;

      return _reward;
    }

//    void OnGotEXP(HeroAttributeFormat heroAttributes)
//    {
//      FightDataFormat _data = new FightDataFormat (heroAttributes);
//
//      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.GOT_EXP, _data, null, this.gotExp);
//    }
//
//    void OnAllLevelUp(HeroAttributeFormat heroAttributes)
//    {
//      FightDataFormat _data = new FightDataFormat (heroAttributes);
//
//      this.battleInfoManagerScript.EnqueueMessage (INFO_FORMAT_LABEL.LEVEL_UP, _data, null, heroAttributes.Level);
//    }

//    public void SaveTime()
//    {
//      var _timeSpan = this.UTCTimePrev.Value.Subtract (new DateTime(this.BaseYear,this.BaseMonth,this.BaseDay));
//      UserSaveDataManager.Instance.TimespanSeconds = _timeSpan.TotalSeconds;
//      Debug.Log ("Init TimespanSeconds : " + UserSaveDataManager.Instance.TimespanSeconds);
//    }

    void SaveTime()
    {
      var _UTCTime = this.LoadNistUTCTime();

      if (_UTCTime== null) 
      {
        return;
      }

      this.UTCTimePrev = _UTCTime.Value;

      var _timeSpan = this.UTCTimePrev.Value.Subtract (new DateTime(this.BaseYear,this.BaseMonth,this.BaseDay));
      UserSaveDataManager.Instance.TimespanSeconds = _timeSpan.TotalSeconds;
      Debug.Log ("TimespanSeconds : " + UserSaveDataManager.Instance.TimespanSeconds);
    }

    public IEnumerator SaveTimeCoroutine()
    {
      while (!this.IsGameOver) 
      {
        yield return new WaitForSecondsRealtime (this.ConstValues.RefreshTimeSeconds);

        SaveTime ();
      }

      yield break;
    }

    public DateTime? LoadNistUTCTime()
    {
      try 
      {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create(this.TimeURL);
        myHttpWebRequest.Timeout = this.ConstValues.TimeoutMiliseconds;
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        Debug.Log(todaysDates);

        DateTime _time = DateTime.ParseExact(todaysDates, 
          "ddd, dd MMM yyyy HH:mm:ss 'GMT'", 
          CultureInfo.InvariantCulture.DateTimeFormat, 
          DateTimeStyles.AssumeUniversal);

        _time = _time.ToUniversalTime();
        return _time;

      } 
      catch (System.Net.WebException ex) 
      {
        Debug.LogWarning (ex.Status);
        return null;
      }
    }

    IEnumerator FailedCoroutine()
    {
      if (this.ConnectionErrorPrefab != null) 
      {
        this.popManager.ShowWindow (this.ConnectionErrorPrefab);
      }
      yield break;
    } 
      
    BattleInfoManager battleInfoManagerScript;
    PopWindowManager popManager;
  }
}
