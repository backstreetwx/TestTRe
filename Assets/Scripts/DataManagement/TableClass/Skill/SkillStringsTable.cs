using UnityEngine;
using System.Collections;
using System.Linq;
using ConstCollections.PJEnums;
using ConstCollections.PJEnums.Skill;
using DataManagement.Common;

namespace DataManagement.TableClass.Skill
{
  [System.Serializable]
  public class SkillStringsTable : AbsMultiLanguageTable 
  {
    [CsvColumnAttribute(1)]
    public short SkillID;

    [CsvColumnAttribute(2)]
    public SKILL_STRINGS_LABEL Label;
  }
}

