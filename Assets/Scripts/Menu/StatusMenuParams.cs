using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusMenuParams : MonoBehaviour
{

    public Text health;
    public Text mana;
    public Text attack;
    public Text defence;
    public Text magAttack;
    public Text magDefence;
    public Text agility;

    public void UpdateCharacter(Hero hero)
    {
        if (hero != null)
        {
            HeroProperties hp = hero.HeroPropetries;

            health.text = hp.Hp.ToString();
            mana.text = hp.Mp.ToString();
            attack.text = hp.Atk.ToString();
            defence.text = hp.Def.ToString();
            magAttack.text = hp.Mat.ToString();
            magDefence.text = hp.Mdf.ToString();
            agility.text = hp.Agi.ToString();
        }
    }
}
