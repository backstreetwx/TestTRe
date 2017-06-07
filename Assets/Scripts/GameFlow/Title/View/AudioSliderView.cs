using UnityEngine;
using System.Collections;
using PJAudio;
using DataManagement.SaveData;
using UnityEngine.UI;
using ConstCollections.PJEnums;

namespace Title.Views{

  public class AudioSliderView : MonoBehaviour {

    public STRINGS_LABEL Settings;

    // Use this for initialization
    void Start () 
    {
      this.bgmPlayer = FindObjectOfType<BGMPlayer> ();
      this.sePlayer = FindObjectOfType<SEPlayer> ();

      this.slider = this.GetComponent<Slider> ();
      if (this.slider.onValueChanged.GetPersistentEventCount() == 0) 
      {
        #if UNITY_EDITOR
        UnityEditor.Events.UnityEventTools.AddPersistentListener(this.slider.onValueChanged, OnValueChanged);
        #else
        this.slider.onValueChanged.AddListener (OnValueChanged);
        #endif
      }

      switch (Settings) 
      {
      case STRINGS_LABEL.BGM_LABEL:
        this.slider.value = ConfigDataManager.Instance.BGMVolume;
        break;
      case STRINGS_LABEL.SE_LABEL:
        this.slider.value = ConfigDataManager.Instance.SEVolume;
        break;
      default:
        break;
      }
    }

    void OnValueChanged(float value)
    {
      switch (Settings) 
      {
      case STRINGS_LABEL.BGM_LABEL:
        ConfigDataManager.Instance.BGMVolume = value;
        break;
      case STRINGS_LABEL.SE_LABEL:
        ConfigDataManager.Instance.SEVolume = value;
        break;
      default:
        break;
      }
    }

    Slider slider;
    BGMPlayer bgmPlayer;
    SEPlayer sePlayer;
  }
}

