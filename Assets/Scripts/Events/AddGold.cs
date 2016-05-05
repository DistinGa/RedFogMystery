using UnityEngine;

public class AddGold : MonoBehaviour 
{
  [SerializeField] private int addValue = 0;

  public void OnEventAction()
  {
    Party party = FindObjectOfType<Party>();
    if (party != null)
      party.Gold += addValue;
  }  
}
