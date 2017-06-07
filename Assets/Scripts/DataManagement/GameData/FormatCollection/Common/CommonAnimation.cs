using UnityEngine;
using System.Collections;

namespace DataManagement.GameData.FormatCollection.Common
{
  [System.Serializable]
  public class CommonAnimationFormat
  {
    public string TexturePath;
    public int IconID;
    public int IdleID;
    public int AttackID;
    public int GetDamageID;
    public int DeadID;

    public CommonAnimationFormat()
    {
      this.TexturePath = null;
      this.IconID = -1;
      this.IdleID = -1;
      this.AttackID = -1;
      this.GetDamageID = -1;
      this.DeadID = -1;
    }
  }
}
