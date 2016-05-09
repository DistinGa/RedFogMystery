using UnityEngine;
using System;

public class ShowDialog : TriggerBase
{
    public Sprite[] Portrait = null;

    public void OnEventAction()
    {
        base.StartDialog();
        dialogPanel.Show();
        currentLine = triggerNumLines[currentStep];
        SetDialog(currentLine);
    }

    public override void EndDialog()
    {
        base.EndDialog();
        dialogPanel.Hide();
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
