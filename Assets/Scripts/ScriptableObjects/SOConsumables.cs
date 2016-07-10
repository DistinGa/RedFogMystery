using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllConsumables", menuName = "Scriptable Objects/Consumables", order = 0)]
public class SOConsumables : ScriptableObject
{
    public ConsumablePropetries[] Consumables;

    public ConsumablePropetries Get(int index)
    {
        return Consumables[index];
    }

    public ConsumablePropetries[] Get(List<int> indexes)
    {
        ConsumablePropetries[] ret = new ConsumablePropetries[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
