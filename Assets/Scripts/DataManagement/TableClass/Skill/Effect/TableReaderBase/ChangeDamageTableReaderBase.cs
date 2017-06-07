using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class ChangeDamageTableReaderBase : AbstractTableReader<ChangeDamageTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_change_damage.csv";
      }
    }
  }
}
