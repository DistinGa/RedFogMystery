using UnityEngine;
using System.Collections;

[AddComponentMenu("Cut Scenes/Текст над головой")]
public class CSOverheadText : CSEvent
{
    public OverheadTextScript targetObject;
    [Multiline][TextArea]
    public string text;
    public float delay;

    public override void OnEventAction()
    {
        GameManager.GM.OverheadText(targetObject.transform, text, delay);

        if (NextStep != null)
            NextStep();
    }
}
