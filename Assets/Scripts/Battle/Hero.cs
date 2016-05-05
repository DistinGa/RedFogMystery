using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable] public class HeroPropetries
{
  public string Name = "";
  public Sprite Portrait = null;
  public float Hp = 0;
  public float Mhp = 0;
  public float Mp = 0;
  public float Mmp = 0;
  public float Cr = 0;
  public float Mcr = 0;
  public float Atk = 0;
  public float Def = 0;
  public float Mat = 0;
  public float Mdf = 0;
  public float Agi = 0;
  [HideInInspector] public List<string> Armors = new List<string>();
}

public enum ThingType
{
  Thing,
  Armor,
  Material,
  Key
}

[Serializable] public class ThingPropetries
{
  public string Name = "Name";
  public Sprite Portrait = null;
  public ThingType Type = ThingType.Thing;
  public string Description = "Description";
  public int Count = 1;
  public float Hp = 0;
  public float Mhp = 0;
  public float Mp = 0;
  public float Mmp = 0;
  public float Cr = 0;
  public float Mcr = 0;
  public float Atk = 0;
  public float Def = 0;
  public float Mat = 0;
  public float Mdf = 0;
  public float Agi = 0;
}

public class Hero : MonoBehaviour
{
  [SerializeField] private HeroPropetries heroPropetries = null;
  [HideInInspector] public HeroUI HeroUi = null;

  public HeroPropetries HeroPropetries
  {
    get { return heroPropetries;}
    set
    {
      heroPropetries = value;
      if (HeroUi != null)
        HeroUi.UpdateUI();      
    }
  }

  public void ConnectToPartyGui()
  {
    HeroesPanel heroesPanel = FindObjectOfType<HeroesPanel>();
    if (heroesPanel != null)
      HeroUi = FindObjectOfType<HeroesPanel>().AttachHero(this);
  }

  public void AddPower(HeroPropetries hps)
  {
    heroPropetries.Hp += hps.Hp;
    heroPropetries.Mhp += hps.Mhp;
    heroPropetries.Mp += hps.Mp;
    heroPropetries.Mmp += hps.Mmp;
    heroPropetries.Cr += hps.Cr;
    heroPropetries.Mcr += hps.Mcr;
    heroPropetries.Atk += hps.Atk;
    heroPropetries.Def += hps.Def;
    heroPropetries.Mat += hps.Mat;
    heroPropetries.Mdf += hps.Mdf;
    heroPropetries.Agi += hps.Agi;
    if (hps.Armors.Count > 0)
      heroPropetries.Armors.Add(hps.Armors[0]);    
    
    if (HeroUi != null)
      HeroUi.UpdateUI();
  }
}
