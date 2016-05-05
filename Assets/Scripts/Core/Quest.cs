using UnityEngine;
using System;

[Serializable] public class Step
{
  public string Description = "Action1...";
  public int ValueOfAction = 1;  
  public GameObject TargetObject = null;  
}

public class Quest : MonoBehaviour
{
  [SerializeField] public int currentStep = 0;
  [SerializeField] private Step[] steps = null;

  private int actioningStep = -1;

  public int CurrentStep
  {
    get { return currentStep; }
    set
    {
      currentStep = value;
      int i = 0;
      foreach (var step in steps)
      {
        if (step.ValueOfAction == currentStep)
        {
          actioningStep = i;
          DoAction();
        }
        i++;
      }      
    }
  }

  private void DoAction()
  {
    Debug.Log("Выполняется " + steps[actioningStep].TargetObject.name + " | Step = " + actioningStep);
    if (steps[actioningStep].TargetObject != null)
      steps[actioningStep].TargetObject.BroadcastMessage("OnEventAction");
    else
      Debug.LogWarning("На объекте " + gameObject.name + " Step " + actioningStep + " TargetObject не назначен!");   
  }
}
