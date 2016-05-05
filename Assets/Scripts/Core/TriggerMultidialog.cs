using UnityEngine;
using System;

public class TriggerMultidialog : TriggerBase
{
  public Sprite[] Portrait = null;

  public override void StartDialog()
  {
    base.StartDialog();
    dialogPanel.Show();
    currentLine = triggerNumLines[currentStep];
    SetDialog(currentLine);
    characterMoving.KeyboardControl = false;
  }

  public override void EndDialog()
  {
    base.EndDialog();
    dialogPanel.Hide();
    characterMoving.KeyboardControl = true;    
  }

  protected override void SetDialog(int line)
  {
    base.SetDialog(line);
    if (allBoxes[0, line] != "")
    {
      dialogPanel.PortraitImage.enabled = true;
      dialogPanel.PortraitBackground.enabled = true;
      int portraitId = Int32.Parse(allBoxes[0, line]);
      dialogPanel.PortraitImage.sprite = Portrait[portraitId];
    }  
  }   
}
