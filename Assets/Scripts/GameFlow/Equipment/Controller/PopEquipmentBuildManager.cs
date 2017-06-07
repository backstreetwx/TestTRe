using UnityEngine;
using System.Collections;
using Common;

public class PopEquipmentBuildManager : SingletonObject<PopEquipmentBuildManager> {

  public GameObject WindowRootObject;

  public int WindowCount;

  protected override void Awake()
  {
    base.Awake ();
    this.WindowCount = 0;
  }

  void Start()
  {
    this.systemManager = FindObjectOfType<SystemManager>();
  }

  public void ShowWindow(GameObject prefab)
  {
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
    this.systemManager.PopObject ();
  }

  public void CloseAllRootWindows()
  {
    while (this.WindowCount > 0) 
    {
      this.systemManager.PopObject ();
    }
  }

  public void OnPopWindow()
  {
    this.WindowCount--;
  }

  SystemManager systemManager;
}
