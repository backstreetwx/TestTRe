using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InitHero.Views{

  public class NameOptionView : MonoBehaviour {

    public void Init()
    {
      textLabel = GetComponent<Text> ();
    }

    public void ShowName(string name)
    {
      textLabel.text = name;
    }

    Text textLabel;
  }

}
