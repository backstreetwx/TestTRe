using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameFlow.Battle.Common.View{

  public class SearchProgressView : MonoBehaviour 
  {
    public int SearchProgress
    {
      set
      { 
        this.searchProgress = value;
        this.slider.value = this.slider.maxValue - this.searchProgress;
      }
    }

    void Awake()
    {
      this.slider = GetComponent<Slider> ();
    }

    Slider slider;

    [SerializeField, ReadOnly]
    public int searchProgress;
    
  }
}
