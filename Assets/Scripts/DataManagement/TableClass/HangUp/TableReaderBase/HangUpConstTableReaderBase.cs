using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.HangUp.TableReaderBase
{
  public class HangUpConstTableReaderBase : AbstractTableReader<HangUpConstTable> 
  {
    public override string TablePath {
      get {
        return "Data/HangUp/hangup_const.csv";
      }
    }
  }
}
