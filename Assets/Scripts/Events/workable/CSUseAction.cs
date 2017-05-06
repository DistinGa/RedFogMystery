using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Cut Scenes/Действие по кнопке")]
public class CSUseAction : CSEvent
{
    [SerializeField]
    CSEvent Script;

    public override void OnEventAction()
    {
        if (Script == null)
        {
            //Debug.LogError("Не назначен скрипт CSEvent", this);
            return;
        }

        Script.OnEventAction();
    }

    void Update ()
    {
        if (Input.GetAxisRaw("Submit") > 0)
        {
            OnEventAction();
        }
    }
}
