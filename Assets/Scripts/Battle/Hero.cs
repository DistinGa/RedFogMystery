using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable] public class HeroProperties : Properties
{
    public Sprite Portrait = null;
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
    public Sprite Portrait = null;
    public RuntimeAnimatorController AnimatorController = null;
    public LevelParameters lp;  //ScriptableObject с параметрами по уровням

    public int level;       //уровень
    public int expToLevelUp;//опыт до следуюющего уровня
    public float curHp = 0; //текущие очки здоровья
    public float curMp = 0; //текущие очки маны
    public float curCr = 0; //текущие очки коррупции

    public HeroProperties HeroPropetries
    {
        get {
            HeroProperties HerProp = new HeroProperties(lp.Levels[level - 1]);
            HerProp.Portrait = Portrait;
            HerProp.AnimatorController = AnimatorController;
            HerProp.level = level;
            HerProp.expToLevelUp = expToLevelUp;
            HerProp.curHp = curHp;
            HerProp.curMp = curMp;
            HerProp.curCr = curCr;

            return HerProp;
            //return new HeroProperties();
        }
    }

    public void GetExp(int exp)
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
                    GetExp(tmpExp);
                }
            }
            else
                return;
        }
    }
}

