using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameFlow.Battle.View{

  public class BattleAreaBackgroundView : MonoBehaviour {

    void Awake () 
    {
      spriteRender = GetComponent<SpriteRenderer> ();
    }

    public void SetAreaBG(string path)
    {
      Sprite _sprite = Resources.Load<Sprite> (path);
      spriteRender.sprite = _sprite;
    }

    SpriteRenderer spriteRender;
  }
}
