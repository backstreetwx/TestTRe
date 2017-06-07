using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Common;
using DataManagement;
using DataManagement.SaveData;
using DataManagement.SaveData.FormatCollection;
using ConstCollections.PJConstStrings;
using HeroInfo.Test;
using NextJump.Views;
using DataManagement.TableClass.Hero;
using DataManagement.TableClass;
using DataManagement.GameData;
using DataManagement.GameData.FormatCollection;
using HeroInfo.Controllers;


namespace NextJump.Controllers{

  public class HeroFullManager : MonoBehaviour {

    public string NextSceneName;
    public WindowPopManager PopHeroFullManager;

    public HeroFullCanvasHeroController[] HeroControllers;

    public NextJumpView NextJumpButton;

    public Sprite HeroKeepOnSprite;
    public Sprite HeroKeepOffSprite;

    // Use this for initialization
    void Start ()
    {
      NextJumpButton.Init ();
      NextJumpButton.SetButtonClickableOrNot (false);
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      PopHeroFullManager = FindObjectOfType<WindowPopManager>();

      var _dataList = HeroDataManager.Instance.GetHeroDataList ();
      List<HeroDataFormat> _heroDataList = new List<HeroDataFormat> ();

      List<HeroSaveDataFormat> _heroSaveDataList = HeroSaveDataManager.Instance.HeroSaveDataList;

      heroToBeReplaceList = new List<HeroSaveDataFormat> ();
      int _lastestSlotId = UserSaveDataManager.Instance.LastestHeroSlotID;

      for(int i = 0; i < _heroSaveDataList.Count; i++)
      {
        if (_heroSaveDataList [i].SlotID != _lastestSlotId) 
        {
          heroToBeReplaceList.Add (_heroSaveDataList[i]);
          _heroDataList.Add (_dataList[i]);
        }
      }

      //FIXME : yangzhi-wang Get the name string by Array
      for (int i = 0; i < HeroControllers.Length; i++)
      {
        
        HeroControllers [i].Init (_heroDataList[i]);
        HeroControllers [i].DisplayInfo (new HeroFullCanvasHeroDataFormat (heroToBeReplaceList[i].NameString,
                                                                           heroToBeReplaceList[i].Level,
          new HeroIconDataFormat(_heroDataList[i].AnimationInfo.TexturePath,_heroDataList[i].AnimationInfo.IconID)));

        HeroControllers [i].HeroSaveButtonViewShow (HeroKeepOffSprite);
      }

    }

    public void ChooseHero(int order)
    {
      //save this one, replace other one
      switch (order) 
      {
        case 0:
          this.nextSlotID = heroToBeReplaceList [1].SlotID;
          break;
        case 1:
          this.nextSlotID = heroToBeReplaceList [0].SlotID;
          break;
        default:
          break;
      }

      for (int i = 0; i < HeroControllers.Length; i++) 
      {
        if (i == order) {
          HeroControllers [i].HeroSaveButtonViewShow (HeroKeepOnSprite);
        } 
        else 
        {
          HeroControllers [i].HeroSaveButtonViewShow (HeroKeepOffSprite);
        }
      }

      NextJumpButton.SetButtonClickableOrNot (true);
    }

    public void NextJump()
    {
      
      globalDataManager.SetValue<int> (NextJumpString.NEXT_HERO_SLOTID, this.nextSlotID,NextJumpString.MEMORY_SPACE);
      SceneManager.LoadScene (this.NextSceneName);
    }

    public void Back()
    {

      PopHeroFullManager.Close ();
    }

    int nextSlotID;
    List<HeroSaveDataFormat> heroToBeReplaceList;
    GlobalDataManager globalDataManager;

  }

  [System.Serializable]
  public class HeroFullCanvasHeroDataFormat
  {
    public string Name;
    public int Level;
    public HeroIconDataFormat HeroIconData;

    public HeroFullCanvasHeroDataFormat(string name, int level, HeroIconDataFormat heroIconData)
    {
      this.Name = name;
      this.Level = level;
      this.HeroIconData = heroIconData;
    }
  }

}
