using UnityEngine;

public class BlockControl : MonoBehaviour 
{
  [SerializeField] private bool newValue = false;

  public void OnEventAction()
  {
    CharacterMoving characterMoving = FindObjectOfType<CharacterMoving>();
    if (characterMoving != null)
      characterMoving.Block = !newValue;
  }  
}
