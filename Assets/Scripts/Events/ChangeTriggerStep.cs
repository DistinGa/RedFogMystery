using UnityEngine;

public class ChangeTriggerStep : MonoBehaviour
{
  [SerializeField] private TriggerBase trigger = null;
  [SerializeField] private int newValue = 1;

  public void OnEventAction()
  {
    if (trigger != null)
      trigger.CurrentStep = newValue;
    else
      Debug.LogWarning("Дмитрий! Объект " + gameObject.name + " trigger не назначен!");
  }
}
