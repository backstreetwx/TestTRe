using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Battle;
using DataManagement.Common;

namespace DataManagement.TableClass.BattleInfo
{
  [System.Serializable]
  public class BattleStringFormatTable : AbsMultiLanguageTable 
  {
    [CsvColumnAttribute(1)]
    public INFO_FORMAT_LABEL Label;
  }
}
