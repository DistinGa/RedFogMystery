using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using System.Reflection;


[AddComponentMenu("Cut Scenes/Выполнить процедуру")]
public class CSStartMethod : CSEvent {
    public UnityEvent Method;

    public override void OnEventAction()
    {
        Method.Invoke();

        if (NextStep != null)
            NextStep();
    }

}
