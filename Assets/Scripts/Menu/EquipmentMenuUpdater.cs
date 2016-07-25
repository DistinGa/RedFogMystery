using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EquipmentMenuUpdater : MonoBehaviour
{
    public Hero currentHero;
    // Экипировка
    public GameObject UI_CharacterEquip;
    public EquipmentMenuStatsInfo UI_Stats;

    // для отображения вещей и их описания
    public GameObject UI_EquipmentGrid;
    public GameObject UI_EquipmentPrefab;
    public Text UI_Description;

    // какой слот экипировки выбран
    public EquipmentType currentEquipmentType;

    // текущий выбранный предмет
    private int markPosition;
    private int Counter;

    public void Start()
    {
        currentHero = GameManager.GM.PartyContent()[0];
        currentEquipmentType = EquipmentType.Weapon;
        markPosition = 0;
    }

    public void UpdateMenu()
    {
        UI_Description.text = " "; // очистка описания в случае пустого инвентаря

        currentHero.Weapon = (EquipmentProperties)GameManager.GM.AllEquipments.Get(8); // временный пункт
        currentHero.Armor = (EquipmentProperties)GameManager.GM.AllEquipments.Get(1); // временный пункт

        UI_CharacterEquip.GetComponent<EquipmentMenuCharacter>().UpdateCharacter(currentHero);

        UI_Stats.UpdateParams(currentHero.HeroPropetries, currentHero.HeroPropetries);
        //currentHero.PredictProperties
    }

    public void ChangeEquipmentTypeWeapon()
    {
        currentEquipmentType = EquipmentType.Weapon;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeHead()
    {
        currentEquipmentType = EquipmentType.Helmet;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeArmor()
    {
        currentEquipmentType = EquipmentType.Armor;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeAccessory1()
    {
        currentEquipmentType = EquipmentType.Accessory;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeAccessory2()
    {
        currentEquipmentType = EquipmentType.Accessory;
        markPosition = 0;
        UpdateMenu();
    }
}