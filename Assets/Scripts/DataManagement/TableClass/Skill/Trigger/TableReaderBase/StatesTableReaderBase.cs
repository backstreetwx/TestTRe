using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Trigger.TableReaderBase
{
  public class StatesTableReaderBase : AbstractTableReader<StatesTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/trigger_states.csv";
      }
    }
  }
}
