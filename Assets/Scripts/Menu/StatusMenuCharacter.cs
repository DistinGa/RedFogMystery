using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusMenuCharacter : MonoBehaviour
{
    public Image portret;
    public Text characterName;
    public Text level;
    public Text xp;
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
            HeroProperties hp = hero.HeroPropetries;
            //portret = 
            characterName.text = hp.Name;
            level.text = hero.level.ToString();
            xp.text = hero.expToLevelUp.ToString();

            corruption.text = hp.curCr + "/" + hp.Cr;
            corruptionBar.size = (float)hp.curCr / (float)hp.Cr;

            health.text = hp.curHp + "/" + hp.Hp;
            healthBar.size = (float)hp.curHp / (float)hp.Hp;

            mana.text = hp.curMp + "/" + hp.Mp;
            manaBar.size = (float)hp.curMp / (float)hp.Mp;
        }
    }
}
