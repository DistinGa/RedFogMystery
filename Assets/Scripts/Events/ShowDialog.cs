using UnityEngine;

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
}
