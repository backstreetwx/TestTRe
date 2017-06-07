using UnityEngine;
using System.Collections;
using DataManagement.GameData;
using GameFlow.Battle.View;

namespace GameFlow.Battle.Controller{

  public class BattleAreaBackgroundManager : MonoBehaviour {

    public BattleAreaBackgroundView AreaBackgroundViewScript;

    void OnEnable()
    {
      BattleDataManager.Instance.BattleAreaLevelChangedEvent += AreaLevelChanged;
    }

    void OnDisable()
    {
      BattleDataManager.Instance.BattleAreaLevelChangedEvent -= AreaLevelChanged;
    }

    void Start () 
    {
      var _battleBGPath = BattleDataManager.Instance.GetBattleBGPath();
      AreaBackgroundViewScript.SetAreaBG (_battleBGPath);
    }

    void AreaLevelChanged(short area,short level)
    {
      var _battleBGPath = BattleDataManager.Instance.GetBattleBGPath(area, level);
      AreaBackgroundViewScript.SetAreaBG (_battleBGPath);
    }

  }
}
