using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.TableClass.Equipment;
using ConstCollections.PJConstOthers;
using ConstCollections.PJEnums;
using ConstCollections.PJConstStrings;
using DataManagement.TableClass;
using Common;
using Equipment.Views;

namespace Equipment.Controllers{

  public class EquipmentReinforceLevelButtonGroupController : MonoBehaviour {

    public EquipmentReinforcementManager EquipmentReinforcementManagerScript;

    public EquipmentReinforceLevelSelectButtonView[] ReinforceSelectNormalButtonArray;

    public EquipmentReinforceLevelSelectButtonView ReinforceSelectSpecialButton;

    public Sprite ButtonSelectedSprite;
    public Sprite ButtonUnSelectedSprite;

    void OnEnable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnResourceChanged;
    }

    void OnDisable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnResourceChanged;
    }

    // Use this for initialization
    public void Init () 
    {
      this.reinforceCostDataList = EquipmentReinforceCostTableReader.Instance.DefaultCachedList;

      ReinforceSelectSpecialButton.Init (GetMultiLangStringByReinforceLevel(3));
      ReinforceSelectSpecialButton.Display ();
      for (int i = 0; i < ReinforceSelectNormalButtonArray.Length; i++) 
      {
        
        ReinforceSelectNormalButtonArray [i].Init (GetMultiLangStringByReinforceLevel(i));
        ReinforceSelectNormalButtonArray [i].Display ();

      }

      SetAllButtonUnclickable ();

      int _auraNow = UserSaveDataManager.Instance.Aura;
      int _dimensionChipNow = UserSaveDataManager.Instance.DimensionChip;
      IsResourceEnough (_auraNow,_dimensionChipNow);

    }

    public void IsResourceEnough(int aura,int dimensionChip)
    {
      if (EquipmentReinforcementManagerScript.isReinforceLevelMaxFlag)
        return;

      int _normalButtonClickAbleNum = 0;

      if (aura >= this.reinforceCostDataList[0].CostNumber) 
      {
        _normalButtonClickAbleNum += 1;
      }
      if (aura >= this.reinforceCostDataList[1].CostNumber) 
      {
        _normalButtonClickAbleNum += 1;
      }
      if (aura >= this.reinforceCostDataList[2].CostNumber) 
      {
        _normalButtonClickAbleNum += 1;
      }

      for (int i = 0; i < _normalButtonClickAbleNum; i++) 
      {
        ReinforceSelectNormalButtonArray [i].ButtonClickAbleOrNot (true);
      }

      int _level = _normalButtonClickAbleNum - 1;

      if (dimensionChip >= this.reinforceCostDataList [3].CostNumber) 
      {
        ReinforceSelectSpecialButton.ButtonClickAbleOrNot (true);
        _level = 3;
      }

      //set default one to be the bigest one 
      if(_level > 0)
        EquipmentReinforceLevelSelect (_level);
    }


    public void EquipmentReinforceLevelSelect(int level)
    {
      EquipmentReinforcementManagerScript.SelectedLevel = level;
      ReinforceSelectSpecialButton.SetSprite(ButtonUnSelectedSprite);
      for (int i = 0; i < ReinforceSelectNormalButtonArray.Length; i++) 
      {
        ReinforceSelectNormalButtonArray [i].SetSprite (ButtonUnSelectedSprite);
      }
      if (level == 3) {
        ReinforceSelectSpecialButton.SetSprite (ButtonSelectedSprite);
      }
      else 
      {
        ReinforceSelectNormalButtonArray [level].SetSprite (ButtonSelectedSprite);
      }

    }

    public void SetAllButtonUnclickable()
    {
      for (int i = 0; i < ReinforceSelectNormalButtonArray.Length; i++) 
      {
        ReinforceSelectNormalButtonArray [i].ButtonClickAbleOrNot (false);
      }

      ReinforceSelectSpecialButton.ButtonClickAbleOrNot (false);
    }

    MultiLangString<StringsTable> GetMultiLangStringByReinforceLevel(int level)
    {
      STRINGS_LABEL _labelSpecial = EquipmentString.EquipmentReinforceLevelStringDic[level];
      var _labelID = StringsTableReader.Instance.FindID (_labelSpecial);
      int _costValue = ExchangeCostValueForDisplay (this.reinforceCostDataList [level].CostNumber);
      MultiLangString<StringsTable> _multi = new MultiLangString<StringsTable> (_labelID,StringsTableReader.Instance,_costValue);
      return _multi;
    }

    int ExchangeCostValueForDisplay(int costNumber)
    {
      int _param = 0;
      if (costNumber < EquipmentOthers.TEN_THOUSAND) 
      {
        _param = costNumber;
      }
      else if(costNumber >= EquipmentOthers.TEN_THOUSAND)
      {
        _param = costNumber/EquipmentOthers.TEN_THOUSAND;
      }
      return _param;
    }

    void OnResourceChanged(UserSaveDataBasicFormat userSaveData)
    {
      int _auraNow = userSaveData.Aura;
      int _dimensionChipNow = userSaveData.DimensionChip;

      SetAllButtonUnclickable ();
      IsResourceEnough (_auraNow,_dimensionChipNow);
    }


    List<EquipmentReinforceCostTable> reinforceCostDataList;
  }
}
