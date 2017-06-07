using UnityEngine;
using System.Collections;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using Common;
using DataManagement;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums.Equipment;
using DataManagement.SaveData;
using Equipment.Views;
using DataManagement.TableClass.Equipment;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.BattleBottom;

namespace Equipment.Controllers{

  public class EquipmentExchangeLabelManager : MonoBehaviour {

    public EquipmentLabelController EquipmentTakeOff;
    public AttributeComparisonController AttributeTakeOff;
    public GameObject GetDimensionChipFromEquipmentPrefab;
    public GameObject EquipmentDestroyConfirmPrefab;
    public EquipmentBuildManager EquipmentBuildManagerScript;
    public PopEquipmentBuildManager PopEquipmentBuild;

    public GameObject CurrentAttributeObj;
    public GameObject TakeOffAttributeObj;
    public GameObject CurrentEquipmentObj;
    public GameObject TakeOffEquipmentObj;
    public EQUIPMENT_COMPARISON_TYPE ComparisonType;

    void OnEnable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

    public void Init () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      battleBottomManager = FindObjectOfType<BattleBottomManager> ();
      PopEquipmentBuild = FindObjectOfType<PopEquipmentBuildManager> ();
      AttributeTakeOff.Init ();
      EquipmentTakeOff.Init ();
      ComparisonType = EQUIPMENT_COMPARISON_TYPE.EQUIPMENT_ATTRIBUTE;
    }

    public void EquipmentLoad(EquipmentBuildData _buildData)
    {
      this.heroDataFormat = _buildData.HeroData;
      this.equipmentType = _buildData.EquipmentType;
      this.equipmentTakeOff = _buildData.HeroEquipmentData;
      EquipmentTakeOff.LoadDataToDisplay (this.equipmentTakeOff);
      AttributeTakeOff.DisplayWithUnequippedEquipment (this.heroDataFormat,this.equipmentTakeOff);
    }

    public void EquipmentDestroyButtonClick(string nextToString)
    {
      object _enum = System.Enum.Parse(typeof(POP_WINDOW_NEXT_TO), nextToString, true);
      POP_WINDOW_NEXT_TO nextTo = POP_WINDOW_NEXT_TO.NONE;
      if (_enum == null)
        return;
      else
        nextTo = (POP_WINDOW_NEXT_TO)_enum;
      EquipmentDestroy (nextTo);
    }

    public void EquipmentDestroy(POP_WINDOW_NEXT_TO nextTo)
    {
      EquipmentBuildManagerScript.IsEquipmentExchanging = false;
      EquipmentBuildManagerScript.SetEquipmentBuildLabelState (true);
      battleBottomManager.CallBackStack.Pop ();
      battleBottomManager.State = BUTTON_POP_STATE.CAN_POP;
      int _random = Random.Range (0,100);
      if (_random<this.equipmentTakeOff.DimensionChipOutputProbability) 
      {
        UserSaveDataManager.Instance.DimensionChip += this.equipmentTakeOff.DimensionChipOutput;
        globalDataManager.SetValue<AcquireDimensionChipDataFormat> (EquipmentString.EQUIPMENT_DESTROY_ACQUIRE,new AcquireDimensionChipDataFormat(this.equipmentTakeOff.NameString,this.equipmentTakeOff.DimensionChipOutput),EquipmentString.MEMORY_SPACE);
        globalDataManager.SetValue<POP_WINDOW_NEXT_TO> (EquipmentString.POP_WINDOW_NEXT_TO,nextTo,EquipmentString.MEMORY_SPACE);
        EquipmentBuildManagerScript.GetDimensionChipWhenDestroyEquipment = true;
        PopEquipmentBuild.ShowWindow (GetDimensionChipFromEquipmentPrefab);
      }
      EquipmentBuildUIRecover ();
      this.gameObject.SetActive (false);


    }

    public void EquipmentExchange()
    {
      var _change = this.equipmentTakeOff;
      this.equipmentTakeOff = EquipmentBuildManagerScript.EquipmentBuildData.HeroEquipmentData;
      EquipmentTakeOff.LoadDataToDisplay (this.equipmentTakeOff);

      this.equipmentTakeOn = _change;

      for(int i = 0;i<this.heroDataFormat.EquipmentList.Count; i++)
      {
        if (this.heroDataFormat.EquipmentList [i].EquipmentType == this.equipmentType) 
        {
          this.heroDataFormat.EquipmentList [i] = this.equipmentTakeOn;
        }
      }

      HeroSaveDataManager.Instance.Overwrite (this.heroDataFormat);
    }

    public void EquipmentBuildUIRecover()
    {
      ComparisonType = EQUIPMENT_COMPARISON_TYPE.EQUIPMENT_ATTRIBUTE;
      CurrentEquipmentObj.gameObject.SetActive (true);
      TakeOffEquipmentObj.gameObject.SetActive (true);
      CurrentAttributeObj.gameObject.SetActive (false);
      TakeOffAttributeObj.gameObject.SetActive (false);
    }

    public void EquipmentComparison()
    {
      // change type to the opposite one
      // swtich equipment comparison UI and attribute comparison UI
      if (ComparisonType == EQUIPMENT_COMPARISON_TYPE.CHARACTER_ATTRIBUTE) 
      {
        CurrentEquipmentObj.gameObject.SetActive (true);
        TakeOffEquipmentObj.gameObject.SetActive (true);
        CurrentAttributeObj.gameObject.SetActive (false);
        TakeOffAttributeObj.gameObject.SetActive (false);
        ComparisonType = EQUIPMENT_COMPARISON_TYPE.EQUIPMENT_ATTRIBUTE;
      } 
      else if (ComparisonType == EQUIPMENT_COMPARISON_TYPE.EQUIPMENT_ATTRIBUTE) 
      {
        CurrentEquipmentObj.gameObject.SetActive (false);
        TakeOffEquipmentObj.gameObject.SetActive (false);
        CurrentAttributeObj.gameObject.SetActive (true);
        TakeOffAttributeObj.gameObject.SetActive (true);
        ComparisonType = EQUIPMENT_COMPARISON_TYPE.CHARACTER_ATTRIBUTE;
      }

    }

    void OnHeroCacheChanged(int slotID,HeroDataFormat heroDataCache)
    {
      if (this.heroDataFormat == null)
        return;
      if (this.heroDataFormat.Attributes.SlotID != slotID)
        return;

      this.heroDataFormat = heroDataCache.CloneEx ();
      AttributeTakeOff.DisplayWithUnequippedEquipment (this.heroDataFormat,this.equipmentTakeOff);

    }
    BattleBottomManager battleBottomManager;
    HeroEquipmentFormat equipmentTakeOn;
    HeroEquipmentFormat equipmentTakeOff;
    EQUIPMENT_TYPE equipmentType;
    HeroDataFormat heroDataFormat;
    GlobalDataManager globalDataManager;
  }

  [System.Serializable]
  public class AcquireDimensionChipDataFormat
  {
    public MultiLangString<EquipmentStringsTable> NameString;
    public int DimensionChipNum;

    public AcquireDimensionChipDataFormat(MultiLangString<EquipmentStringsTable> nameString ,int dimensionChipNum)
    {
      this.NameString = nameString;
      this.DimensionChipNum = dimensionChipNum;
    }

  }
}
