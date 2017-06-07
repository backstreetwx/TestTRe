using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection;
using DataManagement;
using ConstCollections.PJEnums.Character;
using DataManagement.GameData;
using GameFlow.Battle.Common.Controller;
using GameFlow.Battle.Controller;
using DataManagement.GameData.FormatCollection.Common.Skill;

namespace BattleCharacterInfo.Controllers{

  public class BattleCharacterInfoManager : MonoBehaviour {

    public BattleCharacterAttributeController CharacterAttribute;
    public BattleCharacterSkillManager CharacterSkill;

    void OnEnable()
    {
      if(this.heroManager == null)
        this.heroManager = FindObjectOfType<HeroManager> ();
      if(this.enemyManager == null)
        this.enemyManager = FindObjectOfType<EnemyManager> ();

      this.heroManager.HeroControllerListChangedEvent += HeroControllerListChanged;
      this.enemyManager.EnemyControllerListChangedEvent += EnemyControllerListChanged;

    }

    void OnDisable()
    {
      this.heroManager.HeroControllerListChangedEvent -= HeroControllerListChanged;
      this.enemyManager.EnemyControllerListChangedEvent -= EnemyControllerListChanged;
    }

    // Use this for initialization
    void Start () 
    {
      globalDataManager = FindObjectOfType<GlobalDataManager> ();
      battleBottomManager = FindObjectOfType<BattleBottomManager> ();
      AbsCharacterControllerFormat characterData = globalDataManager.GetValue<AbsCharacterControllerFormat> (AbsCharacterControllerFormat.NAME,AbsCharacterControllerFormat.MEMORY_SPACE);
      if (characterData != null) 
      {
        this.characterType = characterData.Controller.Type;
        this.slotID = characterData.Controller.SlotID;
        this.characterController = characterData.Controller;
        CharacterAttribute.Init (characterData.Controller);
        CharacterAttribute.DataDisplay ();
        if (characterData.Controller.Type == CHARACTER_TYPE.HERO) 
        {
          var _heroController = (HeroController)characterData.Controller;
          List<CommonSkillFormat> _skillData = new List<CommonSkillFormat> ();
          foreach (var skill in _heroController.HeroDataCache.SkillList) 
          {
            _skillData.Add (skill);
          }

          CharacterSkill.Init (_skillData);

        }
        else if(characterData.Controller.Type == CHARACTER_TYPE.ENEMY)
        {
          var _enemyController = (EnemyController)characterData.Controller;

          List<CommonSkillFormat> _skillData = new List<CommonSkillFormat> ();
          foreach (var skill in _enemyController.EnemyDataCache.SkillList) 
          {
            _skillData.Add (skill);
          }

          CharacterSkill.Init (_skillData);
        }

        globalDataManager.RemoveValue (AbsCharacterControllerFormat.NAME, AbsCharacterControllerFormat.MEMORY_SPACE);
      }

    }


    void HeroControllerListChanged(CHARACTER_TYPE characterType,List<AbsCharacterController> controller)
    {
      if (this.characterType != characterType)
        return;

      // if slotID is 2, but list count is 2, there are only slotID 0,1 ,cause the list is sorted By SlotID
      if (controller.Count < this.slotID + 1) 
      {
        battleBottomManager.Close ();
        return;
      }

      this.characterController = controller [this.slotID];

      var _heroController = (HeroController)characterController; 
      List<CommonSkillFormat> _skillData = new List<CommonSkillFormat> ();
      foreach (var skill in _heroController.HeroDataCache.SkillList) 
      {
        _skillData.Add (skill);
      }

      CharacterSkill.Init (_skillData);
      CharacterAttribute.Init (this.characterController);
      CharacterAttribute.DataDisplay ();

    }

    void EnemyControllerListChanged(CHARACTER_TYPE characterType,List<AbsCharacterController> controller)
    {
      if (this.characterType != characterType)
        return;

      // if slotID is 2, but list count is 2, there are only slotID 0,1 ,cause the list is sorted By SlotID
      if (controller.Count < this.slotID + 1) 
      {
        battleBottomManager.Close ();
        return;
      }

      this.characterController = controller [this.slotID];

      var _enemyController = (EnemyController)characterController;

      List<CommonSkillFormat> _skillData = new List<CommonSkillFormat> ();
      foreach (var skill in _enemyController.EnemyDataCache.SkillList) 
      {
        _skillData.Add (skill);
      }

      CharacterSkill.Init (_skillData);
      CharacterAttribute.Init (this.characterController);
      CharacterAttribute.DataDisplay ();
    }

    BattleBottomManager battleBottomManager;
    AbsCharacterController characterController;
    int slotID;
    CHARACTER_TYPE characterType;
    HeroManager heroManager;
    EnemyManager enemyManager;
    GlobalDataManager globalDataManager;
  }
}
