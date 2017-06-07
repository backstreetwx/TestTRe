using UnityEngine;
using System.Collections;
using Equipment.Views;
using DataManagement;
using ConstCollections.PJConstStrings;
using ConstCollections.PJEnums.Equipment;
using GameFlow.Battle.Controller;
using ConstCollections.PJEnums.BattleBottom;
using ConstCollections.PJEnums;
using DataManagement.TableClass;

namespace Equipment.Controllers{

  public class GetDimensionChipFromEquipmentManager : MonoBehaviour {

    public GetDimensionChipFromEquipmentMessageView MessageView;
    public DimensionChipAquireNumView ChipNumView;
    public STRINGS_LABEL EquipmentDestroyAcquireMessage;
    public STRINGS_LABEL EquipmentDestroyAcquireNumber;

    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      popEquipmentBuildManager = FindObjectOfType<PopEquipmentBuildManager> ();
      popEquipmentManager = FindObjectOfType<PopEquipmentManager> ();
      battleBottomManager = FindObjectOfType<BattleBottomManager> ();
      var _format = StringsTableReader.Instance.GetString (EquipmentDestroyAcquireMessage);
      MessageView.Init (_format);
      string _equipmentAcquireNumberString = StringsTableReader.Instance.GetString (EquipmentDestroyAcquireNumber);
      ChipNumView.Init (_equipmentAcquireNumberString);
      var _acquireDimensionChipData = globalDataManager.GetValue<AcquireDimensionChipDataFormat> (EquipmentString.EQUIPMENT_DESTROY_ACQUIRE,EquipmentString.MEMORY_SPACE);
      this.nextTo = globalDataManager.GetValue<POP_WINDOW_NEXT_TO> (EquipmentString.POP_WINDOW_NEXT_TO,EquipmentString.MEMORY_SPACE);
      MessageView.AcquireDimensionChipMessage (_acquireDimensionChipData);
      ChipNumView.AcquireDimensionChipNumber (_acquireDimensionChipData);
    }

    public void OKButtonClick()
    {
      switch (this.nextTo) 
      {
      case POP_WINDOW_NEXT_TO.CLOSE:
        this.popEquipmentBuildManager.Close ();
        break;
      case POP_WINDOW_NEXT_TO.BACK:
        this.popEquipmentManager.CloseAllRootWindows();
        break;
      case POP_WINDOW_NEXT_TO.ACHIEVEMENT:
        break;
      case POP_WINDOW_NEXT_TO.MAP:
        break;
      case POP_WINDOW_NEXT_TO.BATTLE:
        this.battleBottomManager.Close ();
        break;
      case POP_WINDOW_NEXT_TO.HERO:
        this.battleBottomManager.ShowWindow (battleBottomManager.HeroInfoPrefab);
        break;
      case POP_WINDOW_NEXT_TO.STONE:
        break;
      case POP_WINDOW_NEXT_TO.SETTINGS:
        this.battleBottomManager.ShowWindow (battleBottomManager.SettingsPrefab);
        break;
      
      }
      globalDataManager.RemoveValue(EquipmentString.POP_WINDOW_NEXT_TO,EquipmentString.MEMORY_SPACE);
    }

    BattleBottomManager battleBottomManager;
    PopEquipmentManager popEquipmentManager;
    PopEquipmentBuildManager popEquipmentBuildManager;
    POP_WINDOW_NEXT_TO nextTo;
    GlobalDataManager globalDataManager;
  }
}
