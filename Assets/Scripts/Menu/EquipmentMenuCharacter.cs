using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentMenuCharacter : MonoBehaviour
{
    //public Image portret;
    public Text weapon;
    public Text head;
    public Text body;
    public Text accessory1;
    public Text accessory2;

    public void UpdateCharacter(Hero hero)
    {
        //portret = hero.HeroPropetries.Portrait;
        if (hero.Weapon.index != 0)
            weapon.text = hero.Weapon.Name;
        else
            weapon.text = "Empty";

        if (hero.Helmet.index != 0)
            head.text = hero.Helmet.Name;
        else
            head.text = "Empty";

        if (hero.Armor.index != 0)
            body.text = hero.Armor.Name;
        else
            body.text = "Empty";

        if (hero.Accessory1.index != 0)
            accessory1.text = hero.Accessory1.Name;
        else
            accessory1.text = "Empty";

        if (hero.Accessory2.index != 0)
            accessory2.text = hero.Accessory2.Name;
        else
            accessory2.text = "Empty";
    }

}
