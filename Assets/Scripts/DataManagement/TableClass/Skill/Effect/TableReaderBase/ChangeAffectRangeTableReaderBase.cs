using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class ChangeAffectRangeTableReaderBase : AbstractTableReader<ChangeAffectRangeTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_change_affect_range.csv";
      }
    }
  }
}
