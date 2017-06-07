using UnityEngine;
using System;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.GameData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.TableClass.Equipment;
using ConstCollections.PJEnums.Equipment;

namespace Test{

  public class Test : MonoBehaviour {

    public TestView View;
    public ImageView IconView;

    void Start()
    {
      View.Init ();
      IconView.Init ();
    }

    public void ShowDBMessage()
    {
      var _t = EquipmentTableReader.Instance.FindDefaultUnique (0);
      Debug.Log ("QualityGrade" + _t.QualityGrade );
      Debug.Log ("DimensionChipOutput" + _t.DimensionChipOutput );


      var _t1 = EquipmentStringsTableReader.Instance.GetString (4);
      Debug.Log ("Equipment Name " + _t1 );

      var _t2 = EquipmentAttributesTableReader.Instance.FindDefaultByEquipmentID (0);
      for (int i = 0; i < _t2.Count; i++) 
      {
        Debug.Log ("EquipmentAttributes " + _t2[i].AttributeType.ToString() );
      }

      var _t3 = EquipmentQualityGradeAuraTableReader.Instance.GetCostAura (4);
      Debug.Log ("Quality Grade Cost Aura " + _t3 );

      IconView.SetSkillIcon (_t.TexturePath,_t.TextureIconID);

      var _t4 = EquipmentOtherValueTableReader.Instance.FindDefaultUnique ((ushort)0);

      string _format = _t4.Format;
      string _displayData = _t.DimensionChipOutput + "+ " + _t1 + " + " + _t2 [0].AttributeType.ToString () + " Cost Aura :" + _t3;


      var _t5 = EquipmentOtherValueTableReader.Instance.FindDefaultUnique (0);

      Debug.LogFormat ("success rate : {0}",_t5.Rate);

      var _t6 = EquipmentReinforceValueRangeTableReader.Instance.FindDefaultUniqueByLevel (0);
      var _rand = Math.Round(UnityEngine.Random.Range (_t6.RandMin, _t6.RandMax), 2);
      Debug.LogFormat ("reinforce value base : {0} ,value RandMin : {1}, value RandMax : {2},value rand  : {3}",_t6.Base,_t6.RandMin,_t6.RandMax,_rand);

      var _t7 = EquipmentReinforceAttributeRangeTableReader.Instance.FindDefaultUniqueByAttributeType (ATTRIBUTE_TYPE.PEN);
      Debug.LogFormat ("PEN rand min  : {0} , rand max : {1}",_t7.RandMin,_t7.RandMax);


      var _t8 = EquipmentReinforceCostTableReader.Instance.FindDefaultUniqueByLevel (2);
      Debug.LogFormat ("reinforce cost level2 cost type : {0} , cost number : {1}",_t8.CostType,_t8.CostNumber);

      View.ShowMessage (string.Format(_format,_displayData));

    }
  }
}
