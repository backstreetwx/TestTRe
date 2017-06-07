using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Hero.TableReaderBase
{
  public class HeroBaseTableReaderBase : AbstractTableReader<HeroBaseTable> 
  {
    public override string TablePath {
      get {
        return "Data/Hero/hero_base.csv";
      }
    }
  }
}
