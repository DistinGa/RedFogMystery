using UnityEditor;

[CustomEditor(typeof(ScreenEffect))]
public class ScreenEffectEditor : Editor
{
  public override void OnInspectorGUI()
  {
    ScreenEffect myTarget = (ScreenEffect)target;
    DrawDefaultInspector();
    
    myTarget.ActionTime = EditorGUILayout.FloatField("ActionTime", myTarget.ActionTime);

    if (myTarget.ActionTime > 0)
    {
      myTarget.TargetQuest = EditorGUILayout.ObjectField("Script:", myTarget.TargetQuest, typeof(Quest), true) as Quest;
      myTarget.StepOnEndAction = EditorGUILayout.IntField("StepOnEndAction", myTarget.StepOnEndAction);      
    }    
  }
}
