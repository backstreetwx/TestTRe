using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class LastStandAttackPowerTableReaderBase : AbstractTableReader<LastStandAttackPowerTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_last_stand_attack_power.csv";
      }
    }
  }
}
