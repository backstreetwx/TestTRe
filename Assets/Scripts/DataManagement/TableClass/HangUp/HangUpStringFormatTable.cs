using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.HangUp;
using DataManagement.Common;

namespace DataManagement.TableClass.HangUp
{
  [System.Serializable]
  public class HangUpStringFormatTable : AbsMultiLanguageTable 
  {
    [CsvColumnAttribute(1)]
    public STRINGS_LABEL Label;
  }
}
