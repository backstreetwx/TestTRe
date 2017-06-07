using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class StandardAttackPowerTableReaderBase : AbstractTableReader<StandardAttackPowerTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_standard_attack_power.csv";
      }
    }
  }
}
