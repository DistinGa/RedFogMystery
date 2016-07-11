using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllMaterials", menuName = "Scriptable Objects/Materials", order = 0)]
public class SOMaterials : ScriptableObject
{
    public MaterialPropetries[] Materials;

    public MaterialPropetries Get(int index)
    {
        return Materials[index];
    }

    public MaterialPropetries[] Get(List<int> indexes)
    {
        MaterialPropetries[] ret = new MaterialPropetries[indexes.Count];
        for(int i = 0; i < indexes.Count; i++)
        {
            ret[i] = Get(indexes[i]);
        }

        return ret;
    }
}
