using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using DG.Tweening;

[AddComponentMenu("Cut Scenes/Свойство объекта")]
public class CSSetObjectProperty : CSEvent {
    public GameObject GameObject;
    public UnityEngine.Component Comp;
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
    //анимация
    public bool Animate;
    public float Duration;
    public Ease AnimaType;

    public override void OnEventAction()
    {
        //FieldInfo fInfo;

        //fInfo = Comp.GetType().GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Instance);
        //if (fInfo != null)
        //    fInfo.SetValue(Comp, getActualValue(fInfo));
        //else
        //    Debug.LogError("На объекте не найдено поле <" + PropertyName + ">", Comp);

        PropertyInfo PropInfo = Comp.GetType().GetProperty(PropertyName);
        if (Animate)
        {
            Tweener myTween = null;

            if (PropInfo.PropertyType == typeof(bool))
                PropInfo.SetValue(Comp, getActualValue(PropInfo), null);
            else if (PropInfo.PropertyType == typeof(int))
                myTween = DOTween.To(() => (int)getter(), x => PropInfo.SetValue(Comp, x, null), intValue, Duration);
            else if (PropInfo.PropertyType == typeof(float))
                myTween = DOTween.To(() => (float)getter(), x => PropInfo.SetValue(Comp, x, null), floatValue, Duration);
            else if (PropInfo.PropertyType == typeof(string))
                PropInfo.SetValue(Comp, getActualValue(PropInfo), null);
            else if (PropInfo.PropertyType == typeof(Vector2))
                myTween = DOTween.To(() => (Vector2)getter(), x => PropInfo.SetValue(Comp, x, null), vector2Value, Duration);
            else if (PropInfo.PropertyType == typeof(Vector3))
                myTween = DOTween.To(() => (Vector3)getter(), x => PropInfo.SetValue(Comp, x, null), vector3Value, Duration);
            else if (PropInfo.PropertyType == typeof(Vector4))
                myTween = DOTween.To(() => (Vector4)getter(), x => PropInfo.SetValue(Comp, x, null), vector4Value, Duration);
            //else if (PropInfo.PropertyType == typeof(Quaternion))
            //    DOTween.To(() => (Quaternion)getter(), x => PropInfo.SetValue(Comp, x, null), Quaternion.Euler(vector3Value.x, vector3Value.y, vector3Value.z), Duration);
            else if (PropInfo.PropertyType == typeof(Color))
                myTween = DOTween.To(() => (Color)getter(), x => PropInfo.SetValue(Comp, x, null), colorValue, Duration);
            else if (PropInfo.PropertyType == typeof(UnityEngine.Object))
                PropInfo.SetValue(Comp, getActualValue(PropInfo), null);

            myTween.SetLoops(2, LoopType.Yoyo).SetEase(AnimaType);
        }
        else
            PropInfo.SetValue(Comp, getActualValue(PropInfo), null);

        if (NextStep != null)
            NextStep();

    }

    private object getter()
    {
        PropertyInfo PropInfo = Comp.GetType().GetProperty(PropertyName);
        return PropInfo.GetValue(Comp, null);
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
