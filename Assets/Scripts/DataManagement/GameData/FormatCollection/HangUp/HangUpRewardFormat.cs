using UnityEngine;
using System.Collections;
using DataManagement.TableClass.HangUp;

namespace DataManagement.GameData.FormatCollection.Common.HangUp
{
  [System.Serializable]
  public class HangUpRewardFormat
  {
    public int GotEXP;
    public int GotAura;
    public int GotDimensionChip;

    public HangUpRewardFormat(double deltaSeconds, short area, short level, HangUpConstTable constTable)
    {
      var _reward = HangUpRewardTableReader.Instance.FindDefaultUnique (area, level);

      var _deltaMins = (int) System.Math.Round(deltaSeconds / 60, 0);

      float _exp = _reward.EXP * _deltaMins;
      float _aura = _reward.Aura * _deltaMins;
      float _dimensionChip = _reward.DimensionChip * _deltaMins;

      var _a = Random.Range (constTable.CAMin, constTable.CAMax);
      var _b = Random.Range (constTable.CBMin, constTable.CBMax);
      var _c = Random.Range (constTable.CCMin, constTable.CCMax);

      _exp *= _a;
      _aura *= _a;
      _dimensionChip = this.GotDimensionChip * _b * _b + _c;
 
      _dimensionChip = Mathf.Max (constTable.CDemensionMin, _dimensionChip);

      this.GotEXP = Mathf.FloorToInt (_exp);
      this.GotAura = Mathf.FloorToInt (_aura);
      this.GotDimensionChip = Mathf.FloorToInt (_dimensionChip);
    }

    public override string ToString ()
    {
      return JsonUtility.ToJson(this,true);
    }
  }
}
