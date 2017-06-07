using UnityEngine;
using System.Collections;
using DataManagement.SaveData;
using System.Linq;

namespace DataManagement.TableClass.TableReaderBase
{
  [System.Serializable]
  public abstract class AbsMulitiLanguageTableReaderBase<T> : AbstractTableReader<T> where T : AbsMultiLanguageTable, new()
  {
    public virtual string GetString(ushort ID, SystemLanguage? lang = null)
    {
      AbsMultiLanguageTable _row =  base.FindDefaultUnique (ID);
      SystemLanguage _lang = lang ?? ConfigDataManager.Instance.UserLanguage;
      return GetString (_row, _lang);
    }
      
    public static string GetString(AbsMultiLanguageTable row, SystemLanguage lang)
    {
      if (row == null)
        return null;

      string _str;
      switch (lang) 
      {
      case SystemLanguage.English:
        _str = row.TextEN;
        break;
      case SystemLanguage.Japanese:
        _str = row.TextJP;
        break;
      case SystemLanguage.ChineseSimplified:
        _str = row.TextCNS;
        break;
      case SystemLanguage.ChineseTraditional:
        _str = row.TextCNT;
        break;
      case SystemLanguage.Chinese:
        _str = row.TextCNS;
        break;
      default:
        _str = row.TextEN;
        break;
      }

      return StringFormat (_str);
    }

    public static string StringFormat(string fromCSV)
    {
      if (fromCSV == null || fromCSV.Count() == 0)
        return null;

      return fromCSV.
        Replace("<br>", "\n").
        Replace("\\n", "\n").
        Replace("<cm>", ",").
        Replace("<dq>", "\"");
    }
  }
}

