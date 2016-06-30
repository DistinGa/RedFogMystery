using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
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
            //level.text = Hero.Level.ToString();
            level.text = "NoN";
            corruption.text = hero.HeroPropetries.curCr + "/" + hero.HeroPropetries.Cr;
            health.text = hero.HeroPropetries.curHp + "/" + hero.HeroPropetries.Hp;
            mana.text = hero.HeroPropetries.curMp + "/" + hero.HeroPropetries.Mp;
            corruptionBar.size = (float)hero.HeroPropetries.curCr / (float)hero.HeroPropetries.Cr;
            healthBar.size = (float)hero.HeroPropetries.curHp / (float)hero.HeroPropetries.Hp;
            manaBar.size = (float)hero.HeroPropetries.curMp / (float)hero.HeroPropetries.Mp;
        }
    }


}
