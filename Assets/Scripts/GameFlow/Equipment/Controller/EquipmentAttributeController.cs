using UnityEngine;
using System.Collections;
using Equipment.Views;
using DataManagement.GameData.FormatCollection;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJEnums;
using ConstCollections.PJConstStrings;
using DataManagement.TableClass;
using DataManagement.TableClass.Equipment;
using Common;

namespace Equipment.Controllers{

  public class EquipmentAttributeController : MonoBehaviour {

    public EquipmentAttributeTypeView AttributeType;
    public EquipmentAttributeNumView BaseAttributeNum;
    public EquipmentAttributeNumView OffsetAttributeNum;

    // Use this for initialization
    public void Init () 
    {
      var _textFormat = EquipmentOtherValueTableReader.Instance.DefaultCachedList[0].Format;
      
      AttributeType.Init ();
      BaseAttributeNum.Init (_textFormat);
      OffsetAttributeNum.Init (_textFormat);

    }

    public void AttributeDisplay(EquipmentAttribute equipmentBaseAttribute,EquipmentAttribute equipmentOffsetAttribute)
    {
      STRINGS_LABEL _label = EquipmentString.EquipmentAttributeStringDic[equipmentBaseAttribute.AttributeType];
      ushort _ID = StringsTableReader.Instance.FindID (_label);
      MultiLangString<StringsTable> _multi = new MultiLangString<StringsTable> (_ID,StringsTableReader.Instance);
      AttributeType.ShowEquipmentAttributeType (_multi);
      AttributeType.Display ();
      BaseAttributeNum.ShowAttributeNum (equipmentBaseAttribute.Attribute);
      if(equipmentOffsetAttribute.Attribute > 0)
        OffsetAttributeNum.ShowAttributeNum (equipmentOffsetAttribute.Attribute,true);
    }

    public void DataClear()
    {
      AttributeType.Clear ();
      BaseAttributeNum.Clear ();
      OffsetAttributeNum.Clear ();
    }
  }
}
