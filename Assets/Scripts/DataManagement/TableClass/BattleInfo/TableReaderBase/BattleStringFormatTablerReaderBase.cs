using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using ConstCollections.PJEnums.Battle;
using DataManagement.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataManagement.SaveData;

namespace DataManagement.TableClass.BattleInfo.TableReaderBase
{
  [System.Serializable]
  public class BattleStringFormatTablerReaderBase : AbsMulitiLanguageTableReaderBase<BattleStringFormatTable> 
  {
    public static string ColumnLabelName = "Label";

    public override string TablePath {
      get {
        return "Data/BattleInfo/battle_string_format.csv";
      }
    }

    public ushort FindID(INFO_FORMAT_LABEL label, SystemLanguage? lang = null)
    {
      var _rows = this.DefaultCachedList.Where(_row => {
        // field
        FieldInfo fieldInfo = _row.GetType ().GetField (ColumnLabelName, BindingFlags.Instance | BindingFlags.Public);
        if(fieldInfo == null)
          return false;

        return ((INFO_FORMAT_LABEL)fieldInfo.GetValue (_row) == label);
      }).ToList ();

      if (_rows.Count == 0)
        throw new System.NullReferenceException ();

      if (_rows.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _rows[0].ID;
    }

    public string FindString(INFO_FORMAT_LABEL label, SystemLanguage? lang = null)
    {
      if (label == INFO_FORMAT_LABEL.BLANK_LINE)
        return "";

      var _rows = this.DefaultCachedList.Where(_row => {
        // field
        FieldInfo fieldInfo = _row.GetType ().GetField (ColumnLabelName, BindingFlags.Instance | BindingFlags.Public);
        if(fieldInfo == null)
          return false;

        return ((INFO_FORMAT_LABEL)fieldInfo.GetValue (_row) == label);
      }).ToList ();

      if (_rows.Count == 0)
        throw new System.NullReferenceException ();

      if (_rows.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      SystemLanguage _lang = lang ?? ConfigDataManager.Instance.UserLanguage;
      return GetString (_rows[0], _lang);
    }
  }
}
