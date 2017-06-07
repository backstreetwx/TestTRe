using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;
using ConstCollections.PJEnums.BattleBottom;


namespace GameFlow.Battle.Controller{

  public class BattleBottomManager : SingletonObject<BattleBottomManager> {

    public GameObject WindowRootObject;

    public GameObject HeroInfoPrefab;
    public GameObject SettingsPrefab;

    public BUTTON_POP_STATE State;

    public Stack<System.Action<POP_WINDOW_NEXT_TO>> CallBackStack;

    public int WindowCount;

    protected override void Awake()
    {
      base.Awake ();
      this.WindowCount = 0;
    }

    void Start()
    {
      State = BUTTON_POP_STATE.CAN_POP;
      this.systemManager = FindObjectOfType<SystemManager>();
      CallBackStack = new Stack<System.Action<POP_WINDOW_NEXT_TO>> ();
    }

    public void ShowWindow(GameObject prefab)
    {

      this.Close ();
              
      Object _gameObj = Instantiate (prefab, WindowRootObject.transform, false);
      this.systemManager.PushObject (_gameObj, OnPopWindow);
      this.WindowCount++;
    }

    public void ToggleActiveGameObject(GameObject gameObject)
    {
      gameObject.SetActive (!gameObject.activeSelf);
    }

    public void Close()
    {
      while (this.WindowCount>0) 
      {
        this.systemManager.PopObject ();
      }
    }

    public void OnPopWindow()
    {
      this.WindowCount--;
    }

    public void PopTo(string nextToString)
    {
      object _enum = System.Enum.Parse(typeof(POP_WINDOW_NEXT_TO), nextToString, true);
      POP_WINDOW_NEXT_TO nextTo = POP_WINDOW_NEXT_TO.NONE;
      if (_enum == null)
        return;
      else
        nextTo = (POP_WINDOW_NEXT_TO)_enum;

      if (State == BUTTON_POP_STATE.CAN_NOT_POP) 
      {
        if (CallBackStack.Count > 0) 
        {
          var _callBack = CallBackStack.Peek();
          if (_callBack != null)
            _callBack (nextTo);
        }
      } 
      else if (State == BUTTON_POP_STATE.CAN_POP) 
      {
        switch(nextTo)
        {
        case POP_WINDOW_NEXT_TO.ACHIEVEMENT:
          break;
        case POP_WINDOW_NEXT_TO.MAP:
          break;
        case POP_WINDOW_NEXT_TO.BATTLE:
          Close ();
          break;
        case POP_WINDOW_NEXT_TO.HERO:
          ShowWindow (HeroInfoPrefab);
          break;
        case POP_WINDOW_NEXT_TO.STONE:
          break;
        case POP_WINDOW_NEXT_TO.SETTINGS:
          ShowWindow (SettingsPrefab);
          break;
        }
      }
    }

    public void SetState(BUTTON_POP_STATE state,System.Action<POP_WINDOW_NEXT_TO> callBack)
    {
      this.State = state;
      if (callBack != null)
        CallBackStack.Push (callBack);

    }

    SystemManager systemManager;
  }
}
