using UnityEngine;
using System.Collections;
using DataManagement.TableClass.TableReaderBase;

namespace DataManagement.TableClass.Equipment.TableReaderBase
{
  public class EquipmentOtherValueTableReaderBase : AbstractTableReader<EquipmentOtherValueTable> {

    public override string TablePath {
      get {
        return "Data/Equipment/equipment_other_value.csv";
      }
    }

  }
}
