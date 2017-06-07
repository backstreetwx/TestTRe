using UnityEngine;
using System.Collections;
using ConstCollections.PJEnums.Skill;

namespace DataManagement.TableClass.Skill
{
  [System.Serializable]
  public class SkillTable : AbstractTable 
  {
//    public SKILL_CODE_NAME CodeName;
    public string CodeNameString;
    public SKILL_TYPE SkillType;
    public short ParentID;
    public string TexturePath;
    public int TextureIconID;
  }
}
