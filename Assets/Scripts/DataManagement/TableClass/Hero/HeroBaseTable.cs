using UnityEngine;
using System.Collections;

namespace DataManagement.TableClass.Hero
{
  [System.Serializable]
  public class HeroBaseTable : AbstractTable 
  {    
    public int Level;
    public int STRMin;
    public int VITMin;
    public int INTMin;
    public int DEXMin;
    public int RandomPointMax;
    public int AllPointMax;

    public short NameFormatID;
    public int AttributeMaximum;
  }
}
