using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Skill.Effect
{
  [System.Serializable]
  public class ChangeAttributeTable : AbstractTable 
  {
    public bool IsAggregate;

    public float HPCp;
    public float HPSp;

    public float HPMaxCp;
    public float HPMaxSp;

    public float RESCp;
    public float RESSp;

    public float ATKCp;
    public float ATKSp;

    public float MAGCp;
    public float MAGSp;

    public float DEFCp;
    public float DEFSp;

    public float ACCp;
    public float ACSp;

    public float CRICp;
    public float CRISp;

    public float PENCp;
    public float PENSp;

    public float HITCp;
    public float HITSp;

    public float AVDCp;
    public float AVDSp;

//    public float NormCp;
//    public float NormSp;
  }
}
