using UnityEngine;
using System.Collections;
using Equipment.Views;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJEnums.Equipment;
using DataManagement.GameData;
using ConstCollections.PJEnums;
using DataManagement.TableClass;
using DataManagement.TableClass.Equipment;
using Common;
using DataManagement.SaveData.FormatCollection;

namespace Equipment.Controllers{

  public class EquipmentLabelController : MonoBehaviour {

    public EquipmentIconView EquipmentIcon;
    public EquipmentNameView EquipmentName;
    public EquipmentAttributeNumView ReinforcementLevel;

    public EquipmentAttributeController[] AttributeList;

    public STRINGS_LABEL NoEquipment;

    public void Init () 
    {
      var _textFormat = EquipmentOtherValueTableReader.Instance.DefaultCachedList[0].Format;
      ReinforcementLevel.Init (_textFormat);
      ReinforcementLevel.Clear ();

      EquipmentIcon.Init ();
      EquipmentIcon.IconDisplayOrNot (false);
      var _noEquipment = StringsTableReader.Instance.GetString (NoEquipment);
      EquipmentName.Init (_noEquipment);
      EquipmentName.NoEquipmentMessage();

      for (int i = 0; i < AttributeList.Length; i++) 
      {
        AttributeList [i].Init ();
        AttributeList [i].DataClear ();
      }

  	}

    public void LoadDataToDisplay(HeroEquipmentFormat equipmentData)
    {
      ReinforcementLevel.Clear ();
      EquipmentIcon.IconDisplayOrNot (false);
      EquipmentName.NoEquipmentMessage();
      for (int i = 0; i < AttributeList.Length; i++) 
      {
        AttributeList [i].DataClear ();
      }

      this.selfEquipmentData = equipmentData;


      if (this.selfEquipmentData != null) 
      {
        if (this.selfEquipmentData.ReinforcementLevel > 0)
          ReinforcementLevel.ShowAttributeNum (this.selfEquipmentData.ReinforcementLevel,true);
        EquipmentIcon.IconDisplayOrNot (true);
        EquipmentIcon.ShowEquipmentIcon (this.selfEquipmentData.TexturePath,this.selfEquipmentData.TextureIconID);
        var _equipmentString = new MultiLangString<EquipmentStringsTable> ((ushort)this.selfEquipmentData.DBEquipmentID, EquipmentStringsTableReader.Instance);
        EquipmentName.ShowEquipmentName (_equipmentString);
        var _attributeBaseList = this.selfEquipmentData.EquipmentAttributeBaseList;
        var _attributeOffsetList = this.selfEquipmentData.EquipmentAttributeOffsetList;
        for (int i = 0; i < _attributeBaseList.Count; i++) 
        {
          EquipmentAttribute _attributeOffset = new EquipmentAttribute (_attributeBaseList[i].AttributeType,0);

          for (int j = 0; j < _attributeOffsetList.Count; j++) 
          {
            if (_attributeOffsetList [j].AttributeType == _attributeBaseList [i].AttributeType)
              _attributeOffset.Attribute += _attributeOffsetList [j].Attribute;
          }


          AttributeList [i].AttributeDisplay (_attributeBaseList[i],_attributeOffset);
        }
      }
    }

    HeroEquipmentFormat selfEquipmentData;
   
  }
}
