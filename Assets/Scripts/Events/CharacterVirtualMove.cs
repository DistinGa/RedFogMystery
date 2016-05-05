using UnityEngine;
using System;

[Serializable] public class MovePropetries
{
  public Direction Direction = Direction.Left;
  public float Time = 1;
}

public enum Direction
{
  None,
  Left,
  Right,
  Down,
  Up
}
public class CharacterVirtualMove : MonoBehaviour 
{
  [SerializeField] private Quest targetQuest = null;
  [SerializeField] private int stepOnEndAction = 0;
    // DM
  [SerializeField] private int TimeTillEnd = 0;
    // DM
  [SerializeField] private MovePropetries[] MovePropetrieses = null;  
  private CharacterMoving characterMoving = null;
  private int currentStep = 0;

  private void Start()
  {    
    characterMoving = FindObjectOfType<CharacterMoving>();
  }

  public void OnEventAction()
  {    
    // DM
      if(TimeTillEnd > 0)
          Invoke("SetDirection", TimeTillEnd);
    // DM
      else
          SetDirection();
  }

  private void SetDirection()
  {
    if (MovePropetrieses.Length > currentStep)
    {
      characterMoving.AutoMoveDirection = MovePropetrieses[currentStep].Direction;
      Invoke("SetDirection", MovePropetrieses[currentStep].Time);
      ++currentStep;
    }
    else
    {
      characterMoving.AutoMoveDirection = Direction.None;
      targetQuest.CurrentStep = stepOnEndAction;
    }
  }  
}
