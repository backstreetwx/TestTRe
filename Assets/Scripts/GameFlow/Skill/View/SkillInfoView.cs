using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Skill.Views{

  public class SkillInfoView : MonoBehaviour {

    public void Init()
    {
      selfText = GetComponent<Text> ();
    }

    public void ShowInfo(string info)
    {
      selfText.text = info;
    }
      
    Text selfText;

  }
}
