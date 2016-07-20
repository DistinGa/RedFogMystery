using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllConsumables", menuName = "Scriptable Objects/Consumables", order = 0)]
public class SOConsumables : ScriptableObject
{
    public ConsumableProperties[] Consumables;

    public ConsumableProperties Get(int index)
    {
        return Consumables[index];
    }

    public ConsumableProperties[] Get(List<int> indexes)
    {
        ConsumableProperties[] ret = new ConsumableProperties[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
