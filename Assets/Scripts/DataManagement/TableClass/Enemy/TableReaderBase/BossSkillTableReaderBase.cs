using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;

namespace DataManagement.TableClass.Enemy.TableReaderBase
{
  public class BossSkillTableReaderBase : AbstractTableReader<BossSkillTable> 
  {
    public override string TablePath {
      get {
        return "Data/Enemy/boss_skill.csv";
      }
    }

    public List<BossSkillTable> FindDefaultByMonsterID(ushort bossID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.BossID == bossID;
      });
    }
  }
}
