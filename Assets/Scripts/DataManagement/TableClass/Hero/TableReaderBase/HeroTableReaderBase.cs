using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Hero.TableReaderBase
{
  public class HeroTableReaderBase : AbstractTableReader<HeroTable> 
  {
    public override string TablePath {
      get {
        return "Data/Hero/hero.csv";
      }
    }
  }
}
