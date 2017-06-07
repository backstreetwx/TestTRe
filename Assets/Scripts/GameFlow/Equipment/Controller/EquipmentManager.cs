using UnityEngine;
using System.Collections;
using DataManagement;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJConstStrings;

namespace Equipment.Controllers{

  public class EquipmentManager : MonoBehaviour {

    public EquipmentController WeaponController;
    public EquipmentController ArmorController;
    public EquipmentController DecorationsController;

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();

      int? _slotId = globalDataManager.GetNullableValue<int> (EquipmentString.HERO_SLOTID,EquipmentString.MEMORY_SPACE);

      if (_slotId != null) 
      {
        var _heroDataList = HeroDataManager.Instance.HeroDataCacheList;
        for (int i = 0; i < _heroDataList.Count; i++) 
        {
          if (_heroDataList [i].Attributes.SlotID == (int)_slotId)
            heroData = _heroDataList [i];
        }


        WeaponController.Init (heroData);
        ArmorController.Init (heroData);
        DecorationsController.Init (heroData);
        WeaponController.DisplayData ();
        ArmorController.DisplayData ();
        DecorationsController.DisplayData ();
        globalDataManager.RemoveValue(EquipmentString.HERO_SLOTID,EquipmentString.MEMORY_SPACE);
      }

    }

    GlobalDataManager globalDataManager;
    HeroDataFormat heroData;
  }
}
