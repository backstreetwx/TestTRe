using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using ConstCollections.PJEnums.HangUp;
using System.Linq;
using System.Reflection;

namespace DataManagement.TableClass.HangUp.TableReaderBase
{
  public class HangUpStringFormatTableReaderBase : AbsMulitiLanguageTableReaderBase<HangUpStringFormatTable> 
  {
    public static string ColumnLabelName = "Label";

    public override string TablePath {
      get {
        return "Data/HangUp/hangup_string_format.csv";
      }
    }

    public ushort FindID(STRINGS_LABEL label, SystemLanguage? lang = null)
    {
      var _rows = this.DefaultCachedList.FindAll(_row => {
        return _row.Label == label;
      });

      if (_rows.Count == 0)
        throw new System.NullReferenceException ();

      if (_rows.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _rows[0].ID;
    }
  }
}
