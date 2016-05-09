using UnityEngine;

public class TriggerNPC : TriggerBase
{
  public Sprite Portrait = null;
  [SerializeField] private Animator bodyAnimator = null;
  protected Animator thisAnimator = null;
  private Vector3 previousPosition = Vector3.zero;
  private bool isRotateToCharacter = false;

    protected override void Start()
  {
    base.Start();
    previousPosition = transform.position;
    thisAnimator = GetComponent<Animator>();
    if (thisAnimator != null)
      thisAnimator.speed = Random.value * 0.5f + 0.5f;
  }

  protected override void SetDialog(int line)
  {
    base.SetDialog(line);
    if (allBoxes[0, line] != "")
    {
      Sprite otherPortrait = characterMoving.Portrait;
      dialogPanel.PortraitImage.enabled = true;
      dialogPanel.PortraitBackground.enabled = true;
      dialogPanel.PortraitImage.sprite = allBoxes[0, line] == "1" ? Portrait : otherPortrait;
    }  
  }

  public override void StartDialog()
  {
    base.StartDialog();
    dialogPanel.Show();
    currentLine = triggerNumLines[currentStep];
    SetDialog(currentLine);
    bodyAnimator.SetBool("Running", false);    
    if (thisAnimator != null)
      thisAnimator.speed = 0;
    characterMoving.KeyboardControl = false;
  }

  public override void EndDialog()
  {
    base.EndDialog();
    dialogPanel.Hide();
    characterMoving.KeyboardControl = true;
  }

  protected override void Update()
  {
    base.Update();
  }

  private void FixedUpdate()
  {
    if (!isRotateToCharacter)
    {
      bodyAnimator.SetFloat("SpeedX", transform.position.x - previousPosition.x);
      bodyAnimator.SetFloat("SpeedY", transform.position.y - previousPosition.y);
    }
    bodyAnimator.SetBool("Running", previousPosition != transform.position);
    previousPosition = transform.position;
  }

  protected override void OnCharacterTriggerEnter()
  {
    if (thisAnimator != null)
      thisAnimator.speed = 0;
    bodyAnimator.SetFloat("SpeedX", isCharacterRight ? 1 : -1);
    isRotateToCharacter = true;
  }

  protected override void OnCharacterTriggerExit()
  {
    if (thisAnimator != null)
      thisAnimator.speed = 1;
    isRotateToCharacter = false;
  }  
}
