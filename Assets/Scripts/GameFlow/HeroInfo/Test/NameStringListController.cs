using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HeroInfo.Test{

  public class NameStringListController : MonoBehaviour {

    public List<string> NameList = new List<string> ();

    public string GetNameById(int id)
    {
      return NameList [id];
    }
  }

}
