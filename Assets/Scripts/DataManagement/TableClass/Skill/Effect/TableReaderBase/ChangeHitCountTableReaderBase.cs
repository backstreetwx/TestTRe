using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class ChangeHitCountTableReaderBase : AbstractTableReader<ChangeHitCountTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_change_hit_count.csv";
      }
    }
  }
}
