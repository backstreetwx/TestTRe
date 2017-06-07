using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Hero
{
  [System.Serializable]
  public class HeroTable : AbstractTable 
  {
    // For animation
    public string TexturePath;
    public int TextureIconID;
    public int TextureIdleID;
    public int TextureAttackID;
    public int TextureGetDamageID;
    public int TextureDeadID;
  }
}

