using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Equipment.Views;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJEnums.Equipment;
using DataManagement.GameData;

namespace HeroInfo.Controllers{

  public class EquipmentLayoutDisplayController : MonoBehaviour {

    public EquipmentIconView WeaponIconView;
    public EquipmentIconView ArmorIconView;
    public EquipmentIconView AccessoryIconView;

    // Use this for initialization
    public void Init () 
    {
      WeaponIconView.Init ();
      ArmorIconView.Init ();
      AccessoryIconView.Init ();
      //Set default Icon
      this.weaponDefaultTexturePath = EquipmentDataManager.Instance.GetDefaultTexturePathByEquipmentType (EQUIPMENT_TYPE.WEAPON);
      this.armorDefaultTexturePath = EquipmentDataManager.Instance.GetDefaultTexturePathByEquipmentType (EQUIPMENT_TYPE.ARMOR);
      this.accessoryDefaultTexturePath = EquipmentDataManager.Instance.GetDefaultTexturePathByEquipmentType (EQUIPMENT_TYPE.DECORATIONS);
      WeaponIconView.ShowEquipmentIcon (this.weaponDefaultTexturePath,0);
      ArmorIconView.ShowEquipmentIcon (this.armorDefaultTexturePath,0);
      AccessoryIconView.ShowEquipmentIcon (this.accessoryDefaultTexturePath,0);
    }

    public void SetDataForDisplay(HeroDataFormat heroDataFormat)
    {
      equipmentList = heroDataFormat.EquipmentList;

      WeaponIconView.ShowEquipmentIcon (this.weaponDefaultTexturePath,0);
      ArmorIconView.ShowEquipmentIcon (this.armorDefaultTexturePath,0);
      AccessoryIconView.ShowEquipmentIcon (this.accessoryDefaultTexturePath,0);

      SetEquipmentIcon (equipmentList);
    }


    void SetEquipmentIcon(List<HeroEquipmentFormat> equipmentList)
    {
      if (equipmentList.Count > 0) 
      {
        for (int i = 0; i < equipmentList.Count; i++) 
        {
          switch(equipmentList[i].EquipmentType)
          {
          case EQUIPMENT_TYPE.WEAPON:
            {
              WeaponIconView.ShowEquipmentIcon (equipmentList[i].TexturePath,equipmentList[i].TextureIconID);
              break;
            }
          case EQUIPMENT_TYPE.ARMOR:
            {
              ArmorIconView.ShowEquipmentIcon (equipmentList[i].TexturePath,equipmentList[i].TextureIconID);
              break;
            }
          case EQUIPMENT_TYPE.DECORATIONS:
            {
              AccessoryIconView.ShowEquipmentIcon (equipmentList[i].TexturePath,equipmentList[i].TextureIconID);
              break;
            }

          }
        }
      }
    }

    string weaponDefaultTexturePath;
    string armorDefaultTexturePath;
    string accessoryDefaultTexturePath;

    List<HeroEquipmentFormat> equipmentList;
  }
}
