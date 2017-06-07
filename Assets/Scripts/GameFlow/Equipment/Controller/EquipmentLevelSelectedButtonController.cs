using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ConstCollections.PJConstOthers;
using DataManagement.TableClass;
using ConstCollections.PJEnums;
using DataManagement.TableClass.Equipment;

namespace Equipment.Controllers{

  public class EquipmentLevelSelectedButtonController : MonoBehaviour {

    public int QualityGrade;
    // Use this for initialization
    public void Init () 
    {
      selfButton = GetComponent<Button> ();
      selfImage = GetComponent<Image> ();
      buttonText = GetComponentInChildren<Text> ();
      int _aura = EquipmentQualityGradeAuraTableReader.Instance.GetCostAura (QualityGrade);
      string _format = "";
      if (_aura < EquipmentOthers.TEN_THOUSAND) 
      {
        _format = StringsTableReader.Instance.GetString (STRINGS_LABEL.EQUIPMENT_BUILD_LEVEL_LESS_THAN_TEN_THOUSAND_BUTTON);

        buttonText.text = string.Format (_format,_aura);
      }
      else if(_aura>=EquipmentOthers.TEN_THOUSAND)
      {
        _format = StringsTableReader.Instance.GetString (STRINGS_LABEL.EQUIPMENT_BUILD_LEVEL_MORE_THAN_TEN_THOUSAND_BUTTON);

        buttonText.text = string.Format (_format,_aura/EquipmentOthers.TEN_THOUSAND);
      }

    }

    public void ButtonClickAbleOrNot(bool clickAble)
    {
      if (clickAble) 
      {
        selfButton.interactable = true;
      }
      else if(!clickAble)
      {
        selfButton.interactable = false;
      }
    }

    public void SetSprite(Sprite sprite)
    {
      selfImage.sprite = sprite;
    }
    Text buttonText;
    Image selfImage;
    Button selfButton;
  }
}
