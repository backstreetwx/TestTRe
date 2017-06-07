using UnityEngine;
using System.Collections;
using DataManagement.TableClass.BattleInfo;

namespace Test{

  public class TestBattleAreaScene : MonoBehaviour {

    public TestAreaSceneView AreaView;

    void Start()
    {
      var _t = BattleAreaLevelTableReader.Instance.FindDefaultUnique(0,0);

      AreaView.SetAreaBG (_t.TexturePath);
    }
  	
  }
}
