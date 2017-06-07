using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Enemy
{
  [System.Serializable]
  public class BossSkillTable : AbstractTable 
  {
    public ushort BossID;
    public ushort SkillSlotID;
    public ushort SKillID;
    public int SkillLevel;
  }
}
