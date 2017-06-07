using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.HangUp
{
  [System.Serializable]
  public class HangUpConstTable : AbstractTable 
  {    
    public int TimeoutMiliseconds;
    public int RefreshTimeSeconds;
    public int VideoRewardTimes;
    public float CAMin;
    public float CAMax;
    public float CBMin;
    public float CBMax;
    public float CCMin;
    public float CCMax;
    public float CDemensionMin;
  }
}
