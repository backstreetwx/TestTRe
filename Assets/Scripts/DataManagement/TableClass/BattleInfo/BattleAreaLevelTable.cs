using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.BattleInfo
{
  [System.Serializable]
  public class BattleAreaLevelTable : AbstractTable 
  {
    public short Area;
    public short Level;
    public int SearchPointOutput;
    public int SearchPointPunishment;
    public int SearchPointForBoss;
    public string TexturePath;
  }
}
