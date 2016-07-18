using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllEquipments", menuName = "Scriptable Objects/Equipments", order = 0)]
public class SOEquipments : ScriptableObject
{
    public EquipmentProperties[] Equipments;

    public EquipmentProperties Get(int index)
    {
        return Equipments[index];
    }

    public EquipmentProperties[] Get(List<int> indexes)
    {
        EquipmentProperties[] ret = new EquipmentProperties[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
