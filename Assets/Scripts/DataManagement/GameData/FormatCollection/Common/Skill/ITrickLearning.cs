using UnityEngine;
using System.Collections;

namespace DataManagement.GameData.FormatCollection.Common.Skill
{
  public interface ITrickLearning 
  {
    bool EnableTrickLearning{ get; set;}
    float TrickLearningOffset{ get; set;}
  }
}
