using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Skill.Effect.TableReaderBase
{
  public class TrickLearningTableReaderBase : AbstractTableReader<TrickLearningTable> 
  {
    public override string TablePath {
      get {
        return "Data/Skill/effect_trick_learning.csv";
      }
    }
  }
}
