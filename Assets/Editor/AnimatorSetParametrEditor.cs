using UnityEditor;

[CustomEditor(typeof(AnimatorSetParametr))]
public class AnimatorSetParametrEditor : Editor
{
  public override void OnInspectorGUI()
  {
    AnimatorSetParametr myTarget = (AnimatorSetParametr)target;
    DrawDefaultInspector();
    switch (myTarget.ParametrType)
    {
      case ParametrType.Bool:
        myTarget.NewBool = EditorGUILayout.Toggle("NewBool", myTarget.NewBool);
        break;
      case ParametrType.Int:
        myTarget.NewInt = EditorGUILayout.IntField("NewInt", myTarget.NewInt);
        break;
      case ParametrType.Float:
        myTarget.NewFloat = EditorGUILayout.FloatField("NewFloat", myTarget.NewFloat);
        break;      
    }
    myTarget.ActionTime = EditorGUILayout.FloatField("ActionTime", myTarget.ActionTime);

    if (myTarget.ActionTime > 0)
    {
      myTarget.TargetQuest = EditorGUILayout.ObjectField("Script:", myTarget.TargetQuest, typeof(Quest), true) as Quest;
      myTarget.StepOnEndAction = EditorGUILayout.IntField("StepOnEndAction", myTarget.StepOnEndAction);      
    }    
  }
}
