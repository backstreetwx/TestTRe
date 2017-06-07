using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Hero.TableReaderBase
{
  public class HeroAttributeConstTableReaderBase : AbstractTableReader<HeroAttributeConstTable> 
  {
    public override string TablePath {
      get {
        return "Data/Hero/hero_attribute_const.csv";
      }
    }

    public HeroAttributeConstTable GetCachedValue()
    {
      return this.DefaultCachedList [0];
    }
  }
}
