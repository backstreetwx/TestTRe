using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;
using System.Collections.Generic;

namespace DataManagement.TableClass.Hero.TableReaderBase
{
  public class HeroBaseSkillTableReaderBase : AbstractTableReader<HeroBaseSkillTable> 
  {
    public override string TablePath {
      get {
        return "Data/Hero/hero_base_skill.csv";
      }
    }

    public List<HeroBaseSkillTable> FindDefaultByHeroBaseID(ushort heroBaseID)
    {
      return this.DefaultCachedList.FindAll (row => {
        return row.HeroBaseID == heroBaseID;
      });
    }
  }
}
