using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllEquipments", menuName = "Scriptable Objects/Equipments", order = 0)]
public class SOEquipments : ScriptableObject
{
    public EquipmentPropetries[] Equipments;

    public EquipmentPropetries Get(int index)
    {
        return Equipments[index];
    }

    public EquipmentPropetries[] Get(List<int> indexes)
    {
        EquipmentPropetries[] ret = new EquipmentPropetries[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
