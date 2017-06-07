using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using InitHero.Views;
using DataManagement;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstStrings;
using DataManagement.TableClass.Hero;
using DataManagement.GameData;

namespace InitHero.Controllers{
  
  public class PropertyController : MonoBehaviour {

    public HeroInitAttributeFormat HeroAttributes;

    public PropertyView PropertySTRView;
    public PropertyView PropertyVITView;
    public PropertyView PropertyINTView;
    public PropertyView PropertyDEXView;



    // Use this for initialization
    public void Init (HeroBaseTable heroBase) 
    {
      heroBaseData = heroBase;
      GetAndShowRandomProperty ();
    }

    public void GetAndShowRandomProperty()
    {
      this.HeroAttributes = GetRandomHeroAttributes ();
      this.PropertySTRView.ShowAttribute (this.HeroAttributes.STR, this.HeroAttributes.STRUp);
      this.PropertyVITView.ShowAttribute (this.HeroAttributes.VIT, this.HeroAttributes.VITUp);
      this.PropertyINTView.ShowAttribute (this.HeroAttributes.INT, this.HeroAttributes.INTUp);
      this.PropertyDEXView.ShowAttribute (this.HeroAttributes.DEX, this.HeroAttributes.DEXUp);
    }

    public void PropertyDescription()
    {
      //FIXME : do it after this UI design finish

    }

    HeroInitAttributeFormat GetRandomHeroAttributes()
    {
      int _totalNum = heroBaseData.RandomPointMax;
      int _attributeMaximum = heroBaseData.AttributeMaximum;
      int _level = heroBaseData.Level;

      int _STRRandMax = _attributeMaximum - heroBaseData.STRMin;//25 - 5 = 20
      int _VITRandMax = _attributeMaximum - heroBaseData.VITMin;
      int _INTRandMax = _attributeMaximum - heroBaseData.INTMin;
      int _DEXRandMax = _attributeMaximum - heroBaseData.DEXMin;

      int _STR = 0;
      int _VIT = 0;
      int _INT = 0;
      int _DEX = 0;


      while(_totalNum > 0)
      {
        int _randomMax = Mathf.Min (_totalNum,_STRRandMax);
        int _randomSTR = Random.Range (0, _randomMax + 1);
        _STR += _randomSTR;
        _STRRandMax -= _randomSTR;
        _totalNum -= _randomSTR;

        if(_totalNum > 0)
        {
          _randomMax = Mathf.Min (_totalNum,_VITRandMax);
          int _randomVIT = Random.Range (0, _randomMax + 1);
          _VIT += _randomVIT;
          _VITRandMax -= _randomVIT;
          _totalNum -= _randomVIT;
        }

        if(_totalNum > 0)
        {
          _randomMax = Mathf.Min (_totalNum,_INTRandMax);
          int _randomINT = Random.Range (0, _randomMax + 1);
          _INT += _randomINT;
          _INTRandMax -= _randomINT;
          _totalNum -= _randomINT;
        }

        if(_totalNum > 0)
        {
          _randomMax = Mathf.Min (_totalNum,_DEXRandMax);
          int _randomDEX = Random.Range (0, _randomMax + 1);
          _DEX += _randomDEX;
          _DEXRandMax -= _randomDEX;
          _totalNum -= _randomDEX;
        }

      }

      return new HeroInitAttributeFormat (_level, heroBaseData.STRMin + _STR, heroBaseData.VITMin + _VIT, heroBaseData.INTMin + _INT, heroBaseData.DEXMin + _DEX);
    }

    HeroBaseTable heroBaseData;

  }

  [System.Serializable]
  public class HeroInitAttributeFormat
  {
    public int Level;
    public float STR;
    public float VIT;
    public float INT;
    public float DEX;
    public float STRUp;
    public float VITUp;
    public float INTUp;
    public float DEXUp;

    public HeroInitAttributeFormat(int level,int str, int vit, int Int, int dex)
    {
      this.Level = level;
      this.STR = str;
      this.VIT = vit;
      this.INT = Int;
      this.DEX = dex;

      this.STRUp = this.STR * UP_COE;
      this.VITUp = this.VIT * UP_COE;
      this.INTUp = this.INT * UP_COE;
      this.DEXUp = this.DEX * UP_COE;
    }

    static readonly float UP_COE = 0.1F;
  }
}
