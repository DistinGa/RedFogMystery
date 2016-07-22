using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable] public class HeroProperties : Properties
{
    public RuntimeAnimatorController AnimatorController = null;
    public int level;       //уровень
    public int expToLevelUp;//опыт до следуюющего уровня
    public float curHp;     //текущие очки здоровья
    public float curMp;     //текущие очки маны
    public float curCr;     //текущие очки коррупции

    public HeroProperties() { }

    public HeroProperties(Properties pr) : base(pr)
    { }
}


[Serializable]
public class Hero
{
    public bool isActive;   //персонаж в игре (присоединился к партии)
    public RuntimeAnimatorController AnimatorController = null;
    public LevelParameters lp;  //ScriptableObject с параметрами по уровням

    public int level;       //уровень
    public int expToLevelUp;//опыт до следуюющего уровня
    public float curHp = 0; //текущие очки здоровья
    public float curMp = 0; //текущие очки маны
    public float curCr = 0; //текущие очки коррупции

    //Экипировка
    int weapon;
    int armor;
    int helmet;
    int[] accessory = new int[2];

    public EquipmentProperties Weapon
    {
        set { weapon = value.index; }
        get { return (EquipmentProperties)GameManager.GM.AllEquipments.Get(weapon); }
    }

    public EquipmentProperties Armor
    {
        set { armor = value.index; }
        get { return (EquipmentProperties)GameManager.GM.AllEquipments.Get(armor); }
    }

    public EquipmentProperties Helmet
    {
        set { helmet = value.index; }
        get { return (EquipmentProperties)GameManager.GM.AllEquipments.Get(helmet); }
    }

    public EquipmentProperties Accessory1
    {
        set { accessory[0] = value.index; }
        get { return (EquipmentProperties)GameManager.GM.AllEquipments.Get(accessory[0]); }
    }

    public EquipmentProperties Accessory2
    {
        set { accessory[1] = value.index; }
        get { return (EquipmentProperties)GameManager.GM.AllEquipments.Get(accessory[1]); }
    }

    public HeroProperties HeroPropetries
    {
        get {
            HeroProperties HerProp = new HeroProperties(lp.Levels[level - 1]);
            //Добавление воздействия от предметов
            foreach (var item in accessory)
            {
                HerProp.Add(GameManager.GM.AllEquipments.Get(item));
            }
            HerProp.Add(Helmet);
            HerProp.Add(Armor);
            HerProp.Add(Weapon);

            HerProp.AnimatorController = AnimatorController;
            HerProp.level = level;
            HerProp.expToLevelUp = expToLevelUp;
            HerProp.curHp = curHp;
            HerProp.curMp = curMp;
            HerProp.curCr = curCr;

            return HerProp;
        }
    }

    //Возвращает свойства игрока, которые будут после ээкипировки указанного предмета
    public HeroProperties PredictProperties(EquipmentProperties NewEq, EquipmentProperties OldEq = null)
    {
        HeroProperties HerProp = HeroPropetries;
        if(OldEq != null)
            HerProp.Subtract(OldEq);

        HerProp.Add(NewEq);

        return HerProp;
    }

    //Обработка получения опыта
    public void AddExp(int exp)
    {
        expToLevelUp -= exp;
        if (expToLevelUp <= 0)
        {
            //переход на след. уровень
            int tmpExp = -expToLevelUp;
            if (level < lp.Levels.Length)
            {
                level++;
                expToLevelUp = lp.Levels[level-1].expToLevelUp;

                //Добавление остатка опыта
                if (tmpExp > 0)
                {
                    AddExp(tmpExp);
                }
            }
            else
                return;
        }
    }
}

