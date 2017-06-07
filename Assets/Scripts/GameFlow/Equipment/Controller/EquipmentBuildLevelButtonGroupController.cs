using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Equipment.Views;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstOthers;
using DataManagement.TableClass.Equipment;

namespace Equipment.Controllers{

  public class EquipmentBuildLevelButtonGroupController : MonoBehaviour {

    public EquipmentLevelSelectedButtonController[] EquipmentLevelSelectedButtonList;

    public EquipmentBuildManager BuildManager;

    public Sprite ButtonSelectedSprite;
    public Sprite ButtonUnSelectedSprite;

    void OnEnable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent += OnAuraChanged;
    }

    void OnDisable()
    {
      UserSaveDataManager.Instance.UserSaveDataBasicEvent -= OnAuraChanged;
    }


    public void Init () 
    {
      this.qualityGradeAuraList = EquipmentQualityGradeAuraTableReader.Instance.DefaultCachedList;
      for (int i = 0; i < EquipmentLevelSelectedButtonList.Length; i++) 
      {
        EquipmentLevelSelectedButtonList [i].Init ();
        EquipmentLevelSelectedButtonList [i].ButtonClickAbleOrNot (false);
      }
      int _auraNow = UserSaveDataManager.Instance.Aura;

      IsResourceEnough (_auraNow);
    }

    public void IsResourceEnough(int aura)
    {
      int _buttonClickAbleNum = 0;

      if (aura >= this.qualityGradeAuraList[0].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[1].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[2].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[3].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[4].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[5].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[6].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }
      if (aura >= this.qualityGradeAuraList[7].CostAura) 
      {
        _buttonClickAbleNum += 1;
      }

      for (int i = 0; i < EquipmentLevelSelectedButtonList.Length; i++) 
      {
        EquipmentLevelSelectedButtonList [i].ButtonClickAbleOrNot (false);
      }

      for (int i = 0; i < _buttonClickAbleNum; i++) 
      {
        EquipmentLevelSelectedButtonList [i].ButtonClickAbleOrNot (true);
      }
      //set default one to be the bigest one 
      if(_buttonClickAbleNum > 0)
        EquipmentQualityGradeSelect (_buttonClickAbleNum-1);
    }

    public void EquipmentQualityGradeSelect(int quialityGrade)
    {
      BuildManager.QualityGrade = quialityGrade;
      for (int i = 0; i < EquipmentLevelSelectedButtonList.Length; i++) 
      {
        EquipmentLevelSelectedButtonList [i].SetSprite (ButtonUnSelectedSprite);
      }

      EquipmentLevelSelectedButtonList [quialityGrade].SetSprite (ButtonSelectedSprite);
    }


    void OnAuraChanged(UserSaveDataBasicFormat userSaveData)
    {
      int _auraNow = userSaveData.Aura;
      IsResourceEnough (_auraNow);
    }

    List<EquipmentQualityGradeAuraTable> qualityGradeAuraList;
  }
}
