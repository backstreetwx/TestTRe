using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using DataManagement.SaveData;
using DataManagement.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataManagement.TableClass.Enemy.TableReaderBase
{
  [System.Serializable]
  public class MonsterNameTableReaderBase : AbsMulitiLanguageTableReaderBase<MonsterNameTable> 
  {
    public override string TablePath {
      get {
        return "Data/Enemy/monster_name.csv";
      }
    }
  }
}
