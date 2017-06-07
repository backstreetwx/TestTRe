using UnityEngine;
using UnityEditor;

namespace PJDebug.Editor
{
  [UnityEditor.CustomEditor(typeof(SaveDataDeugger))]
  public class SaveDataDeuggerEditor : UnityEditor.Editor
  {

    void OnEnable()
    {
      script = (SaveDataDeugger)target;

    }

    public override void OnInspectorGUI()
    {

      DrawDefaultInspector ();

      if (!Application.isPlaying)
        return;

      if (this.buttonStyle == null) 
      {
        this.buttonStyle = new GUIStyle (GUI.skin.button);
        this.buttonStyle.alignment = TextAnchor.MiddleLeft;
      }

      if (GUILayout.Button("Add Aura : " + script.OffsetAura, this.buttonStyle)) 
      {
        script.AddAura();
      }

      if (GUILayout.Button("Add DimesionChip : " + script.OffsetDimesionChip, this.buttonStyle)) 
      {
        script.AddDimesionChip();
      }

      if (GUILayout.Button("Add Level : " + script.OffsetLevel, this.buttonStyle)) 
      {
        script.AddLevel();
      }

      if (GUILayout.Button("Add Area : " + script.OffsetArea, this.buttonStyle)) 
      {
        script.AddArea();
      }

      if (GUILayout.Button("Set Level : " + script.Level, this.buttonStyle)) 
      {
        script.SetLevel();
      }

      if (GUILayout.Button("Set Area : " + script.Area, this.buttonStyle)) 
      {
        script.SetArea();
      }

      if (GUILayout.Button(string.Format( "Add Hero[{0}] EXP : {1}", script.HeroSlotID, script.OffsetEXP),this.buttonStyle)) 
      {
        script.AddHeroEXP();
      }

      if (GUILayout.Button("Add Active Hero EXP : " + script.OffsetEXP, this.buttonStyle)) 
      {
        script.AddActiveHeroEXP();
      }

      if (GUILayout.Button(string.Format( "Add Hero[{0}] Skill Point : {1}", script.HeroSlotID, script.OffsetSkillPoint), this.buttonStyle)) 
      {
        script.AddSkillPoint();
      }
        
      if (GUILayout.Button(string.Format( "Add Hero[{0}] \n{1}", script.HeroSlotID, script.OffsetHeroAttribute), this.buttonStyle)) 
      {
        script.AddHeroAttribute();
      }

    }

    SaveDataDeugger script;
    GUIStyle buttonStyle;
  }
}
