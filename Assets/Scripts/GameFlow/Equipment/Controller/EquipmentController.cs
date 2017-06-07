using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Equipment.Views;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJEnums.Equipment;
using DataManagement;
using ConstCollections.PJConstStrings;
using DataManagement.GameData;
using Common;
using ConstCollections.PJEnums;
using DataManagement.TableClass;
using DataManagement.TableClass.Equipment;
using DataManagement.SaveData.FormatCollection;

namespace Equipment.Controllers{

  public class EquipmentController : MonoBehaviour {

    public EQUIPMENT_TYPE EquipmentType;
    public STRINGS_LABEL NoEquipment;

    public EquipmentIconView EquipmentIcon;
    public EquipmentNameView EquipmentName;
    public EquipmentAttributeNumView ReinforcementLevel;

    public EquipmentAttributeController[] AttributeList;

    public ChangeButtonController ChangeButton;
    public ReinforceButtonView ReinforceButton;

    public PopEquipmentManager PopEquipmentManager;
    public GameObject EquipmentBuildPrefab;
    public GameObject EquipmentReinforcementPrefab;

    void OnEnable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent += OnHeroCacheChanged;
    }

    void OnDisable()
    {
      HeroDataManager.Instance.HeroDataCacheChangedEvent -= OnHeroCacheChanged;
    }

    // Use this for initialization
    public void Init (HeroDataFormat heroData) 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      this.selfHeroData = heroData;
      EquipmentIcon.Init ();
      EquipmentIcon.IconDisplayOrNot (false);
      var _noEquipment = StringsTableReader.Instance.GetString (NoEquipment);
      EquipmentName.Init (_noEquipment);
      EquipmentName.NoEquipmentMessage();

      ChangeButton.Init ();
      ChangeButton.EquipmentExistOrNot (false);

      ReinforceButton.Init ();
      ReinforceButton.ButtonDisplayOrNot (false);

      //hero jumped
      if (!this.selfHeroData.Attributes.Active) 
      {
        ChangeButton.gameObject.SetActive (false);
        ReinforceButton.gameObject.SetActive (false);
      }

      var _textFormat = EquipmentOtherValueTableReader.Instance.DefaultCachedList[0].Format;
      ReinforcementLevel.Init (_textFormat);

      this.selfEquipmentData = null;

      for (int i = 0; i < AttributeList.Length; i++) 
      {
        AttributeList [i].Init ();
      }
      ClearOldData ();
    }

    public void DisplayData()
    {
      if (this.selfHeroData.EquipmentList != null && this.selfHeroData.EquipmentList.Count > 0) 
      {
        for (int i = 0; i < this.selfHeroData.EquipmentList.Count; i++) 
        {
          if (this.selfHeroData.EquipmentList [i].EquipmentType == EquipmentType) 
          {
            
            EquipmentIcon.IconDisplayOrNot (true);
            EquipmentIcon.ShowEquipmentIcon (this.selfHeroData.EquipmentList [i].TexturePath,this.selfHeroData.EquipmentList [i].TextureIconID);
            var _equipmentString = new MultiLangString<EquipmentStringsTable> ((ushort)this.selfHeroData.EquipmentList [i].DBEquipmentID, EquipmentStringsTableReader.Instance);
            EquipmentName.ShowEquipmentName (_equipmentString);

            //only the hero who is active can change equipments
            if (this.selfHeroData.Attributes.Active) 
            {
              ChangeButton.gameObject.SetActive (true);
              ReinforceButton.gameObject.SetActive (true);
              ChangeButton.EquipmentExistOrNot (true);
              ReinforceButton.ButtonDisplayOrNot (true);
            }

            this.selfEquipmentData = this.selfHeroData.EquipmentList [i];
            if (this.selfEquipmentData.ReinforcementLevel > 0)
              ReinforcementLevel.ShowAttributeNum (this.selfEquipmentData.ReinforcementLevel,true);

            var _attributeBaseList = this.selfHeroData.EquipmentList [i].EquipmentAttributeBaseList;
            var _attributeOffsetList = this.selfHeroData.EquipmentList [i].EquipmentAttributeOffsetList;
            for (int j = 0; j < _attributeBaseList.Count; j++) 
            {
              EquipmentAttribute _attributeOffset = new EquipmentAttribute (_attributeBaseList[j].AttributeType,0);

              for (int k = 0; k < _attributeOffsetList.Count; k++) 
              {
                if (_attributeOffsetList [k].AttributeType == _attributeBaseList [j].AttributeType)
                  _attributeOffset.Attribute += _attributeOffsetList [k].Attribute;
              }

              AttributeList [j].AttributeDisplay (_attributeBaseList[j],_attributeOffset);
            }
          }
        }
      }
    }

    public void ClearOldData ()
    {
      for (int i = 0; i < AttributeList.Length; i++) 
      {
        AttributeList [i].DataClear ();
      }
      ReinforcementLevel.Clear ();
    }

    public void EquipmentBuild()
    {
      EquipmentBuildData _equipmentBuildData = new EquipmentBuildData (this.EquipmentType,this.selfHeroData,this.selfEquipmentData);
      globalDataManager.SetValue<EquipmentBuildData> (EquipmentString.EQUIPMENT_BUILD_DATA, _equipmentBuildData,EquipmentString.MEMORY_SPACE);
      PopEquipmentManager.ShowWindow (EquipmentBuildPrefab);
    }

    public void EquipmentReinforce()
    {
      EquipmentReinforceData _equipmentReinforceData = new EquipmentReinforceData (this.selfHeroData,this.selfEquipmentData);
      globalDataManager.SetValue<EquipmentReinforceData> (EquipmentString.EQUIPMENT_REINFORCE_DATA, _equipmentReinforceData,EquipmentString.MEMORY_SPACE);
      PopEquipmentManager.ShowWindow (EquipmentReinforcementPrefab);
    }

    void OnHeroCacheChanged(int slotID, HeroDataFormat heroCache)
    {
      if (this.selfHeroData == null)
        return;
      if (this.selfHeroData.Attributes.SlotID != slotID)
        return;

      this.selfHeroData = heroCache.CloneEx ();
      ClearOldData ();
      DisplayData ();
    }

    GlobalDataManager globalDataManager;
    HeroEquipmentFormat selfEquipmentData;
    HeroDataFormat selfHeroData;
  }

  [System.Serializable]
  public class EquipmentBuildData
  {
    public EQUIPMENT_TYPE EquipmentType;
    public HeroDataFormat HeroData;
    public HeroEquipmentFormat HeroEquipmentData;

    public EquipmentBuildData(){
    }

    public EquipmentBuildData(EQUIPMENT_TYPE equipmentType, HeroDataFormat heroData ,HeroEquipmentFormat heroEquipmentData)
    {
      this.EquipmentType = equipmentType;
      this.HeroData = heroData;
      this.HeroEquipmentData = heroEquipmentData;
    }

  }

  [System.Serializable]
  public class EquipmentReinforceData
  {
    public HeroDataFormat HeroData;
    public HeroEquipmentFormat HeroEquipmentData;

    public EquipmentReinforceData(HeroDataFormat heroData ,HeroEquipmentFormat heroEquipmentData)
    {
      this.HeroData = heroData;
      this.HeroEquipmentData = heroEquipmentData;
    }
  }
}
