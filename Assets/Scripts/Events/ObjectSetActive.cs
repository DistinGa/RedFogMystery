using UnityEngine;

public class ObjectSetActive : MonoBehaviour 
{
  [SerializeField] private GameObject targetObject = null;
  [SerializeField] private bool newValue = false;

  public void OnEventAction()
  {
    if (targetObject != null)    
      targetObject.SetActive(newValue);     
    else
      Debug.LogWarning("Объект " + gameObject.name + " targetObject не назначен!");
  }  
}
