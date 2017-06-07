using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DataManagement.GameData.FormatCollection.Common.HangUp;
using Common;
using DataManagement.TableClass.HangUp;
using System.Text;
using ConstCollections.PJConstStrings;

namespace GameFlow.HangUp.Controller
{
  public class RewardTextController : MonoBehaviour {

    // Use this for initialization
    void Start () 
    {

      if (this.textScript == null)
        this.textScript = GetComponent<Text> ();
      
      var _globalData = FindObjectOfType<DataManagement.GlobalDataManager> ();
      var _data = _globalData.GetValue<HangUpRewardFormat>(HangUpString.REWARD, HangUpString.MEMORY_SPACE);
      Init (_data);
    }

    public void Init(HangUpRewardFormat data)
    {
      StringBuilder _finalStr = new StringBuilder();

      if (data.GotEXP > 0) 
      {
        var _id = HangUpStringFormatTableReader.Instance.FindID (ConstCollections.PJEnums.HangUp.STRINGS_LABEL.GET_EXP_TEXT);
        var _str = new MultiLangString<HangUpStringFormatTable> (_id, HangUpStringFormatTableReader.Instance, data.GotEXP); 
        _finalStr.Append (_str.ToString ());
      }

      if (data.GotAura > 0) 
      {
        _finalStr.AppendLine ();

        var _id = HangUpStringFormatTableReader.Instance.FindID (ConstCollections.PJEnums.HangUp.STRINGS_LABEL.GET_AURA_TEXT);
        var _str = new MultiLangString<HangUpStringFormatTable> (_id, HangUpStringFormatTableReader.Instance, data.GotAura); 
        _finalStr.Append (_str.ToString ());
      }

      if (data.GotDimensionChip > 0) 
      {
        _finalStr.AppendLine ();

        var _id = HangUpStringFormatTableReader.Instance.FindID (ConstCollections.PJEnums.HangUp.STRINGS_LABEL.GET_DIMENSION_CHIP_TEXT);
        var _str = new MultiLangString<HangUpStringFormatTable> (_id, HangUpStringFormatTableReader.Instance, data.GotDimensionChip); 
        _finalStr.Append (_str.ToString ());
      }

      if (this.textScript == null)
        this.textScript = GetComponent<Text> ();

      this.textScript.text = _finalStr.ToString ();
    }

    Text textScript;
  }
}
