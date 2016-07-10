using UnityEngine;
using System.Collections;

[System.Serializable]
public class Properties
{
    public Sprite Portrait = null;
    public string Name = "";
    public string Description = "";
    public float Hp = 0;
    public float Mp = 0;
    public float Cr = 0;
    public float Atk = 0;
    public float Def = 0;
    public float Mat = 0;
    public float Mdf = 0;
    public float Agi = 0;

    public Properties() { }

    public Properties(Properties pr)
    {
        Portrait = pr.Portrait;
        Name = pr.Name;
        Description = pr.Description;
        Hp = pr.Hp;
        Mp = pr.Mp;
        Cr = pr.Cr;
        Atk = pr.Atk;
        Def = pr.Def;
        Mat = pr.Mat;
        Mdf = pr.Mdf;
        Agi = pr.Agi;
    }

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

public enum EquipmentType
{
    Weapon,
    Armor,
    Helmet,
    Accessory
}

//Кто может использовать данную экипировку
public enum EquipmentApplication
{
    Common,         //доступно всем
    LevelDepend,    //доступно персонажу достигшему определённого уровня
    Personal        //доступно только определённому персонажу
}

[System.Serializable]
public class ConsumablePropetries : Properties
{
    public int index;
}

[System.Serializable]
public class MaterialPropetries : Properties
{
    public int index;
}

[System.Serializable]
public class KeyPropetries : Properties
{
    public int index;
}

[System.Serializable]
public class EquipmentPropetries : Properties
{
    public int index;
    public EquipmentType equipType;
    public EquipmentApplication equipApplication;

    [Tooltip("Уровень, с которого доступен предмет (устанавливается при LevelDepend)")]
    public int forLevel;    //уровень, с которого доступен предмет (устанавливается при LevelDepend)
    [Tooltip("Имя героя, кому доступен предмет (устанавливается при Personal)")]
    public string HeroName; //Имя героя, кому доступен предмет (устанавливается при Personal)
}
