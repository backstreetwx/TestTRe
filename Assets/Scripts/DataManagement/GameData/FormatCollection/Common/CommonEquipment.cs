using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Equipment;
using Common;
using DataManagement.TableClass.Equipment;

namespace DataManagement.GameData.FormatCollection.Common
{
  public interface ICommonEquipment
  {
    CommonAttributeFormat AttributesBuff 
    {
      get;
    }
  }

  [System.Serializable]
  public class CommonEquipmentFormat : ICommonEquipment
  {
    public int DBEquipmentID;
    public int QualityGrade;
    public EQUIPMENT_TYPE EquipmentType;
    public string TexturePath;
    public int TextureIconID;
    public MultiLangString<EquipmentStringsTable> NameString;

    #region ICommonEquipment implementation

    public virtual CommonAttributeFormat AttributesBuff {
      get
      {
//        throw new System.NotImplementedException ();
        //FIXME: yang-zhang
        return new CommonAttributeFormat(true);
      }
    }

    #endregion

    public CommonEquipmentFormat()
    {
      this.DBEquipmentID = -1;
      this.QualityGrade = -1;
      this.EquipmentType = EQUIPMENT_TYPE.NONE;
      this.TexturePath = "";
      this.TextureIconID = -1;
      this.NameString = null;
    }
  }
}
