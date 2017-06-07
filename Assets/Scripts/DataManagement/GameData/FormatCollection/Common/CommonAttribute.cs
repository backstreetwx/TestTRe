using UnityEngine;
using System.Collections;

namespace DataManagement.GameData.FormatCollection.Common
{
  [System.Serializable]
  public class CommonAttributeFormat
  {
    public int Level;
    public int HP;
    public int HPMax;
    public float RES;
    public float ATK;
    public float MAG;
    public float DEF;
    public float AC;
    public float CRI;
    public float PEN;
    public float HIT;
    public float AVD;

    public CommonAttributeFormat(bool clearToZero = false)
    {
      if (clearToZero == true) 
      {
        this.ClearToZero ();
        return;
      }

      this.Level = 0;
      this.HP = -1;
      this.HPMax = 0;
      this.RES = -1;
      this.ATK = -1;
      this.MAG = -1;
      this.DEF = -1;
      this.AC = -1;
      this.CRI = -1;
      this.PEN = -1;
      this.HIT = -1;
      this.AVD = -1;
    }

    public virtual void ClearToZero()
    {
      this.Level = 0;
      this.HP = 0;
      this.HPMax = 0;
      this.RES = 0;
      this.ATK = 0;
      this.MAG = 0;
      this.DEF = 0;
      this.AC = 0;
      this.CRI = 0;
      this.PEN = 0;
      this.HIT = 0;
      this.AVD = 0;
    }


    public static CommonAttributeFormat operator +(CommonAttributeFormat c1, CommonAttributeFormat c2) 
    {
      return c1.Add (c2);
    }

    protected virtual CommonAttributeFormat Add(CommonAttributeFormat c2)
    {
      var _sum = new CommonAttributeFormat (true);
      _sum.Level = this.Level + c2.Level;
      _sum.HP = this.HP + c2.HP;
      _sum.HPMax = this.HPMax + c2.HPMax;
      _sum.RES = this.RES + c2.RES;
      _sum.ATK = this.ATK + c2.ATK;
      _sum.MAG = this.MAG + c2.MAG;
      _sum.DEF = this.DEF + c2.DEF;
      _sum.AC = this.AC + c2.AC;
      _sum.CRI = this.CRI + c2.CRI;
      _sum.PEN = this.PEN + c2.PEN;
      _sum.HIT = this.HIT + c2.HIT;
      _sum.AVD = this.AVD + c2.AVD;

      return _sum;
    }
  }

}
