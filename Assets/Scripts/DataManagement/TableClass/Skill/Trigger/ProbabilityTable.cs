using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Skill.Trigger
{
  [System.Serializable]
  public class ProbabilityTable : AbstractTable 
  {
    public float Cp_0;
    public float Cp_1;
    public bool EnableTrickLearning;
  }
}
