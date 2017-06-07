using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Character;

namespace DataManagement.TableClass.BattleInfo
{
  [System.Serializable]
  public class BossBattleTable : AbstractTable 
  {
    public ushort AreaLevelID;
    public short Area;
    public short Level;
    public ENEMY_TYPE Type;
    public ushort EnemyID;
    public int SlotID;
  }
}
