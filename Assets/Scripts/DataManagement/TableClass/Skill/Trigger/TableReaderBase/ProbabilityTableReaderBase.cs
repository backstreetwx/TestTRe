using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Trigger.TableReaderBase
{
  public class ProbabilityTableReaderBase : AbstractTableReader<ProbabilityTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/trigger_probability.csv";
      }
    }
  }
}
