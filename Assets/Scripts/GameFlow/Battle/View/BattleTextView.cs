using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;
using DataManagement.TableClass;
using ConstCollections.PJEnums;
using System.Linq;

namespace GameFlow.Battle.View
{
  public class BattleTextView : MonoBehaviour
  {
    public Queue BackMessageQueue;
    public Queue FrontMessageQueue;
    public ushort FrontLineCount = 10;

    [Range(0.0F, 1.0F)]
    public float IntervalSeconds = 0.01F;

    public bool StopUpdate;

    void Awake()
    {
      this.BackMessageQueue = new Queue ();
      this.FrontMessageQueue = new Queue();
      this.textComponent = GetComponent<Text> ();
    }

    void Start () 
    {
      StartCoroutine (this.UpdateCoroutine());
    }

    void OnDestroy()
    {
      StopAllCoroutines ();
    }

    IEnumerator UpdateCoroutine()
    {
      while (true) 
      {
        while (this.BackMessageQueue.Count > 0) 
        {

          if(this.FrontMessageQueue.Count >= FrontLineCount)
            this.FrontMessageQueue.Dequeue ();

          RefreshText ();

          var _new = this.BackMessageQueue.Dequeue ();
          this.FrontMessageQueue.Enqueue (_new);

          this.textComponent.text += _new.ToString() + "\n";

          yield return new WaitForSeconds (this.IntervalSeconds);
        }

        if (this.StopUpdate)
          break;

        // One frame break
        yield return new WaitForEndOfFrame();
      }
      yield break;
    }

    public void RefreshText()
    {
      this.textComponent.text = "";
      if (this.FrontMessageQueue.Count > 0) 
      {
        var _oldQueueBuffer = this.FrontMessageQueue.CloneEx();
        while (_oldQueueBuffer.Count > 0) 
        {
          string _strSingle = _oldQueueBuffer.Dequeue ().ToString ();
          this.textComponent.text += _strSingle + "\n";
        }
      }
    }

    Text textComponent;
  }
}
