using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Title.Views{

  public class ButtonRelatedWithDataView : MonoBehaviour {

    public void Init()
    {
      button = GetComponent<Button> ();
    }

    public void SetInvisible()
    {
      button.gameObject.SetActive(false);
    }

    public void SetVisible()
    {
      button.gameObject.SetActive(true);
    }

    Button button;

  }

}
