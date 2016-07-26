using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum InventoryCategory
{
    Consumables,
    Equipments,
    Materials,
    Keys
}

public class InventoryMenuUpdater : MonoBehaviour
{
    public GameObject[] UI_Characters;
    public GameObject UI_ItemGrid;
    public GameObject UI_ItemPrefab;
    public Text UI_Description;

    public InventoryCategory currentItemCategory;

    private int markPosition;
    private int Counter;

    public void Start()
    {
        currentItemCategory = InventoryCategory.Consumables;
        markPosition = 0;
    }

    public void UpdateMenu()
    {
        UI_Description.text = " "; // очистка описания в случае пустого инвентаря
        List<Hero> party = GameManager.GM.PartyContent();

        // Characters Info Update
        if (party.Count > 0)
        {
            for (int i = 0; i < UI_Characters.Length; i++)
            {
                if (i < party.Count)
                {
                    UI_Characters[i].SetActive(true);
                    UI_Characters[i].GetComponent<InventoryMenuCharacter>().UpdateCharacter(party[i]);
                }
                else
                    UI_Characters[i].SetActive(false);
            }
        }

        // Inventory List Update
        ClearInventoryPrefabs();

        switch (currentItemCategory)
        {
            case InventoryCategory.Consumables:
                ConsumableInventoryUpdate();
                break;
            case InventoryCategory.Equipments:
                EquipmentInventoryUpdate();
                break;
            case InventoryCategory.Materials:
                MaterialInventoryUpdate();
                break;
            case InventoryCategory.Keys:
                KeyInventoryUpdate();
                break;
        }
    }

    // удаление всех префабов предметов
    public void ClearInventoryPrefabs()
    {
        foreach (Transform child in UI_ItemGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void ConsumableInventoryUpdate()
    {
        Counter = 0;

        GameObject itemPref;
        foreach (var item in GameManager.GM.Consumables)
        {
            itemPref = Instantiate(UI_ItemPrefab);
            itemPref.SetActive(true);
            itemPref.transform.SetParent(UI_ItemGrid.transform);
            itemPref.transform.localScale = new Vector2(1, 1);

            itemPref.GetComponent<InventoryMenuItemInfo>().ChangeInfo(
                item.Item.Name, item.Count, Counter == markPosition ? true : false, Counter);
            if (Counter == markPosition)
                UI_Description.text = item.Item.Description;
            Counter++;
        }
    }
    void EquipmentInventoryUpdate()
    {
        Counter = 0;

        GameObject itemPref;
        foreach (var item in GameManager.GM.Equipments)
        {
            itemPref = Instantiate(UI_ItemPrefab);
            itemPref.transform.SetParent(UI_ItemGrid.transform);
            itemPref.transform.localScale = new Vector2(1, 1);

            itemPref.GetComponent<InventoryMenuItemInfo>().ChangeInfo(
                item.Item.Name, item.Count, Counter == markPosition ? true : false, Counter);
            if (Counter == markPosition)
                UI_Description.text = item.Item.Description;
            Counter++;
        }
    }
    void MaterialInventoryUpdate()
    {
        Counter = 0;

        GameObject itemPref;
        foreach (var item in GameManager.GM.Materials)
        {
            itemPref = Instantiate(UI_ItemPrefab);
            itemPref.transform.SetParent(UI_ItemGrid.transform);
            itemPref.transform.localScale = new Vector2(1, 1);

            itemPref.GetComponent<InventoryMenuItemInfo>().ChangeInfo(
                item.Item.Name, item.Count, Counter == markPosition ? true : false, Counter);
            if (Counter == markPosition)
                UI_Description.text = item.Item.Description;
            Counter++;
        }
    }
    void KeyInventoryUpdate()
    {
        Counter = 0;

        GameObject itemPref;
        foreach (var item in GameManager.GM.Keys)
        {
            itemPref = Instantiate(UI_ItemPrefab);
            itemPref.transform.SetParent(UI_ItemGrid.transform);
            itemPref.transform.localScale = new Vector2(1, 1);

            itemPref.GetComponent<InventoryMenuItemInfo>().ChangeInfo(
                item.Item.Name, item.Count, Counter == markPosition ? true : false, Counter);
            if (Counter == markPosition)
                UI_Description.text = item.Item.Description;
            Counter++;
        }
    }

    public void ChangeItemTypeConsumables()
    {
        currentItemCategory = InventoryCategory.Consumables;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeItemTypeEquipments()
    {
        currentItemCategory = InventoryCategory.Equipments;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeItemTypeMaterials()
    {
        currentItemCategory = InventoryCategory.Materials;
        markPosition = 0;
        UpdateMenu();
    }
    public void ChangeItemTypeKeys()
    {
        currentItemCategory = InventoryCategory.Keys;
        markPosition = 0;
        UpdateMenu();
    }

    public void ChangeMarkPosition(InventoryMenuItemInfo prefabScript)
    {
        if (markPosition == prefabScript.prefabIndex && currentItemCategory == InventoryCategory.Consumables)
        {
            UseItem(prefabScript.itemName.text);
        }
        else
        {
            markPosition = prefabScript.prefabIndex;
        }
        UpdateMenu();

    }

    public void UseItem(string itemName)
    {
        foreach (var item in GameManager.GM.Consumables)
        {
            if (itemName == item.Item.Name)
            {
                item.Count--;
                GameManager.GM.PartyContent()[0].curHp += item.Item.Hp;
                GameManager.GM.PartyContent()[0].curMp += item.Item.Mp;
            }
        }
    }
}
