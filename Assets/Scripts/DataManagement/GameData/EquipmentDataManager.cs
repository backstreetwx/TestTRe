using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJEnums.Equipment;
using DataManagement.TableClass.Equipment;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstOthers;


namespace DataManagement.GameData{

  public class EquipmentDataManager : Singleton<EquipmentDataManager> {

    public int CalculateAttributeOffSet(int parameterA, int parameterB)
    {
      int _random1 = Random.Range (0,parameterB);
      int _random2 = Random.Range (0,parameterB);
      return (parameterA + (_random1 + _random2) / 2);
    }

    public HeroEquipmentFormat GetRandomEquipmentByQualityGrade(EQUIPMENT_TYPE equipmentType,int quality)
    {
      var _equipmentTableList = EquipmentTableReader.Instance.FindDefault (equipmentType,(short)quality);

      int _weightMax = 0;
      for (int i = 0; i < _equipmentTableList.Count; i++) 
      {
        _weightMax += _equipmentTableList [i].Weights;
      }

      int _random = Random.Range (1,_weightMax+1);

      EquipmentTable _equipmentTable = null;
      int point = 0;
      for(int i = 0; i < _equipmentTableList.Count; i++)
      {
        
        if (point <= _random && _random <= point + _equipmentTableList [i].Weights) 
        {
          _equipmentTable = _equipmentTableList [i].CloneEx ();
          break;
        }
        point += _equipmentTableList [i].Weights;
      }

      var _equipmentAttributeList = EquipmentAttributesTableReader.Instance.FindDefaultByEquipmentID (_equipmentTable.ID);
      List<EquipmentAttribute> equipmentAttributeBaseList = new List<EquipmentAttribute> ();
      for (int i = 0; i < _equipmentAttributeList.Count; i++) 
      {
        int attribute = CalculateAttributeOffSet (_equipmentAttributeList[i].ParameterA,_equipmentAttributeList[i].ParameterB);
        equipmentAttributeBaseList.Add (new EquipmentAttribute(_equipmentAttributeList[i].AttributeType,attribute));
      }

      List<EquipmentAttribute> _equipmentAttrubuteOffsetList = new List<EquipmentAttribute>();

      HeroEquipmentFormat _heroEquipment = new HeroEquipmentFormat (_equipmentTable,equipmentAttributeBaseList,_equipmentAttrubuteOffsetList);
      return _heroEquipment;
    }

    public int GetEquipmentReinforceSuccessRate(int level)
    {
      var _equipmentOtherValue = EquipmentOtherValueTableReader.Instance.DefaultCachedList;
      float _rateBase = (float)_equipmentOtherValue [0].Rate;

      float _calculatedRate = Mathf.Pow (_rateBase,level) * 100;

      return Mathf.FloorToInt(_calculatedRate);

    }

    public HeroEquipmentFormat GetEquipmentAfterReinforced(int selectLevel,HeroEquipmentFormat equipmentData)
    {
      HeroEquipmentFormat _newEquipment = equipmentData.CloneEx ();
      var _equipmentReinforceRangeList = EquipmentReinforceValueRangeTableReader.Instance.DefaultCachedList;
      var _equipmentReinforceRange = _equipmentReinforceRangeList [selectLevel];

      // normal reinforce
      if (selectLevel <= (int)EQUIPMENT_REINFORCE_LEVEL.REINFORCE_BIG) 
      {
        var _attributeOffset = ReinforcementByIncreaseAttributeValue (equipmentData,_equipmentReinforceRange);
        _newEquipment.EquipmentAttributeOffsetList.Add (_attributeOffset);

      }
      //miracle reinforce
      else if(selectLevel == (int)EQUIPMENT_REINFORCE_LEVEL.REINFORCE_MIRACLE)
      {
        //if base attribute list count is less than 4, add a attribute into base attribute list
        if (equipmentData.EquipmentAttributeBaseList.Count < EquipmentOthers.EQUIPMENT_REINFORCE_ATTRIBUTE_BASE_LIST_MAX) 
        {
          var _attributeNew = ReinforcementByIncreaseAttributeType (equipmentData);
          _newEquipment.EquipmentAttributeBaseList.Add (_attributeNew);
        }
        // if base attribute list count is 4, same with normal but with different randon range
        else if (equipmentData.EquipmentAttributeBaseList.Count == EquipmentOthers.EQUIPMENT_REINFORCE_ATTRIBUTE_BASE_LIST_MAX) 
        {
          var _attributeOffset = ReinforcementByIncreaseAttributeValue (equipmentData,_equipmentReinforceRange);
          _newEquipment.EquipmentAttributeOffsetList.Add (_attributeOffset);
        }
      }
      _newEquipment.ReinforcementLevel += 1;
      return _newEquipment;
    }

    public EquipmentAttribute ReinforcementByIncreaseAttributeValue(HeroEquipmentFormat equipmentData,EquipmentReinforceValueRangeTable equipmentReinforceRange)
    {
      //random a type to reinforce
      int _randTypeID = Random.Range(0,equipmentData.EquipmentAttributeBaseList.Count);
      int _normalAttribute = equipmentData.EquipmentAttributeBaseList [_randTypeID].Attribute;
      ATTRIBUTE_TYPE _type = equipmentData.EquipmentAttributeBaseList [_randTypeID].AttributeType;
      //random value of the type
      double _randOffset = System.Math.Round(Random.Range (equipmentReinforceRange.RandMin, equipmentReinforceRange.RandMax), 2);
      int _value = equipmentReinforceRange.Base + Mathf.FloorToInt((float)(_normalAttribute * _randOffset));

      EquipmentAttribute _attributeOffset = new EquipmentAttribute (_type,_value);

      return _attributeOffset;
    }

    public EquipmentAttribute ReinforcementByIncreaseAttributeType(HeroEquipmentFormat equipmentData)
    {
      var _randAttributeTypeList = EquipmentReinforceAttributeRangeTableReader.Instance.DefaultCachedList;
      List<EquipmentReinforceAttributeRangeTable> attributeRandomList = _randAttributeTypeList.CloneEx ();
      for (int i = 0; i < _randAttributeTypeList.Count; i++) 
      {

        for (int j = 0; j < equipmentData.EquipmentAttributeBaseList.Count; j++) 
        {
          //remove the type that already existed
          if (_randAttributeTypeList [i].AttributeType == equipmentData.EquipmentAttributeBaseList [j].AttributeType)
            attributeRandomList.Remove (_randAttributeTypeList [i]);
        }

      }

      //rand the new type from type list except the types that already exist 
      int _randTypeIndex = Random.Range (0,attributeRandomList.Count);
      ATTRIBUTE_TYPE _randType = _randAttributeTypeList [_randTypeIndex].AttributeType;
      //rand the value
      int _randValue = Random.Range(_randAttributeTypeList [_randTypeIndex].RandMin,_randAttributeTypeList [_randTypeIndex].RandMax+1);

      EquipmentAttribute _attributeOffset = new EquipmentAttribute (_randType,_randValue);

      return _attributeOffset;
    }

    public string GetDefaultTexturePathByEquipmentType(EQUIPMENT_TYPE equipmentType)
    {
      string _texturePath = "";
      var _equipmentTableList = EquipmentTableReader.Instance.FindDefault (equipmentType,0);
      if (_equipmentTableList.Count > 0) 
      {
        var _equipmentTable = _equipmentTableList [0];
        _texturePath = _equipmentTable.TexturePath;
      }

      return _texturePath;

    }
  	
  }
}
