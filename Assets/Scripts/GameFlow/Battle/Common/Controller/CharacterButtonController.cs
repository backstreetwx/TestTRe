using UnityEngine;
using System.Collections;
using DataManagement.GameData.FormatCollection;
using ConstCollections.PJEnums.Character;
using GameFlow.Battle.Controller;
using DataManagement.GameData.FormatCollection.Battle;
using System.Linq;

namespace GameFlow.Battle.Common.Controller
{
  [RequireComponent(typeof(AbsCharacterController))]
  public class CharacterButtonController : MonoBehaviour 
  {
    public AbsCharacterController Controller;
    public GameObject CharacterInfoPrefab;
    void Start()
    {
      this.Controller = GetComponent<AbsCharacterController> ();
    }

    public void OnClick()
    {
      if (this.Controller == null)
        return;

      if (this.Controller.Type == CHARACTER_TYPE.HERO) 
      {
        var _heroManager = FindObjectOfType<HeroManager> ();
        //All dead 
        if (_heroManager.AliveList == null)
          return;
        HeroController _heroController = null;
        for (int i = 0; i < _heroManager.AliveList.Count; i++) 
        {
          if (this.Controller.SlotID == _heroManager.AliveList [i].SlotID)
            _heroController = (HeroController)_heroManager.AliveList [i];
        }
        //Target is dead, but show Animation
        if (_heroController == null)
          return;

        AbsCharacterControllerFormat _heroControllerData = new AbsCharacterControllerFormat (_heroController);
        FindObjectOfType<DataManagement.GlobalDataManager> ().SetValue (AbsCharacterControllerFormat.NAME, _heroControllerData, AbsCharacterControllerFormat.MEMORY_SPACE);
      } 
      else if (this.Controller.Type == CHARACTER_TYPE.ENEMY) 
      {
        var _enemyManager = FindObjectOfType<EnemyManager> ();
        //All dead 
        if (_enemyManager.AliveList == null)
          return;
        EnemyController _enemyController = null;
        for (int i = 0; i < _enemyManager.AliveList.Count; i++) 
        {
          if (this.Controller.SlotID == _enemyManager.AliveList [i].SlotID)
            _enemyController = (EnemyController)_enemyManager.AliveList [i];
        }
        //Target is dead, but show Animation
        if (_enemyController == null)
          return;

        AbsCharacterControllerFormat _enemyControllerData = new AbsCharacterControllerFormat (_enemyController);
        FindObjectOfType<DataManagement.GlobalDataManager> ().SetValue (AbsCharacterControllerFormat.NAME, _enemyControllerData, AbsCharacterControllerFormat.MEMORY_SPACE);
      }

      FindObjectOfType<BattleBottomManager> ().ShowWindow (CharacterInfoPrefab);
    }

    [SerializeField, ReadOnly]
    CharacterSimpleDataFormat simpleData;
  }

  [System.Serializable]
  public class AbsCharacterControllerFormat
  {
    public static string NAME = "Abs Character Controller";
    public static string MEMORY_SPACE = "Abs Character Controller Area";

    public AbsCharacterController Controller;

    public AbsCharacterControllerFormat(AbsCharacterController controller)
    {
      this.Controller = controller;
    }
  }
}
