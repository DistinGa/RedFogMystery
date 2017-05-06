using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Cut Scenes/Пауза")]
public class CSPause : CSEvent
{
    public float period;

    public override void OnEventAction()
    {
        Invoke("doPause", period);
    }

    void doPause()
    {
        if (NextStep != null)
            NextStep();
    }
}
