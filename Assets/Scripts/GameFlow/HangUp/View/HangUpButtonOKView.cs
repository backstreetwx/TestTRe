using UnityEngine;
using System.Collections;
using Common.UI;
using GameFlow.HangUp.Controller;
using Common;

namespace GameFlow.HangUp.View
{
  public class HangUpButtonOKView : ButtonView 
  {
    public HangUpPopBaseController Controller;

    void OnEnable()
    {
      base.AddOnClick (Controller.Close);
    }
  }
}

