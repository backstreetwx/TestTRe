using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Hero.TableReaderBase
{
  public class HeroNameFormatTableReaderBase : AbsMulitiLanguageTableReaderBase<HeroNameFormatTable>
  {
    public override string TablePath {
      get {
        return "Data/Hero/hero_name_format.csv";
      }
    }
  }
}
