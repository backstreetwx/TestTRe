using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;
using ConstCollections.PJEnums.Skill;
using DataManagement.GameData.FormatCollection.Common;
using DataManagement.SaveData.FormatCollection;

namespace DataManagement.TableClass.Skill.TableReaderBase
{
  public class SkillTriggerTableReaderBase : AbstractTableReader<SkillTriggerTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/skill_trigger.csv";
      }
    }

    public List<SkillTriggerTable> FindBySkillID(ushort skillID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.SkillID == skillID;
      });
    }
  }
}
