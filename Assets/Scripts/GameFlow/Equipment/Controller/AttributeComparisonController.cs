using UnityEngine;
using System.Collections;
using Equipment.Views;
using DataManagement.GameData.FormatCollection;
using DataManagement.GameData.FormatCollection.Common;
using System.Collections.Generic;
using DataManagement.TableClass.Equipment;
using DataManagement.GameData;
using System.Linq;

namespace Equipment.Controllers{

  public class AttributeComparisonController : MonoBehaviour {

    public EquipmentInfoView HPView;
    public EquipmentInfoView RESView;
    public EquipmentInfoView ATKView;
    public EquipmentInfoView MAGView;
    public EquipmentInfoView DEFView;
    public EquipmentInfoView ACView;
    public EquipmentInfoView CRIView;
    public EquipmentInfoView PENView;
    public EquipmentInfoView HITView;
    public EquipmentInfoView AVDView;
    public EquipmentInfoView STRView;
    public EquipmentInfoView INTView;
    public EquipmentInfoView VITView;
    public EquipmentInfoView DEXView;
    public EquipmentIconView EquipmentIcon;

    public void Init () 
    {
      var _equipmentTextFormat = EquipmentOtherValueTableReader.Instance.FindDefaultUnique ((ushort)0);
      string _textFormat = _equipmentTextFormat.Format;

      HPView.Init (_textFormat);
      RESView.Init (_textFormat);
      ATKView.Init (_textFormat);
      MAGView.Init (_textFormat);
      DEFView.Init (_textFormat);
      ACView.Init (_textFormat);
      CRIView.Init (_textFormat);
      PENView.Init (_textFormat);
      HITView.Init (_textFormat);
      AVDView.Init (_textFormat);
      STRView.Init(_textFormat);
      INTView.Init(_textFormat);
      VITView.Init(_textFormat);
      DEXView.Init (_textFormat);
      EquipmentIcon.Init ();

    }


    public void SetDataForDisplay(HeroDataFormat heroDataFormat,HeroEquipmentFormat equipmentEquipped)
    {
      HeroAttributeFormat _heroAttributeFinal = heroDataFormat.AttributesWithEquipments;
      HPView.DataDisplayImmediately (_heroAttributeFinal.HPMax);
      RESView.DataDisplayImmediately (_heroAttributeFinal.RES);
      ATKView.DataDisplayImmediately (_heroAttributeFinal.ATK);
      MAGView.DataDisplayImmediately (_heroAttributeFinal.MAG);
      DEFView.DataDisplayImmediately (_heroAttributeFinal.DEF);
      ACView.DataDisplayImmediately (_heroAttributeFinal.AC);
      CRIView.DataDisplayImmediately (_heroAttributeFinal.CRI);
      PENView.DataDisplayImmediately (_heroAttributeFinal.PEN);
      HITView.DataDisplayImmediately (_heroAttributeFinal.HIT);
      AVDView.DataDisplayImmediately (_heroAttributeFinal.AVD);
      STRView.DataDisplayImmediately (_heroAttributeFinal.STR);
      INTView.DataDisplayImmediately (_heroAttributeFinal.INT);
      VITView.DataDisplayImmediately (_heroAttributeFinal.VIT);
      DEXView.DataDisplayImmediately (_heroAttributeFinal.DEX);
      EquipmentIcon.ShowEquipmentIcon (equipmentEquipped.TexturePath,equipmentEquipped.TextureIconID);
    }

    public void DisplayWithUnequippedEquipment(HeroDataFormat heroDataFormat,HeroEquipmentFormat equipmentTakeOff)
    {
      var _heroAttributes = heroDataFormat.Attributes;

      List<HeroEquipmentFormat> _newEquipmentList = new List<HeroEquipmentFormat> ();
      _newEquipmentList.Add (equipmentTakeOff);
      for (int i = 0; i < heroDataFormat.EquipmentList.Count; i++) 
      {
        if (heroDataFormat.EquipmentList [i].EquipmentType != equipmentTakeOff.EquipmentType)
          _newEquipmentList.Add (heroDataFormat.EquipmentList [i]);
      }

      var _attributesWithNewEquipmentList = HeroDataManager.Instance.CalculateAttributesWithEquipments (_heroAttributes,_newEquipmentList.Cast<CommonEquipmentFormat>().ToList());

      var _attributesWithEquippedEquipmentList = heroDataFormat.AttributesWithEquipments;


      HPView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.HPMax,_attributesWithNewEquipmentList.HPMax);
      RESView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.RES,_attributesWithNewEquipmentList.RES);
      ATKView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.ATK,_attributesWithNewEquipmentList.ATK);
      MAGView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.MAG,_attributesWithNewEquipmentList.MAG);
      DEFView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.DEF,_attributesWithNewEquipmentList.DEF);
      ACView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.AC,_attributesWithNewEquipmentList.AC);
      CRIView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.CRI,_attributesWithNewEquipmentList.CRI);
      PENView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.PEN,_attributesWithNewEquipmentList.PEN);
      HITView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.HIT,_attributesWithNewEquipmentList.HIT);
      AVDView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.AVD,_attributesWithNewEquipmentList.AVD);
      STRView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.STR,_attributesWithNewEquipmentList.STR);
      INTView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.INT,_attributesWithNewEquipmentList.INT);
      VITView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.VIT,_attributesWithNewEquipmentList.VIT);
      DEXView.DataDisplayWithFormatWhenDifferent (_attributesWithEquippedEquipmentList.DEX,_attributesWithNewEquipmentList.DEX);
      EquipmentIcon.ShowEquipmentIcon (equipmentTakeOff.TexturePath,equipmentTakeOff.TextureIconID);

    }

  }
}
