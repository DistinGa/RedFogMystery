using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
  [SerializeField] private Color colorOfEffect = Color.black;
  [SerializeField] private float timeOfEffect = 1;
  private CameraController cameraController = null;
  [HideInInspector] public float ActionTime = -1;
  [HideInInspector] public Quest TargetQuest = null;
  [HideInInspector] public int StepOnEndAction = 0;

  private void Start()
  {
    cameraController = FindObjectOfType<CameraController>();
  }

  public void OnEventAction()
  {
    cameraController.StartEffect(colorOfEffect, timeOfEffect);
    if (ActionTime > 0)
      Invoke("EndAction", ActionTime);
  }

  private void EndAction()
  {
    TargetQuest.CurrentStep = StepOnEndAction;
  }
}
