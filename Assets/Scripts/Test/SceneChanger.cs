using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DataManagement;
using DataManagement.SaveData;
using ConstCollections.PJConstStrings;
using DataManagement.SaveData.FormatCollection;

public class SceneChanger : MonoBehaviour {

  public string NextSceneName;

  void Start()
  {
  }

  public void Next()
  {
    SceneManager.LoadScene (this.NextSceneName);
  }

}
