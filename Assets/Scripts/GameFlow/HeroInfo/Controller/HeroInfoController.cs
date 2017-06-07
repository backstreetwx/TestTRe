using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DataManagement;
using DataManagement.SaveData;
using DataManagement.GameData;
using DataManagement.SaveData.FormatCollection;
using DataManagement.GameData.FormatCollection;
using HeroInfo.Controllers;
using HeroInfo.Test;
using Common;
using NextJump.Controllers;
using HeroInfo.Views;
using ConstCollections.PJEnums.Character;
using ConstCollections.PJPaths;
using ConstCollections.PJConstStrings;
using DataManagement.GameData.FormatCollection.Battle;

namespace HeroInfo.Controllers{

  public class HeroInfoController : MonoBehaviour {

    public GameObject HeroLessThan3Canvas;
    public GameObject HeroFullCanvas;
    public GameObject SkillPrefab;
    public GameObject EquipmentPrefab;
    public WindowPopManager PopNextJumpCanvasManager;

    public string NameOfSkillScene;
    public string NameOfEquipmentScene;
    public string NameOfNextJumpScene;
    public BasicAttributeDisplayController BasicAttributeController;
    public CharacterAttributeDisplayController CharacterAttributeController;
    public SkillLayoutDisplayController SkillLayoutController;
    public EquipmentLayoutDisplayController EquipmentLayoutControllerScript;
    public HeroSelecterController HeroSelecter;
    public HeroInfoBGView HeroBGView;
    public int HeroMaxNum = 3;


    public Sprite CharacterSelecteBGOneHero;
    public Sprite CharacterSelecteBGTwoHeroSlot0;
    public Sprite CharacterSelecteBGTwoHeroSlot1;
    public Sprite CharacterSelecteBGThreeHeroSlot0;
    public Sprite CharacterSelecteBGThreeHeroSlot1;
    public Sprite CharacterSelecteBGThreeHeroSlot2;

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      heroes = HeroDataManager.Instance.GetHeroDataList();
      BasicAttributeController.Init ();
      CharacterAttributeController.Init ();
      SkillLayoutController.Init ();
      EquipmentLayoutControllerScript.Init ();
      HeroBGView.Init ();
      InitHeroesIcon ();

      CharacterSimpleDataFormat simpleData =  globalDataManager.GetValue<CharacterSimpleDataFormat> (CharacterSimpleDataFormat.NAME,CharacterSimpleDataFormat.MEMORY_SPACE);
      if (simpleData!=null&&simpleData.Type == CHARACTER_TYPE.HERO) 
      {
        HeroSelected (simpleData.SlotID);
        globalDataManager.RemoveValue (CharacterSimpleDataFormat.NAME, CharacterSimpleDataFormat.MEMORY_SPACE);
      } 
      else 
      {
        //Show Default Player
        HeroSelected(0);
      }

    }

    void OnEnable () 
    {
      HeroSaveDataManager.Instance.HeroSaveDataListChangedEvent += HeroSaveDataListChanged;
    }

    void OnDisable()
    {
      HeroSaveDataManager.Instance.HeroSaveDataListChangedEvent -= HeroSaveDataListChanged;
    }

    public void HeroSelected(int SlotId)
    {
      this.currentSlotID = SlotId;
      Sprite _characterSelectBG = null;
      var _heroList = HeroSaveDataManager.Instance.HeroSaveDataList;
      int _heroCount = _heroList.Count;
      switch (SlotId) 
      {
        case 0:
          if (_heroCount == 1) 
          {
            _characterSelectBG = CharacterSelecteBGOneHero;
          } 
          else if (_heroCount == 2) 
          {
            _characterSelectBG = CharacterSelecteBGTwoHeroSlot0;
          } 
          else if (_heroCount == 3) 
          {
            _characterSelectBG = CharacterSelecteBGThreeHeroSlot0;
          }
          break;
        case 1:
          if (_heroCount == 2) 
          {
            _characterSelectBG = CharacterSelecteBGTwoHeroSlot1;
          } 
          else if (_heroCount == 3) 
          {
            _characterSelectBG = CharacterSelecteBGThreeHeroSlot1;
          }
          break;
        case 2:
          _characterSelectBG = CharacterSelecteBGThreeHeroSlot2;
          break;
        default:
          break;
      }

      HeroBGView.SetBackground (_characterSelectBG);
      HeroSelecter.HeroUnSelected ();
      HeroSelecter.HeroSelected (SlotId);
      DisplayPlayerInfo (heroes [SlotId]);

    }

    public void SkillDisplay()
    {
      
      globalDataManager.SetValue (SkillString.SKILL_SLOT_ID,this.currentSlotID,SkillString.MEMORY_SPACE);

      PopNextJumpCanvasManager.ShowWindow (this.SkillPrefab);
    }

    public void EquipmentDisplay()
    {
      globalDataManager.SetValue (EquipmentString.HERO_SLOTID,this.currentSlotID,EquipmentString.MEMORY_SPACE);

      PopNextJumpCanvasManager.ShowWindow (this.EquipmentPrefab);

    }

    public void NextJump()
    {
      var heroList = HeroSaveDataManager.Instance.HeroSaveDataList;
      int _heroNumber = heroList.Count;

      if (_heroNumber < 3) {
        PopNextJumpCanvasManager.ShowWindow (this.HeroLessThan3Canvas);
      } 
      else if (_heroNumber == 3) 
      {
        PopNextJumpCanvasManager.ShowWindow (this.HeroFullCanvas);
      }

    }

    void InitHeroesIcon()
    {
      List<HeroIconDataFormat> _heroIconDataList = new List<HeroIconDataFormat> ();
      for(int i = 0;i < HeroMaxNum;i++)
      {
        HeroIconDataFormat _heroIconData = new HeroIconDataFormat ("",-1);
        _heroIconDataList.Add (_heroIconData);
      }

      for(int i = 0;i<heroes.Count;i++)
      {
        _heroIconDataList [i].IconID = heroes [i].AnimationInfo.IconID;
        _heroIconDataList [i].Path = heroes [i].AnimationInfo.TexturePath;
      }

      HeroSelecter.Init ();
      HeroSelecter.HeroExistOnSlotOrNot (_heroIconDataList);

    }

    void HeroSaveDataListChanged(List<HeroSaveDataFormat> heroSaveDataList)
    {
      List<HeroDataFormat> _list = new List<HeroDataFormat> ();
      heroSaveDataList.ForEach(heroSaveData => {
        HeroDataFormat _hero = new HeroDataFormat(heroSaveData);
        _list.Add(_hero);
      });
      heroes = _list.CloneEx();
      DisplayPlayerInfo (heroes [this.currentSlotID]);
    }

    void DisplayPlayerInfo(HeroDataFormat hero)
    {
      BasicAttributeController.SetDataForDisplay (hero);
      CharacterAttributeController.SetDataForDisplay (hero);
      SkillLayoutController.SetDataForDisplay (hero);
      EquipmentLayoutControllerScript.SetDataForDisplay (hero);
    }
    int currentSlotID;
    List<HeroDataFormat> heroes;
    GlobalDataManager globalDataManager;
  }

  [System.Serializable]
  public class HeroIconDataFormat
  {
    public string Path;
    public int IconID;

    public HeroIconDataFormat(string path,int iconID)
    {
      this.Path = path;
      this.IconID = iconID;
    }
  }

}
