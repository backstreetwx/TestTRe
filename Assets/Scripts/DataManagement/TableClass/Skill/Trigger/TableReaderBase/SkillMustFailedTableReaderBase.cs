using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Trigger.TableReaderBase
{
  public class SkillMustFailedTableReaderBase : AbstractTableReader<SkillMustFailedTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/trigger_skill_must_failed.csv";
      }
    }
  }
}
