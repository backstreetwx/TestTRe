using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Character;
using GameFlow.Battle.Controller;

namespace DataManagement.GameData.FormatCollection.Battle.Dot
{
  [System.Serializable]
  public class BattleDotFormat 
  {
    public DOT_TYPE Type;
    public float Power;

    public BattleInfoManager BattleInfoManagerScript{ 
      get{ 
        if (this.battleInfoManagerScript == null)
          this.battleInfoManagerScript = GameObject.FindObjectOfType<BattleInfoManager> ();

        return this.battleInfoManagerScript;
      }
    }

    public BattleDotFormat(DOT_TYPE type, float power, float damp)
    {
      this.Type = type;
      this.Power = power;
      this.damp = damp;
    }

    public void Active(FightDataFormat fightData)
    {
      var _power = Mathf.FloorToInt (this.Power);
      fightData.AttributesAggregateBuff.HP -= _power;

      this.BattleInfoManagerScript.EnqueueMessage (ConstCollections.PJEnums.Battle.INFO_FORMAT_LABEL.SKILL_DOT_BURN_DAMAGE, fightData, null, _power);

      this.Damp ();
    }

    void Damp()
    {
      this.Power *= this.damp;
    }

    [SerializeField]
    float damp;

    [System.NonSerialized]
    BattleInfoManager battleInfoManagerScript;
  }

}
