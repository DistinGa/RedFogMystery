using UnityEngine;
using System;

public class CameraMovingEffect : MonoBehaviour 
{      
  [SerializeField] private Animator targetAnimator = null;
  [SerializeField] private string paramName = "Enter parametr name...";
  [SerializeField] private bool newValue = false;
  [SerializeField] private float actionTime = -1;
  [SerializeField] private Quest targetQuest = null;
  [SerializeField] private int stepOnEndAction = 0;
  private CameraController cameraController = null;

  private void Start()
  {
    cameraController = FindObjectOfType<CameraController>();
  }

  public void OnEventAction()
  {
    if (targetAnimator != null)
    {
      cameraController.Target = targetAnimator.transform;
      targetAnimator.SetBool(paramName, newValue);          
      if (actionTime > 0)
        Invoke("EndAction", actionTime);
    }
    else
      Debug.LogWarning("Объект " + gameObject.name + " targetAnimator не назначен!");
  }

  private void EndAction()
  {
    targetQuest.CurrentStep = stepOnEndAction;
  }  
}
