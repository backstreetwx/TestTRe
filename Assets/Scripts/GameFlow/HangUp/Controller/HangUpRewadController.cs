using UnityEngine;
using System.Collections;
using GameFlow.Battle.Controller;
using ConstCollections.PJConstStrings;
using System;
using DataManagement.TableClass.HangUp;

namespace GameFlow.HangUp.Controller
{
  public class HangUpRewadController : HangUpPopBaseController 
  {
    public HangUpTextController TimeTextController;
    public HangUpTextController AdTextController;

    // Use this for initialization
    protected override void Start () 
    {
      base.Start ();

      var _globalData = FindObjectOfType<DataManagement.GlobalDataManager> ();
      var _data = _globalData.GetValue<double>(HangUpString.DELTA_TIME_SECONDS, HangUpString.MEMORY_SPACE);
      this.InitTimeText (_data);

      var InitAdText = HangUpConstTableReader.Instance.DefaultCachedList [0];
      this.InitAdText (InitAdText.VideoRewardTimes);
    }

    void InitTimeText(double seconds)
    {
      var _timeSpan = TimeSpan.FromSeconds(seconds);
      this.TimeTextController.MultiLangString.UpdateArgs (System.Math.Floor(_timeSpan.TotalHours), _timeSpan.Minutes);
    }

    void InitAdText(int times)
    {
      this.AdTextController.MultiLangString.UpdateArgs (times);
    }
  }
}

