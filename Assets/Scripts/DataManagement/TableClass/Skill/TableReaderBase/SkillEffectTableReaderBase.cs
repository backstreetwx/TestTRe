using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;

namespace DataManagement.TableClass.Skill.TableReaderBase
{
  public class SkillEffectTableReaderBase : AbstractTableReader<SkillEffectTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/skill_effect.csv";
      }
    }

    public List<SkillEffectTable> FindBySkillID(ushort skillID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.SkillID == (short)skillID;
      });
    }
  }
}
