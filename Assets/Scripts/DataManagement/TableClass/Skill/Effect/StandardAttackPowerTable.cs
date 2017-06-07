using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;

namespace DataManagement.TableClass.Skill.Effect
{
  [System.Serializable]
  public class StandardAttackPowerTable : AbstractTable 
  {
    public float ATKCp;
    public float ATKSp;
    public float DEFCp;
    public float DEFSp;
    public float MAGCp;
    public float MAGSp;
    public float VITCp;
    public float VITSp;
    public float NormCp;
    public float NormSp;
  }
}
