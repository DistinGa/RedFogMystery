using UnityEditor;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
  public override void OnInspectorGUI()
  {    
    Quest myTarget = (Quest)target;
    EditorGUILayout.LabelField("CurrentStep: " + myTarget.currentStep);
    DrawDefaultInspector();
  }
}
