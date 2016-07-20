using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllKeys", menuName = "Scriptable Objects/Keys", order = 0)]
public class SOKeys : ScriptableObject
{
    public KeyProperties[] Keys;

    public KeyProperties Get(int index)
    {
        return Keys[index];
    }

    public KeyProperties[] Get(List<int> indexes)
    {
        KeyProperties[] ret = new KeyProperties[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
