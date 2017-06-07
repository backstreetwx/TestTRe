using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.BattleInfo
{
  [System.Serializable]
  public class MonsterBattleTable : AbstractTable 
  {
//    public ushort ID;
    public ushort AreaLevelID;
    public short Area;
    public short Level;
    public ushort MonsterID;
    public int SlotID;
    public int Probability;
  }
}
