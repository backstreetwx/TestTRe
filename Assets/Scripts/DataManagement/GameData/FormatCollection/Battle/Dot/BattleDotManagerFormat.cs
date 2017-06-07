using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DataManagement.GameData.FormatCollection.Battle.Dot
{
  public class BattleDotManagerFormat 
  {
    public List<BattleDotFormat> DotList;

    public bool DotExist
    {
      get
      {
        return this.DotList.Count > 0;
      }
    }

    public BattleDotManagerFormat()
    {
      this.DotList = new List<BattleDotFormat> ();
    }

    public void Add(BattleDotFormat dot)
    {
      var _target = this.DotList.Find (item => {
        return item.Type == dot.Type;
      });

      if (_target != null)
        _target.Power += dot.Power;
      else
        this.DotList.Add (dot);
    }

    public void Active(FightDataFormat fightData)
    {
      this.DotList.ForEach (dot => dot.Active (fightData));
    }

    public void Clear()
    {
      this.DotList.Clear ();
    }

  }
}
