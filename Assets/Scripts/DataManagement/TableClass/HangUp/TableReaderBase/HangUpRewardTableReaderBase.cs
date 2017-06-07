using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.HangUp.TableReaderBase
{
  public class HangUpRewardTableReaderBase : AbstractTableReader<HangUpRewardTable> 
  {
    public override string TablePath {
      get {
        return "Data/HangUp/hangup_reward.csv";
      }
    }

    public HangUpRewardTable FindDefaultUnique(short area, short level)
    {
      var _rows = this.DefaultCachedList.FindAll (row => {
        return row.Area == area && row.Level == level;
      });

      if (_rows.Count == 0)
        throw new System.NullReferenceException ();

      if (_rows.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _rows[0];
    }
  }
}
