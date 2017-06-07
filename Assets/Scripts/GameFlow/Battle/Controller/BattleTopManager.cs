using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using Common.Controller;

namespace GameFlow.Battle.Controller
{

  public class BattleTopManager : MonoBehaviour
  {
    public CommonTextController AuraTextController;
    public CommonTextController DimensionChipTextController;

    void OnEnable () 
    {
      // Add listener to UserData
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnAuraChanged;
      AuraTextController.MultiLangString.BindArgs (UserSaveDataManager.Instance.Aura);

      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnDimensionChipChanged;
      DimensionChipTextController.MultiLangString.BindArgs (UserSaveDataManager.Instance.DimensionChip);
    }

    void OnDisable() 
    {
      // Remove listener from UserData
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnAuraChanged;
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnDimensionChipChanged;
    }
      
    void OnAuraChanged(UserSaveDataBasicFormat userSaveDataBasic)
    {
      AuraTextController.MultiLangString.UpdateArgs (userSaveDataBasic.Aura);
    }

    void OnDimensionChipChanged(UserSaveDataBasicFormat userSaveDataBasic)
    {
      DimensionChipTextController.MultiLangString.UpdateArgs (userSaveDataBasic.DimensionChip);
    }
  }
}
