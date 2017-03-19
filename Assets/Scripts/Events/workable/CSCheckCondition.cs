using UnityEngine;
using System.Collections;
using System;
using System.Reflection;


public class CSCheckCondition : CSEvent {
    public GameObject GameObject;
    public UnityEngine.Component Comp;

    public CSEvent TrueEvent, FalseEvent;

    [HideInInspector]
    public string PropertyName;

    [HideInInspector]
    public bool boolValue;
    [HideInInspector]
    public int intValue;
    [HideInInspector]
    public float floatValue;
    [HideInInspector]
    public string stringValue;
    [HideInInspector]
    public Vector2 vector2Value;
    [HideInInspector]
    public Vector3 vector3Value;
    [HideInInspector]
    public Vector4 vector4Value;
    [HideInInspector]
    public Quaternion quaternionValue;
    [HideInInspector]
    public Color colorValue;
    [HideInInspector]
    public UnityEngine.Object unityValue;
    
    public override void OnEventAction()
    {
        PropertyInfo PropInfo = Comp.GetType().GetProperty(PropertyName);
        var lv = PropInfo.GetValue(Comp, null);
        var rv = getActualValue(PropInfo);
        //if (PropInfo.GetValue(Comp, null) == getActualValue(PropInfo))
        if (lv.Equals(rv))
        {
            if(TrueEvent)
                TrueEvent.OnEventAction();
        }
        else
        {
            if(FalseEvent)
                FalseEvent.OnEventAction();
        }

        if (NextStep != null)
            NextStep();
    }

    private object getActualValue(PropertyInfo fInfo)
    {
        object res = 0;

        if (fInfo.PropertyType == typeof(bool))
            res = boolValue;
        else if (fInfo.PropertyType == typeof(int))
            res = intValue;
        else if (fInfo.PropertyType == typeof(float))
            res = floatValue;
        else if (fInfo.PropertyType == typeof(string))
            res = stringValue;
        else if (fInfo.PropertyType == typeof(Vector2))
            res = vector2Value;
        else if (fInfo.PropertyType == typeof(Vector3))
            res = vector3Value;
        else if (fInfo.PropertyType == typeof(Vector4))
            res = vector4Value;
        else if (fInfo.PropertyType == typeof(Quaternion))
            res = Quaternion.Euler(vector3Value.x, vector3Value.y, vector3Value.z);
        else if (fInfo.PropertyType == typeof(Color))
            res = colorValue;
        else if (fInfo.PropertyType == typeof(UnityEngine.Object))
            res = unityValue;

        return res;
    }

    public void ResetValues()
    {
        intValue = 0;
        boolValue = false;
        floatValue = 0;
        stringValue = "";
        colorValue = default(Color);
        vector2Value = default(Vector2);
        vector3Value = default(Vector3);
        vector4Value = default(Vector4);
        vector3Value = default(Vector3);
        unityValue = null;
    }
}
