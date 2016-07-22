using UnityEngine;

public class AddGold : MonoBehaviour 
{
  [SerializeField] private double addValue = 0;

  public void OnEventAction()
  {
    Party party = FindObjectOfType<Party>();
    if (party != null)
      GameManager.GM.AddGold(addValue);
  }  
}
