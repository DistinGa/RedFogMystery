using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllMaterials", menuName = "Scriptable Objects/Materials", order = 0)]
public class SOMaterials : ScriptableObject, IInventorySO
{
    public MaterialProperties[] Materials;

    public Properties Get(int index)
    {
        return Materials[index];
    }

    //public MaterialProperties[] Get(List<int> indexes)
    //{
    //    MaterialProperties[] ret = new MaterialProperties[indexes.Count];
    //    for(int i = 0; i < indexes.Count; i++)
    //    {
    //        ret[i] = Get(indexes[i]);
    //    }

    //    return ret;
    //}
}
