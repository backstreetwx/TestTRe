using UnityEngine;
using System.Collections;
using DataManagement;
using ConstCollections.PJConstStrings;
using Common;
using ConstCollections.PJEnums.Equipment;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.BattleBottom;
using Equipment.Views;
using ConstCollections.PJEnums;
using DataManagement.TableClass;

namespace Equipment.Controllers{

  public class EquipmentDestroyConfirmManager : MonoBehaviour {

    public EquipmentDestroyDescriptionView View;
    public STRINGS_LABEL EquipmentDestroyDescriptionLabel;

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager>();
      popEquipmentManager = FindObjectOfType<PopEquipmentManager>();
      popEquipmentBuildManager = FindObjectOfType<PopEquipmentBuildManager>();
      equipmentExchangeLabelManager = FindObjectOfType<EquipmentExchangeLabelManager> ();
      equipmentBuildManager = FindObjectOfType<EquipmentBuildManager> ();
      battleBottomManager = FindObjectOfType<BattleBottomManager> ();
      this.nextTo = globalDataManager.GetValue<POP_WINDOW_NEXT_TO> (EquipmentString.POP_WINDOW_NEXT_TO,EquipmentString.MEMORY_SPACE);
      var _type = globalDataManager.GetValue<EQUIPMENT_TYPE> (EquipmentString.EQUIPMENT_TYPE_STRING,EquipmentString.MEMORY_SPACE);
      string _description = StringsTableReader.Instance.GetString (EquipmentDestroyDescriptionLabel);
      STRINGS_LABEL _equipmentLabel = EquipmentString.EquipmentTypeStringDic[_type];
      string _typeString = StringsTableReader.Instance.GetString (_equipmentLabel);

      View.Init (string.Format (_description,_typeString));
    }


    public void DestroyAndJumpToNext()
    {
      switch(this.nextTo)
      {
      case POP_WINDOW_NEXT_TO.BACK:
        {
          popEquipmentBuildManager.Close ();
          equipmentExchangeLabelManager.EquipmentDestroy (POP_WINDOW_NEXT_TO.BACK);
          if (!equipmentBuildManager.GetDimensionChipWhenDestroyEquipment)
            popEquipmentManager.Close ();
          break;
        }
      case POP_WINDOW_NEXT_TO.ACHIEVEMENT:
        break;
      case POP_WINDOW_NEXT_TO.MAP:
        break;
      case POP_WINDOW_NEXT_TO.BATTLE:
        {
          popEquipmentBuildManager.Close ();
          equipmentExchangeLabelManager.EquipmentDestroy (POP_WINDOW_NEXT_TO.BATTLE);
          if (!equipmentBuildManager.GetDimensionChipWhenDestroyEquipment) 
          {
            battleBottomManager.Close ();
          }
          break;
        }
      case POP_WINDOW_NEXT_TO.HERO:
        {
          popEquipmentBuildManager.Close ();
          equipmentExchangeLabelManager.EquipmentDestroy (POP_WINDOW_NEXT_TO.HERO);
          if (!equipmentBuildManager.GetDimensionChipWhenDestroyEquipment) 
          {
            battleBottomManager.ShowWindow (battleBottomManager.HeroInfoPrefab);
          }
          break;
        }
      case POP_WINDOW_NEXT_TO.STONE:
        break;
      case POP_WINDOW_NEXT_TO.SETTINGS:
        {
          popEquipmentBuildManager.Close ();
          equipmentExchangeLabelManager.EquipmentDestroy (POP_WINDOW_NEXT_TO.SETTINGS);
          if (!equipmentBuildManager.GetDimensionChipWhenDestroyEquipment) 
          {
            battleBottomManager.ShowWindow (battleBottomManager.SettingsPrefab);
          }
          break;
        }
      }
    }

    public void Cancel()
    {
      popEquipmentBuildManager.Close ();
    }

    BattleBottomManager battleBottomManager;
    EquipmentBuildManager equipmentBuildManager;
    EquipmentExchangeLabelManager equipmentExchangeLabelManager;
    PopEquipmentManager popEquipmentManager;
    PopEquipmentBuildManager popEquipmentBuildManager;
    POP_WINDOW_NEXT_TO nextTo;
    GlobalDataManager globalDataManager;
  }
}
