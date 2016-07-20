using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryMenuCharacter : MonoBehaviour
{
    public Image portret;
    public Text characterName;
    public Image healthBar;
    public Text health;
    public Image manaBar;
    public Text mana;

    public void UpdateCharacter(Hero hero)
    {
        if (hero != null)
        {
            HeroProperties hp = hero.HeroPropetries;
            //portret = 
            characterName.text = hp.Name;

            health.text = hp.curHp + "/" + hp.Hp;
            healthBar.fillAmount = (float)hp.curHp / (float)hp.Hp;

            mana.text = hp.curMp + "/" + hp.Mp;
            manaBar.fillAmount = (float)hp.curMp / (float)hp.Mp;
        }
    }
}
