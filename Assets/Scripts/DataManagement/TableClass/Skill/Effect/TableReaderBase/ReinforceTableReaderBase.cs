using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class ReinforceTableReaderBase : AbstractTableReader<ReinforceTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_reinforce.csv";
      }
    }
  }
}
