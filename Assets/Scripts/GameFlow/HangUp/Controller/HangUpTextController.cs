using UnityEngine;
using System.Collections;
using Common.Controller;
using DataManagement.TableClass.HangUp;
using ConstCollections.PJEnums.HangUp;
using DataManagement.TableClass.TableReaderBase;

namespace GameFlow.HangUp.Controller
{
  public class HangUpTextController : AbsMultiLangTextController<HangUpStringFormatTable> 
  {

    public string LabelName;

    #region implemented abstract members of AbsMultiLangText

    protected override ushort ID 
    {
      get {
        object _target = System.Enum.Parse(typeof(STRINGS_LABEL), this.LabelName, true);

        if (_target == null)
          throw new System.NullReferenceException (string.Format("{0} was not found!", this.LabelName));
        else
          this.label = (STRINGS_LABEL)_target;

        ushort _ID = HangUpStringFormatTableReader.Instance.FindID (this.label);
        return _ID;
      }
    }

    protected override AbsMulitiLanguageTableReaderBase<HangUpStringFormatTable> readerInstance 
    {
      get {
        return HangUpStringFormatTableReader.Instance;
      }
    }

    #endregion

    [SerializeField, ReadOnly]
    STRINGS_LABEL label;
  }
}


