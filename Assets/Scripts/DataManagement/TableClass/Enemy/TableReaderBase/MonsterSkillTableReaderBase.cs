using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Enemy.TableReaderBase
{
  public class MonsterSkillTableReaderBase : AbstractTableReader<MonsterSkillTable> 
  {
    public override string TablePath {
      get {
        return "Data/Enemy/monster_skill.csv";
      }
    }

    public List<MonsterSkillTable> FindDefaultByMonsterID(ushort monsterID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.MonsterID == monsterID;
      });
    }
  }
}
