using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Enemy.TableReaderBase
{
  public class MonsterTableReaderBase : AbstractTableReader<MonsterTable> 
  {
    public override string TablePath {
      get {
        return "Data/Enemy/monster.csv";
      }
    }
  }
}
  