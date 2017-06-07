using UnityEngine;
using Common;
using System;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using DataManagement.Common;
using DataManagement.SaveData;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{
  [System.Serializable]
  public class EquipmentStringsTableReaderBase : AbsMulitiLanguageTableReaderBase<EquipmentStringsTable> {

    public static string ColumnEquipmentIDName = "EquipmentID";

    public override string TablePath {
      get {
        return "Data/Equipment/equipment_strings.csv";
      }
    }

    public ushort FindID(ushort equipmentID,SystemLanguage? lang = null)
    {
      var _cacheList =  this.DefaultCachedList.FindAll (row => {
        return row.EquipmentID == equipmentID; 
      });

      if (_cacheList.Count == 0)
        throw new System.NullReferenceException ();

      if (_cacheList.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      return _cacheList[0].ID;

    }

    public string GetString(short equipmentID,SystemLanguage? lang = null)
    {
      var _cacheList =  this.DefaultCachedList.FindAll (row => {
        return row.EquipmentID == equipmentID; 
      });

      if (_cacheList.Count == 0)
        throw new System.NullReferenceException ();

      if (_cacheList.Count > 1)
        throw new System.Exception ("Find Unique but got duplicated!");

      SystemLanguage _lang = lang ?? ConfigDataManager.Instance.UserLanguage;
      return GetString (_cacheList[0], _lang);

    }
  	
  }
}
