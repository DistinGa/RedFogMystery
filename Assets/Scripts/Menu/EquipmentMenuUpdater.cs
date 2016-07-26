using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum EquipmenPosition
{
    Weapon,
    Head,
    Body,
    Accessory1,
    Accessory2
}

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
    public EquipmenPosition currentEquipmentPosition;

    // текущий выбранный предмет
    private int markPosition;
    private int Counter;

    public void Start()
    {

        currentHero = GameManager.GM.PartyContent()[0];
        currentEquipmentPosition = EquipmenPosition.Weapon;
        markPosition = 0;
        currentHero.Weapon = (EquipmentProperties)GameManager.GM.AllEquipments.Get(8); // временный пункт
        currentHero.Armor = (EquipmentProperties)GameManager.GM.AllEquipments.Get(1); // временный пункт
    }

    public void UpdateMenu()
    {
        UI_Description.text = " "; // очистка описания в случае пустого инвентаря

        UI_CharacterEquip.GetComponent<EquipmentMenuCharacter>().UpdateCharacter(currentHero);

        ClearEquipedItemPrefabs();
        GameObject itemPref;

        EquipmentProperties ep = currentHero.Weapon;
        switch (currentEquipmentPosition)
        {
            case EquipmenPosition.Head:
                ep = currentHero.Helmet;
                break;
            case EquipmenPosition.Body:
                ep = currentHero.Armor;
                break;
            case EquipmenPosition.Accessory1:
                ep = currentHero.Accessory1;
                break;
            case EquipmenPosition.Accessory2:
                ep = currentHero.Accessory2;
                break;
        }

        foreach (var item in GameManager.GM.Equipments)
        {
            if ((int)item.Item.equipType == (int)currentEquipmentPosition ||
                item.Item.equipType == EquipmentType.Accessory && currentEquipmentPosition == EquipmenPosition.Accessory2)
            {
                if (item.Item.index != 0)
                {
                    itemPref = Instantiate(UI_EquipmentPrefab);
                    itemPref.SetActive(true);
                    itemPref.transform.SetParent(UI_EquipmentGrid.transform);
                    itemPref.transform.localScale = new Vector2(1, 1);

                    itemPref.GetComponent<EquipmentMenuItemInfo>().ChangeInfo(
                        item.Item.Name, Counter == markPosition ? true : false, Counter);
                    if (Counter == markPosition)
                    {
                        UI_Description.text = item.Item.Description;

                        UI_Stats.UpdateParams(
                            currentHero.HeroPropetries,
                            currentHero.PredictProperties(item.Item, ep.index == 0 ? null : ep));
                    }
                    Counter++;
                }
            }
        }
    }

    public void ClearEquipedItemPrefabs()
    {
        foreach (Transform child in UI_EquipmentGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnItemKlick(EquipmentMenuItemInfo prefabScript)
    {
        if (markPosition == prefabScript.prefabIndex)
        {
            EquipItem(prefabScript.itemName.text);
            Counter = 0;
            markPosition = 0;
        }
        else
        {
            markPosition = prefabScript.prefabIndex;
            Counter = 0;
        }
        UpdateMenu();
    }
    public void EquipItem(string itemName)
    {
        foreach (var item in GameManager.GM.Equipments)
        {
            if (item.Item.Name.Equals(itemName))
            {
                EquipmentProperties equipment = currentHero.Weapon;
                switch (currentEquipmentPosition)
                {
                    case EquipmenPosition.Weapon:
                        currentHero.Weapon = item.Item;
                        break;
                    case EquipmenPosition.Head:
                        equipment = currentHero.Helmet;
                        currentHero.Helmet = item.Item;
                        break;
                    case EquipmenPosition.Body:
                        equipment = currentHero.Armor;
                        currentHero.Armor = item.Item;
                        break;
                    case EquipmenPosition.Accessory1:
                        equipment = currentHero.Accessory1;
                        currentHero.Accessory1 = item.Item;
                        break;
                    case EquipmenPosition.Accessory2:
                        equipment = currentHero.Accessory2;
                        currentHero.Accessory2 = item.Item;
                        break;
                }
                GameManager.GM.Equipments.Remove(item);
                if (equipment.index != 0)
                    GameManager.GM.AddInventory(equipment);
                return;
            }
        }
    }
    public void RemoveAll()
    {
        EquipmentProperties ep = GameManager.GM.AllEquipments.Equipments[0];
        currentHero.Weapon = ep;
        currentHero.Helmet = ep;
        currentHero.Armor = ep;
        currentHero.Accessory1 = ep;
        currentHero.Accessory2 = ep;
        UpdateMenu();
        markPosition = 0;
        Counter = 0;
    }

    public void ChangeEquipmentTypeWeapon()
    {
        currentEquipmentPosition = EquipmenPosition.Weapon;
        markPosition = 0;
        Counter = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeHead()
    {
        currentEquipmentPosition = EquipmenPosition.Head;
        markPosition = 0;
        Counter = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeBody()
    {
        currentEquipmentPosition = EquipmenPosition.Body;
        markPosition = 0;
        Counter = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeAccessory1()
    {
        currentEquipmentPosition = EquipmenPosition.Accessory1;
        markPosition = 0;
        Counter = 0;
        UpdateMenu();
    }
    public void ChangeEquipmentTypeAccessory2()
    {
        currentEquipmentPosition = EquipmenPosition.Accessory2;
        markPosition = 0;
        Counter = 0;
        UpdateMenu();
    }
}