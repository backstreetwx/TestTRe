using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InitHero.Views{
  
  public class PropertyView : MonoBehaviour {

    void Awake()
    {
      textDetail = GetComponent<Text> ();
    }

    public void ShowAttribute(float property, float propertyUp)
    {
      textDetail.text =  string.Format("{0,-4}({1:f1})", System.Convert.ToInt32(property), propertyUp);
    }

    Text textDetail;
  }
}
