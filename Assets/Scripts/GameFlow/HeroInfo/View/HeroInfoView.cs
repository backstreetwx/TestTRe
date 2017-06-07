using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Common.UI;

namespace HeroInfo.Views{

  public class HeroInfoView : MonoBehaviour {

    public void Init()
    {
      selfText = GetComponent<Text> ();
    }

    public void DataIntDisplay(float data)
    {
      
      selfText.text = Mathf.FloorToInt (data).ToString ();
      
    }

    public void DataStringDisplay(string data)
    {
      selfText.text = data;
    }

    Text selfText;
  }

}
