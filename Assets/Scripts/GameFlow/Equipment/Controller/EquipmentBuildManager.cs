using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums.Equipment;
using Equipment.Views;
using DataManagement.SaveData;
using ConstCollections.PJConstOthers;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData;
using Common;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.BattleBottom;
using DataManagement.TableClass.Equipment;

namespace Equipment.Controllers{

  public class EquipmentBuildManager : MonoBehaviour {

    public EquipmentLabelController CurrentEquipment;
    public AttributeComparisonController CurrentAttribute;
    public EquipmentBuildLevelButtonGroupController ButtonGroupController;
    public EquipmentBuildButtonView BuildButtonView;
    public int QualityGrade;

    public PopEquipmentBuildManager PopEquipmentBuildManagerScript;
    public PopEquipmentManager PopEquipmentManagerScript;

    public GameObject EquipmentBuildLabel;
    public GameObject EquipmentExchangeLabel;
    public EquipmentBuildData EquipmentBuildData;

    public GameObject EquipmentDestroyConfirmPrefab;

    public EquipmentExchangeLabelManager EquipmentExchangeManagerScript;
    public bool IsEquipmentExchanging;

    public bool GetDimensionChipWhenDestroyEquipment;

    void OnEnable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnAuraChanged;
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnAuraChanged;
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

  	// Use this for initialization
  	void Start () 
    {
      GetDimensionChipWhenDestroyEquipment = false;
      IsEquipmentExchanging = false;
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      battleBottomManager = FindObjectOfType<BattleBottomManager> ();
      PopEquipmentManagerScript = FindObjectOfType<PopEquipmentManager> ();
      this.EquipmentBuildData = globalDataManager.GetValue<EquipmentBuildData> (EquipmentString.EQUIPMENT_BUILD_DATA,EquipmentString.MEMORY_SPACE);
      QualityGrade = -1;
      this.equipmentType = this.EquipmentBuildData.EquipmentType;
      this.heroData = this.EquipmentBuildData.HeroData;
      BuildButtonView.Init ();
      CurrentAttribute.Init ();
      if(EquipmentBuildData.HeroEquipmentData!=null)
        CurrentAttribute.SetDataForDisplay (this.heroData,EquipmentBuildData.HeroEquipmentData);
      CurrentEquipment.Init ();
      CurrentEquipment.LoadDataToDisplay (EquipmentBuildData.HeroEquipmentData);
      ButtonGroupController.Init ();
      BuildButtonView.Init ();
      int _auraNow = UserSaveDataManager.Instance.Aura;
      IsResourceEnough (_auraNow);
      EquipmentExchangeManagerScript.Init ();  
     
  	}
  	
    public void BuidEquipment()
    {
      if (QualityGrade != -1) 
      {
        int _aura = EquipmentQualityGradeAuraTableReader.Instance.GetCostAura (QualityGrade);

        var _newEquipment = EquipmentDataManager.Instance.GetRandomEquipmentByQualityGrade (this.equipmentType, QualityGrade);
        UserSaveDataManager.Instance.Aura -= _aura;
        //if no equipment, take on the equipment directly
        if (this.EquipmentBuildData.HeroEquipmentData == null) 
        {
          this.heroData.EquipmentList.Add(_newEquipment);
          HeroSaveDataManager.Instance.Overwrite (this.heroData);
        } 
        else 
        {
          SetEquipmentBuildLabelState (false);
          IsEquipmentExchanging = true;
          battleBottomManager.SetState (BUTTON_POP_STATE.CAN_NOT_POP,PopDestroyConfirmWindow);
          EquipmentBuildData _equipmentBuildData = new EquipmentBuildData(this.equipmentType,this.heroData,_newEquipment);
          EquipmentExchangeLabel.gameObject.SetActive (true);
          EquipmentExchangeManagerScript.EquipmentLoad (_equipmentBuildData);
        }
      }
    }

    public void EquipmentBuildBack()
    {
      if (!IsEquipmentExchanging) 
      {
        PopEquipmentManagerScript.Close ();
      }
      else if(IsEquipmentExchanging)
      {
        PopDestroyConfirmWindow (POP_WINDOW_NEXT_TO.BACK);
      }
    }

    public void PopDestroyConfirmWindow(POP_WINDOW_NEXT_TO nextTo = POP_WINDOW_NEXT_TO.BACK)
    {
      globalDataManager.SetValue<POP_WINDOW_NEXT_TO> (EquipmentString.POP_WINDOW_NEXT_TO,nextTo,EquipmentString.MEMORY_SPACE);
      globalDataManager.SetValue<EQUIPMENT_TYPE> (EquipmentString.EQUIPMENT_TYPE_STRING,this.equipmentType,EquipmentString.MEMORY_SPACE);
      PopEquipmentBuildManagerScript.ShowWindow (EquipmentDestroyConfirmPrefab);

    }

    public void SetEquipmentBuildLabelState(bool state)
    {
      EquipmentBuildLabel.gameObject.SetActive (state);
    }

    void IsResourceEnough(int aura)
    {
      if (aura < EquipmentQualityGradeAuraTableReader.Instance.GetCostAura (0)) 
      {
        BuildButtonView.BuildButtonClickAbleOrNot (false);
      } 
      else 
      {
        BuildButtonView.BuildButtonClickAbleOrNot (true);
      }
    }

    void OnAuraChanged(UserSaveDataBasicFormat userSaveData)
    {
      int _auraNow = userSaveData.Aura;
      IsResourceEnough (_auraNow);
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
          if (this.heroData.EquipmentList [i].EquipmentType == this.equipmentType) 
          {
            
            this.EquipmentBuildData.HeroEquipmentData = this.heroData.EquipmentList [i].CloneEx();

            CurrentEquipment.LoadDataToDisplay (this.EquipmentBuildData.HeroEquipmentData);
            CurrentAttribute.SetDataForDisplay (this.heroData,this.EquipmentBuildData.HeroEquipmentData);
          }
        }
      }
    }


    BattleBottomManager battleBottomManager;
    HeroEquipmentFormat heroEquipment;
    HeroDataFormat heroData;
    EQUIPMENT_TYPE equipmentType;
    GlobalDataManager globalDataManager;
  }

  [System.Serializable]
  public class EquipmentExchangeData
  {
    public EquipmentBuildData EquipmentBuild;
    public int QualityGrade;


    public EquipmentExchangeData(EquipmentBuildData equipmentBuild ,int qualityGrade)
    {
      this.EquipmentBuild = equipmentBuild;
      this.QualityGrade = qualityGrade;
    }

  }
}

