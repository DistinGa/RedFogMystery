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
    public GameObject[] UI_TogglesGroup;
    public GameObject UI_ItemGrid;
    public GameObject UI_ItemPrefab;
    public Text UI_Description;

    [HideInInspector]
    public List<GameObject> UI_Inventory;

    public InventoryCategory currentItemCategory;

    private int markPosition;
    private int markCounter;

    public void Start()
    {
        if (UI_Inventory == null)
            UI_Inventory = new List<GameObject>();
        currentItemCategory = InventoryCategory.Consumables;
        markPosition = 0;
    }

    public void UpdateMenu()
    {
        List<Hero> party = GameManager.GM.PartyContent();
        List<InventoryItem<ConsumableProperties>> currentItems = GameManager.GM.Consumables;
        markCounter = 0;

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
        GameObject itemPref;
        foreach (var item in currentItems)
        {
            itemPref = Instantiate(UI_ItemPrefab);
            itemPref.transform.SetParent(UI_ItemGrid.transform);
            itemPref.transform.localScale = new Vector2(1, 1);

            itemPref.GetComponent<InventoryMenuItemInfo>().ChangeInfo(item.Item.Name, item.Count, markCounter == markPosition ? true : false);
            if (markCounter == markPosition)
                UI_Description.text = item.Item.Description;
            markCounter++;
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

    public void ChangeItemTypeConsumables()
    {
            currentItemCategory = InventoryCategory.Consumables;
    }
    public void ChangeItemTypeEquipments()
    {
            currentItemCategory = InventoryCategory.Equipments;
    }
    public void ChangeItemTypeMaterials()
    {
        currentItemCategory = InventoryCategory.Materials;
    }
    public void ChangeItemTypeKeys()
    {
        currentItemCategory = InventoryCategory.Keys;
    }
}
