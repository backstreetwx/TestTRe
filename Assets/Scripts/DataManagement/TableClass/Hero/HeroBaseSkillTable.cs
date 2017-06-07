using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Hero
{
  [System.Serializable]
  public class HeroBaseSkillTable : AbstractTable 
  {
    public ushort HeroBaseID;
    public ushort SkillSlotID;
    public ushort SKillID;
  }
}
