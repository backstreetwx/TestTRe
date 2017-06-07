using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameFlow.Battle.Common.View
{
  public class CharacterHPView : MonoBehaviour 
  {
    public int HP
    {
      set
      { 
        this.hp = value;
        this.slider.value = this.hp;
      }
    }

    public int HPMax
    {
      set
      { 
        this.hpMax = value;
        this.slider.maxValue = this.hpMax;
      }
    }

    void OnEnable()
    {
      this.slider = GetComponent<Slider> ();
    }

//    // Use this for initialization
//    void Start () {
//      this.slider = GetComponent<Slider> ();
//    }

    // Update is called once per frame
    void Update () {

    }

    public void SetValue(int hp, int hpMax)
    {
      this.HPMax = hpMax;
      this.HP = hp;
    }

    Slider slider;

    [SerializeField, ReadOnly]
    public int hp;
    [SerializeField, ReadOnly]
    public int hpMax;
  }
}
