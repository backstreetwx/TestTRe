using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Enemy
{
  [System.Serializable]
  public class MonsterSkillTable : AbstractTable 
  {
    public ushort MonsterID;
    public ushort SkillSlotID;
    public ushort SKillID;
    public int SkillLevel;
  }
}
