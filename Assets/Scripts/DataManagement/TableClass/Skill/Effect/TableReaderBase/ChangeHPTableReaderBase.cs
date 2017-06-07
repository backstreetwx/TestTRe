using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class ChangeHPTableReaderBase : AbstractTableReader<ChangeHPTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_change_hp.csv";
      }
    }
  }
}
