using UnityEngine;

public class ChangeTriggerType : MonoBehaviour 
{
  [SerializeField] private TriggerBase trigger = null;
  [SerializeField] private TriggerType triggerType = TriggerType.Disabled;    

  public void OnEventAction()
  {
    if (trigger != null)
      trigger.Type = triggerType;
    else
      Debug.LogWarning("Дмитрий! Объект " + gameObject.name + " trigger не назначен!");
  }  
}
