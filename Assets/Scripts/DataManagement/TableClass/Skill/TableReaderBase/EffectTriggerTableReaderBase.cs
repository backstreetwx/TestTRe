using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;

namespace DataManagement.TableClass.Skill.TableReaderBase
{
  public class EffectTriggerTableReaderBase : AbstractTableReader<EffectTriggerTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_trigger.csv";
      }
    }

    public List<EffectTriggerTable> FindDefault(short skillID, short effectID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.SkillID == skillID && row.EffectID == effectID; 
      });
    }
  }
}
