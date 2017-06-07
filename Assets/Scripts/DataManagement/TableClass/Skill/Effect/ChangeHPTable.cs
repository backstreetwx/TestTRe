using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Skill.Effect
{
  [System.Serializable]
  public class ChangeHPTable : AbstractTable 
  {
    public float INTCp;
    public float INTSp;

    public float HPMaxCp;
    public float HPMaxSp;

    public float NormCp;
    public float NormSp;
  }
}
