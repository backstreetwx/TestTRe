using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataManagement.GameData.FormatCollection;
using GameFlow.Battle.Common.Controller;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData;

namespace GameFlow.Battle.Controller
{
  public class HeroManager : AbsCharacterManager 
  {
    public event System.Action<CHARACTER_TYPE,List<AbsCharacterController>> HeroControllerListChangedEvent = delegate(CHARACTER_TYPE characterType,List<AbsCharacterController> obj) {};

    #region implemented abstract members of CharacterBaseManager
    public override bool AllDead
    {
      get
      { 
        bool _allDead = true;
        this.ControllerList.ForEach (_hero => {
          if(_hero.State == ConstCollections.PJEnums.Character.STATES.ALIVE)
          {
            _allDead = false;
          }
        });

        return _allDead;
      }
    }

    public override List<AbsCharacterController> AliveList
    {
      get
      { 
        if (this.AllDead)
          return null;
        
        List<AbsCharacterController> _aliveList = this.ControllerList.FindAll (_hero => {
          return _hero.State == ConstCollections.PJEnums.Character.STATES.ALIVE;
        });

        return _aliveList;
      }
    } 

    public override void InitFight()
    {
      base.InitFight ();

      this.CharacterType = CHARACTER_TYPE.HERO;

      var _dataList = HeroDataManager.Instance.HeroDataCacheList;

      for (int i = 0; i < _dataList.Count; i++) 
      {
        int _slotID = _dataList [i].Attributes.SlotID;

        HeroController _heroController = this.ControllerList.Find (col => col.SlotID == _slotID) as HeroController;

        if (_heroController == null) {
          var _gameObject = Instantiate (this.CharacterPrefab, this.SlotTransformList [_slotID], false) as GameObject;
          _heroController = _gameObject.GetComponent<HeroController> ();
          _heroController.InitFight(_dataList [i]);
          this.ControllerList.Add (_heroController);
        } 
        else 
        {
          _heroController.gameObject.SetActive (true);
          _heroController.InitFight (_dataList [i]);
        }
      }

      IComparer<HeroController> _sortBySlotID = new SortHeroControllerBySlotID (); 
      this.ControllerList.Cast<HeroController> ().ToList().Sort(_sortBySlotID);
      this.HeroControllerListChangedEvent.Invoke (CHARACTER_TYPE.HERO,this.ControllerList);
    }

    #endregion

  }

  [System.Serializable]
  public class TestHeroSpriteIndexListFormat
  {
    public int Idle;
    public int Attack;
    public int GetDamage;
    public int Dead;
  }
}
