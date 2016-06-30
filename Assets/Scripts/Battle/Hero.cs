using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable] public class HeroProperties : Properties
{
  public RuntimeAnimatorController AnimatorController = null;
  public float curHp = 0;   //текущие очки здоровья
  public float curMp = 0;   //текущие очки маны
  public float curCr = 0;   //текущие очки коррупции
}


[System.Serializable]
public class Hero
{
    public bool isActive;
    public int level;
    public int exp;

    public HeroProperties HeroPropetries
    {
        get { return new HeroProperties(); }
    }
}

