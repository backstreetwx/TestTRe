using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Enemy.TableReaderBase
{
  public class BossTableReaderBase : AbstractTableReader<BossTable> 
  {
    public override string TablePath {
      get {
        return "Data/Enemy/boss.csv";
      }
    }
  }
}
