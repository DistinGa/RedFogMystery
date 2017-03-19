using UnityEngine;
using System.Collections;
using System;

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
