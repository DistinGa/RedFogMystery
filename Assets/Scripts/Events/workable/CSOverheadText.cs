using UnityEngine;
using System.Collections;

public class CSOverheadText : CSEvent
{
    public OverheadTextScript targetObject;
    [Multiline][TextArea]
    public string text;
    public float delay;

    public override void OnEventAction()
    {
        GameManager.GM.OverheadText(targetObject.transform, text, delay);
    }
}
