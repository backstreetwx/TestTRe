using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.BattleInfo
{
  [System.Serializable]
  public class MonsterBattleSpawnTable : AbstractTable 
  {
//    public ushort ID;
    public ushort AreaLevelID;
    public short Area;
    public short Level;
    public int SpawnCount;
    public int Probability;
  }
}
