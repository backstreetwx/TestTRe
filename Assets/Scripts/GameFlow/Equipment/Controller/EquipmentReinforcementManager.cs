using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.SaveData;
using DataManagement.GameData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using Common;
using DataManagement;
using ConstCollections.PJConstStrings;
using Equipment.Views;
using DataManagement.TableClass.Equipment;
using DataManagement.TableClass;
using ConstCollections.PJEnums;
using ConstCollections.PJConstOthers;
using ConstCollections.PJEnums.Equipment;

namespace Equipment.Controllers{

  public class EquipmentReinforcementManager : MonoBehaviour {

    public PopEquipmentManager PopEquipmentManagerScript;
    public EquipmentLabelController LabelControllerScript;
    public EquipmentReinforceButtonView ReinforceButton;
    public EquipmentReinforceLevelButtonGroupController ButtonGroupControllerScript;

    public EquipmentReinforceSuccessRateView ReinforceSuccessRateView;

    public int SelectedLevel;

    public bool isReinforceLevelMaxFlag;

    void OnEnable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnResourceChanged;
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnResourceChanged;
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }


    void Start ()
    {
      isReinforceLevelMaxFlag = false;
      reinforceCostList = EquipmentReinforceCostTableReader.Instance.DefaultCachedList;
      SelectedLevel = -1;
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      PopEquipmentManagerScript = FindObjectOfType<PopEquipmentManager> ();
      var _equipmentReinforceData =  globalDataManager.GetValue<EquipmentReinforceData> (EquipmentString.EQUIPMENT_REINFORCE_DATA,EquipmentString.MEMORY_SPACE);
      this.heroData = _equipmentReinforceData.HeroData;
      this.heroEquipment = _equipmentReinforceData.HeroEquipmentData;
      this.reinforceSuccessRate = EquipmentDataManager.Instance.GetEquipmentReinforceSuccessRate (this.heroEquipment.ReinforcementLevel);
      LabelControllerScript.Init ();
      LabelControllerScript.LoadDataToDisplay (this.heroEquipment);
      ButtonGroupControllerScript.Init ();
      ReinforceButton.Init ();
      ReinforceSuccessRateView.Init ();
      ushort _labelID = StringsTableReader.Instance.FindID (STRINGS_LABEL.EQUIPMENT_REINFORCEMENT_SUCCESS_RATE_LABEL);
      MultiLangString<StringsTable> _multiLang = new MultiLangString<StringsTable> (_labelID,StringsTableReader.Instance,this.reinforceSuccessRate);
      ReinforceSuccessRateView.SetReinforceSuccessMultiString (_multiLang);
      ReinforceSuccessRateView.Display ();
      IsReinforceLevelMax (this.heroEquipment.ReinforcementLevel);
      var _resourceList = EquipmentReinforceCostTableReader.Instance.DefaultCachedList;
      this.minAuraDemand = _resourceList [0].CostNumber;
      this.minDimensionChipDemand = _resourceList [3].CostNumber;
      int _auraNow = UserSaveDataManager.Instance.Aura;
      int _auraDimensionChip = UserSaveDataManager.Instance.DimensionChip;
      IsResourceEnough (_auraNow,_auraDimensionChip);
    }


    public void EquipmentReinforce()
    {
      if (SelectedLevel != -1) 
      {
        var _costValueData = this.reinforceCostList [SelectedLevel];

        switch(_costValueData.CostType)
        {
        case EQUIPMENT_REINFORCE_COST_TYPE.AURA:
          UserSaveDataManager.Instance.Aura -= _costValueData.CostNumber;
          break;
        case EQUIPMENT_REINFORCE_COST_TYPE.DIMENSIONCHIP:
          UserSaveDataManager.Instance.DimensionChip -= _costValueData.CostNumber;
          break;
        }

        int _reinforceRand = Random.Range (1,101);
        if (_reinforceRand <= this.reinforceSuccessRate) 
        {
          var _reinforcedEquipment = EquipmentDataManager.Instance.GetEquipmentAfterReinforced (SelectedLevel,this.heroEquipment);
          this.heroEquipment = _reinforcedEquipment;

          for(int i = 0;i<this.heroData.EquipmentList.Count; i++)
          {
            if (this.heroData.EquipmentList [i].EquipmentType == this.heroEquipment.EquipmentType) 
            {
              this.heroData.EquipmentList [i] = this.heroEquipment;
            }
          }

          HeroSaveDataManager.Instance.Overwrite (this.heroData);

        } 
        else 
        {
          //FIXME: show success or Failure animation
          Debug.Log ("reinforce false");
        }

      }
    }


    public void Back()
    {
      PopEquipmentManagerScript.Close ();
    }

    void IsReinforceLevelMax(int level)
    {
      if (level == EquipmentOthers.EQUIPMENT_REINFORCE_MAX_LEVEL) 
      {
        this.isReinforceLevelMaxFlag = true;
        ReinforceButton.ReinforceButtonClickAbleOrNot (false);
        ButtonGroupControllerScript.SetAllButtonUnclickable ();
        ReinforceSuccessRateView.Clear ();
      }
    }


    void OnResourceChanged(UserSaveDataBasicFormat userSaveData)
    {
      int _auraNow = userSaveData.Aura;
      int _dimensionChipNow = userSaveData.DimensionChip;
      IsResourceEnough (_auraNow,_dimensionChipNow);
    }

    void IsResourceEnough(int aura,int dimensionChip)
    {
      if (this.isReinforceLevelMaxFlag)
        return;
      if (aura < this.minAuraDemand&&dimensionChip< this.minDimensionChipDemand) 
      {
        ReinforceButton.ReinforceButtonClickAbleOrNot (false);
      } 
      else 
      {
        ReinforceButton.ReinforceButtonClickAbleOrNot (true);
      }
    }

    void OnHeroCacheChanged(int slotID,HeroDataFormat heroDataCache)
    {
      if (this.heroData == null)
        return;
      if (this.heroData.Attributes.SlotID != slotID)
        return;

      this.heroData = heroDataCache.CloneEx ();

      if (this.heroData.EquipmentList != null && this.heroData.EquipmentList.Count > 0) 
      {
        for (int i = 0; i < this.heroData.EquipmentList.Count; i++) 
        {
          if (this.heroData.EquipmentList [i].EquipmentType == this.heroEquipment.EquipmentType) 
          {

            this.heroEquipment = this.heroData.EquipmentList [i].CloneEx();
            this.reinforceSuccessRate = EquipmentDataManager.Instance.GetEquipmentReinforceSuccessRate (this.heroEquipment.ReinforcementLevel);
            ReinforceSuccessRateView.SetParam (this.reinforceSuccessRate);
            ReinforceSuccessRateView.Display ();
            LabelControllerScript.LoadDataToDisplay (this.heroEquipment);
            IsReinforceLevelMax (this.heroEquipment.ReinforcementLevel);
          }
        }
      }
    }
    List<EquipmentReinforceCostTable> reinforceCostList;
    int reinforceSuccessRate;
    int minAuraDemand;
    int minDimensionChipDemand;
    GlobalDataManager globalDataManager;
    HeroEquipmentFormat heroEquipment;
    HeroDataFormat heroData;
  }
}
