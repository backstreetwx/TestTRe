using UnityEngine;
using System.Collections;
using Common;

namespace GameFlow.Battle.Controller
{

  public class PopWindowManager : SingletonObject<PopWindowManager> 
  {
    public GameObject WindowRootObject;
    public int WindowCount;

    public SystemManager SystemManager
    {
      get
      {
        if (this.systemManager == null) 
        {
          this.systemManager = FindObjectOfType<SystemManager>();
        }

        return this.systemManager;
      }
    }

    protected override void Awake()
    {
      base.Awake ();
      this.WindowCount = 0;
    }

//    void Start()
//    {
//      this.systemManager = FindObjectOfType<SystemManager>();
//    }

    public void ShowWindow(GameObject prefab)
    {
      this.Close ();

      Object _gameObj = Instantiate (prefab, WindowRootObject.transform, false);
      this.SystemManager.PushObject (_gameObj, this.OnPopWindow);
      this.WindowCount++;
    }

    public void OnPopWindow()
    {
      this.WindowCount--;
    }


    public void Close()
    {
      while (this.WindowCount>0) 
      {
        this.SystemManager.PopObject ();
      }
    }

    SystemManager systemManager;
  }
}
