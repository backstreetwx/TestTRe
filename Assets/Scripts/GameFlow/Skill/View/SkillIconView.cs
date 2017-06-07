using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Skill.Views{

  public class SkillIconView : MonoBehaviour {

    public void Init()
    {
      image = GetComponent<Image> ();
    }

    public void SetSkillIconByPath(string path, int textureID)
    {
      this.spriteList = Resources.LoadAll<Sprite> (path);
      image.sprite = this.spriteList[textureID];
    }

    public void SetSkillIconBySprite(Sprite spr)
    {
      image.sprite = spr;
    }

    Sprite[] spriteList;
    Image image;
  }
}
