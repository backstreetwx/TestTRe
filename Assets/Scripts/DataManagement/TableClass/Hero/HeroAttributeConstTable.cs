using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Hero
{
  [System.Serializable]
  public class HeroAttributeConstTable : AbstractTable 
  {
    public float HPMax; 
    public float RES;
    public float ATK;
    public float MAG;
    public float DEF;
    public float AC;
    public float CRI;
    public float PEN; 
    public float HIT; 
    public float AVD;
  }
}
