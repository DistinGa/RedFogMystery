using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

[CustomEditor(typeof(CSSetObjectProperty))]
public class CSSetObjectProperty_Editor : Editor
{
    //GameObject gobj;    //объект, свойство которого будет меняться в рантайме
    PropertyInfo PropInfo; //само свойство которое будет меняться в рантайме
    CSSetObjectProperty trg;    //редактируемый объект CSSetObjectProperty
    string oldPropName;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();

        trg.GameObject = EditorGUILayout.ObjectField(trg.GameObject, typeof(GameObject), true) as GameObject;

        if (trg.GameObject == null)
            return;

        //выбор компонента
        List<string> options = new List<string>();
        int i = 0, curOption = 0;
        foreach (var comp in trg.GameObject.GetComponents<Component>())
        {
            options.Add(comp.GetType().Name);
            if (comp == trg.Comp)
                curOption = i;

            i++;
        }

        int indx = EditorGUILayout.Popup("Component", curOption, options.ToArray());
        trg.Comp = trg.GameObject.GetComponents<Component>()[indx];

        if (trg.Comp == null)
            return;

        //выбор свойства
        options.Clear();
        i = 0;
        curOption = 0;
        foreach (var opt in trg.Comp.GetType().GetProperties())
        {
            if (!opt.CanWrite)
                continue;

            options.Add(opt.Name);
            if (opt.Name == trg.PropertyName)
                curOption = i;

            i++;
        }

        indx = EditorGUILayout.Popup("Value", curOption, options.ToArray());
        trg.PropertyName = options[indx];
        PropInfo = trg.Comp.GetType().GetProperty(options[indx]);

        if (PropInfo != null && oldPropName != trg.PropertyName)
        {
            oldPropName = trg.PropertyName;
            trg.ResetValues();
        }

        if (PropInfo != null)
        {
            if (PropInfo.PropertyType == typeof(int))
            {
                trg.intValue = EditorGUILayout.IntField(trg.intValue);
            }
            else if(PropInfo.PropertyType == typeof(bool))
            {
                trg.boolValue = EditorGUILayout.Toggle(trg.boolValue);
            }
            else if (PropInfo.PropertyType == typeof(float))
            {
                trg.floatValue = EditorGUILayout.FloatField(trg.floatValue);
            }
            else if (PropInfo.PropertyType == typeof(string))
            {
                trg.stringValue = EditorGUILayout.TextField(trg.stringValue);
            }
            else if (PropInfo.PropertyType == typeof(Color))
            {
                trg.colorValue = EditorGUILayout.ColorField(PropInfo.Name, trg.colorValue);
            }
            else if (PropInfo.PropertyType == typeof(Vector2))
            {
                trg.vector2Value = EditorGUILayout.Vector2Field(PropInfo.Name, trg.vector2Value);
            }
            else if (PropInfo.PropertyType == typeof(Vector3))
            {
                trg.vector3Value = EditorGUILayout.Vector3Field(PropInfo.Name, trg.vector3Value);
            }
            else if (PropInfo.PropertyType == typeof(Vector4))
            {
                trg.vector4Value = EditorGUILayout.Vector4Field(PropInfo.Name, trg.vector4Value);
            }
            else if (PropInfo.PropertyType == typeof(Quaternion))
            {
                trg.vector3Value = EditorGUILayout.Vector3Field(PropInfo.Name, trg.vector3Value);
            }
            else
            {
                trg.unityValue = EditorGUILayout.ObjectField(trg.unityValue, PropInfo.PropertyType, true);
            }

            serializedObject.ApplyModifiedProperties(); //для обновления изменений в ссылочных свойствах

        }

    }

    void OnEnable()
    {
        trg = (CSSetObjectProperty)target;
        oldPropName = trg.PropertyName;
        //if(trg.Comp != null)
        //    gobj = trg.GameObject;
    }

}
