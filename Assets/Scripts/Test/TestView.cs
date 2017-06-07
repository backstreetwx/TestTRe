using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Test{

  public class TestView : MonoBehaviour {

  	// Use this for initialization
  	public void Init () 
    {
      selfText = GetComponent<Text> ();
  	}
  	
    public void ShowMessage(string data)
    {
      selfText.text = data;
    }

    Text selfText;
  }
}
