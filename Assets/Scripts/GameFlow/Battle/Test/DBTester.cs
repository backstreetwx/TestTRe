using UnityEngine;
using System.Collections.Generic;
using DataManagement.TableClass.Enemy;
using DataManagement.SaveData;
using DataManagement.TableClass;
using DataManagement.TableClass.BattleInfo;

namespace GameFlow.Battle.Test
{
  [System.Serializable]
  public class EnemyDBManager
  {
//    public EnemyDBManager(List<EnemyTable> list)
//    {
//      this.EnemyList = list;
//    }


    public List<MonsterTable> EnemyList;
    public List<MonsterNameTable> EnemyNameList;

    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  [System.Serializable]
  public class BattleLevelInfoDBManager
  {
    public List<BattleAreaLevelTable> BattleLevelList;
    public override string ToString ()
    {
      return JsonUtility.ToJson (this, true);
    }
  }

  public class DBTester : MonoBehaviour 
  {
//    public List<EnemyTable> EnemyList;
    // Use this for initialization
    void Start () {
//      var _t = EnemyTableReader.Instance.DefaultCachedList;//new EnemyDBManager(EnemyTableReader.Instance.DefaultCachedList);
//
//      Debug.Log (_t[0]);
//      var _t = new EnemyDBManager();
//      _t.EnemyNameList = EnemyNameTableReader.Instance.DefaultCachedList;
//      Debug.Log (EnemyNameTableReader.Instance.GetString(0, SystemLanguage.Japanese));
      var _t = new BattleLevelInfoDBManager();
      _t.BattleLevelList = BattleAreaLevelTableReader.Instance.DefaultCachedList;
      var _t2 =  BattleAreaLevelTableReader.Instance.FindDefaultUnique (1,1);
      Debug.Log (_t);

        Debug.Log (_t2);
    }
      
  }
}

