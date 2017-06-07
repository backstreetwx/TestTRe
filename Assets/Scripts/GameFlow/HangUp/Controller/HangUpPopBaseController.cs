using UnityEngine;
using System.Collections;
using GameFlow.Battle.Controller;

namespace GameFlow.HangUp.Controller
{
  public class HangUpPopBaseController : MonoBehaviour 
  {
    // Use this for initialization
    protected virtual void Start () 
    {
      this.popManager = FindObjectOfType<PopWindowManager> ();
    }

    public virtual void Close()
    {
      this.popManager.Close ();
    }

    PopWindowManager popManager;
  }
}
