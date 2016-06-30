using UnityEngine;
using System.Collections;

[System.Serializable]
public class Properties
{
    public string Name = "";
    public Sprite Portrait = null;
    public float Hp = 0;
    public float Mp = 0;
    public float Cr = 0;
    public float Atk = 0;
    public float Def = 0;
    public float Mat = 0;
    public float Mdf = 0;
    public float Agi = 0;

    //Добавление воздействия от другого предмета
    public Properties Add(Properties ps)
    {
        Hp += ps.Hp;
        Mp += ps.Mp;
        Cr += ps.Cr;
        Atk += ps.Atk;
        Def += ps.Def;
        Mat += ps.Mat;
        Mdf += ps.Mdf;
        Agi += ps.Agi;

        return this;
    }
}

public enum ItemType
{
    Consumable,
    Equipment,
    Material,
    Key
}

[System.Serializable]
public class ItemPropetries : Properties
{
    public ItemType Type;
    public string Description = "Description";
}
