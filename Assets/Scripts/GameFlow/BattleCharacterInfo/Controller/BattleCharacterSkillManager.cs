using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement.GameData.FormatCollection;
using GameFlow.Battle.Common.Controller;
using DataManagement.GameData.FormatCollection.Common.Skill;

namespace BattleCharacterInfo.Controllers{

  public class BattleCharacterSkillManager : MonoBehaviour {

    public int SkillNumber = 4;

    public BattleCharacterSkillController[] CharacterSkillArray;

    public void Init(List<CommonSkillFormat> skillList)
    {
      this.skillDataList = skillList;

      int[] _points = new int[SkillNumber];
      for(int i = 0; i < SkillNumber; i++)
      {
        _points [i] = -1;
      }

      int dataLength = this.skillDataList.Count;

      for (int i = 0; i < dataLength; i++) 
      {
        _points [i] = this.skillDataList[i].Level;
      }

      for(int i = 0; i < CharacterSkillArray.Length; i++)
      {
        CharacterSkillArray [i].Init (_points[i]);
      }

      for(int i = 0; i < this.skillDataList.Count; i++)
      {
        CharacterSkillArray [i].DisplaySkillInfo (this.skillDataList);
      }

    }

    List<CommonSkillFormat> skillDataList;

  }
}
