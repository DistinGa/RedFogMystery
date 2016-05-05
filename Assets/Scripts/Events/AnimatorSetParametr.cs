using UnityEngine;

public enum ParametrType
{
  Bool,  
  Int,
  Float,
  Trigger
}

public class AnimatorSetParametr : MonoBehaviour 
{  
  [SerializeField] private Animator targetAnimator = null;
  [SerializeField] private string paramName = "Enter parametr name...";
  public ParametrType ParametrType = ParametrType.Bool;
  [HideInInspector] public bool NewBool = false;
  [HideInInspector] public int NewInt = 0;
  [HideInInspector] public float NewFloat = 0;
  [HideInInspector] public int Delay = 0;
  [HideInInspector] public float ActionTime = -1;
  [HideInInspector] public Quest TargetQuest = null;
  [HideInInspector] public int StepOnEndAction = 0;


  public void OnEventAction()
  {
    if (targetAnimator != null)
    {
      switch (ParametrType)
      {
        case ParametrType.Bool:
          targetAnimator.SetBool(paramName, NewBool);
          break;
        case ParametrType.Int:
          targetAnimator.SetInteger(paramName, NewInt);
          break;
        case ParametrType.Float:
          targetAnimator.SetFloat(paramName, NewFloat);
          break;
        case ParametrType.Trigger:
          targetAnimator.SetTrigger(paramName);
          break;
      }
      if (ActionTime > 0)
          Invoke("EndAction", Delay);
    }
    else
      Debug.LogWarning("Объект " + gameObject.name + " targetAnimator не назначен!");    
  }

  private void EndAction()
  {
    TargetQuest.CurrentStep = StepOnEndAction;
  }
}
