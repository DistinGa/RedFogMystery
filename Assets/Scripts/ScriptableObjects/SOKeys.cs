using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllKeys", menuName = "Scriptable Objects/Keys", order = 0)]
public class SOKeys : ScriptableObject
{
    public KeyPropetries[] Keys;

    public KeyPropetries Get(int index)
    {
        return Keys[index];
    }

    public KeyPropetries[] Get(List<int> indexes)
    {
        KeyPropetries[] ret = new KeyPropetries[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
