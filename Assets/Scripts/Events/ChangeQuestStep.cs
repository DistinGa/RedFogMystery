using UnityEngine;

public class ChangeQuestStep : MonoBehaviour
{
  [SerializeField] private Quest quest = null;
  [SerializeField] private int newValue = 1;

  public void OnEventAction()
  {
    Invoke("ChangeCurrentStep", 0);
  }

  void ChangeCurrentStep()
  {
    if (quest != null)
      quest.CurrentStep = newValue;
    else
      Debug.LogWarning("Объект " + gameObject.name + " quest не назначен!");
  }
}