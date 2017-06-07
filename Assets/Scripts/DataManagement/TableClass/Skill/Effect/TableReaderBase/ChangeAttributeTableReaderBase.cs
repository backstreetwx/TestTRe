using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class ChangeAttributeTableReaderBase : AbstractTableReader<ChangeAttributeTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_change_attribute.csv";
      }
    }
  }
}
