using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuCharacter : MonoBehaviour
{
    public Text characterName;
    public Text level;
    public Text corruption;
    public Scrollbar corruptionBar;
    public Text health;
    public Scrollbar healthBar;
    public Text mana;
    public Scrollbar manaBar;


    public void UpdateCharacter(Hero hero)
    {
        if (hero != null)
        {
            characterName.text = hero.HeroPropetries.Name;
            //level.text = hero.HeroPropetries.Level.
            level.text = "NoN";
            corruption.text = hero.HeroPropetries.Cr + "/" + hero.HeroPropetries.Mcr;
            health.text = hero.HeroPropetries.Hp + "/" + hero.HeroPropetries.Mhp;
            mana.text = hero.HeroPropetries.Mp + "/" + hero.HeroPropetries.Mmp;
            corruptionBar.size = (float)hero.HeroPropetries.Cr / (float)hero.HeroPropetries.Mcr;
            healthBar.size = (float)hero.HeroPropetries.Hp / (float)hero.HeroPropetries.Mhp;
            manaBar.size = (float)hero.HeroPropetries.Mp / (float)hero.HeroPropetries.Mmp;
        }
    }


}
