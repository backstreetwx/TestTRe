using UnityEngine;
using System.Collections;
using DataManagement.SaveData;

namespace Test{

  public class AddResourceController : MonoBehaviour {

    public int AuraNum;
    public int ChipNum;

  	// Use this for initialization
  	void Start () 
    {
  	
  	}
  	
    public void AddAura()
    {
      UserSaveDataManager.Instance.Aura += AuraNum;
    }

    public void AddChip()
    {
      UserSaveDataManager.Instance.DimensionChip += ChipNum;
    }

  }
}
