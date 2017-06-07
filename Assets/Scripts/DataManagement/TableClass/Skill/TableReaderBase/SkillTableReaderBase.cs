using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.TableReaderBase
{
  public class SkillTableReaderBase : AbstractTableReader<SkillTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/skill.csv";
      }
    }
  }
}
