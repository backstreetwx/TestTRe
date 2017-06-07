using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataManagement.SaveData;
using DataManagement.TableClass;
using ConstCollections.PJEnums;

namespace Skill.Views{

  public class SkillOperaterView : MonoBehaviour {

    public string ButtonTextContent;

  	// Use this for initialization
  	public void Init () 
    {
      selfImage = GetComponent<Image> ();
      selfButton = GetComponent<Button> ();
      this.systemLanguage = ConfigDataManager.Instance.UserLanguage;
  	}
  	
    public void SetButtonSprites(Sprite idle,Sprite pressed)
    {
      SpriteState _spriteState = new SpriteState ();
      _spriteState = selfButton.spriteState;
      _spriteState.pressedSprite = pressed;
      selfButton.spriteState = _spriteState; 
      selfImage.sprite = idle;

    }

    public void ButtonClickableOrNot(bool temp)
    {
      if (temp) 
      {
        selfButton.interactable = true;
      } 
      else
      {
        selfButton.interactable = false;
      }
    }

    public void ButtonVisibleOrNot(bool temp)
    {
      if (temp) 
      {
        selfButton.gameObject.SetActive(true);
      } 
      else
      {
        selfButton.gameObject.SetActive(false);
      }
    }
    SystemLanguage systemLanguage;
    Button selfButton;
    Image selfImage;
  }
}
