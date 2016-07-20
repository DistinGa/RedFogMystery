using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum InventoryCategory
{
    Items,
    Equipment,
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

    public InventoryCategory currentCategory;

    private int markPosition;

    public void Start()
    {
        if (UI_Inventory == null)
            UI_Inventory = new List<GameObject>();
        currentCategory = InventoryCategory.Items;
        markPosition = 0;
    }

    public void UpdateMenu()
    {
        List<Hero> party = GameManager.GM.PartyContent();
        List<InventoryItem<ConsumableProperties>> currentItems = GameManager.GM.Consumables;

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
        if (currentItems.Count > 0)
        {
            // ? стрёмный кусок гавнокода
            for (int i = UI_Inventory.Count - 1; i >= 0; i--)
                Destroy(UI_Inventory[i]);

            UI_Inventory.Clear();

            for (int i = 0; i < currentItems.Count; i++)
            {
                GameObject itemPref = Instantiate(UI_ItemPrefab);
                itemPref.transform.SetParent(UI_ItemGrid.transform);
                UI_Inventory.Add(itemPref);
            }

            for (int i = 0; i < UI_Inventory.Count; i++)
            {
                Debug.Log(currentItems[i].Item.Name);
                //ConsumableProperties cp = GameManager.GM.AllConsumables.Consumables[currentItems[i]];
                //UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(cp.Name, 1, true);
                if (i == markPosition)
                {
                    UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(
                        currentItems[i].Item.Name, currentItems[i].Count, true);
                    UI_Description.text = currentItems[i].Item.Description.ToString();
                }
                else
                    UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(
                        currentItems[i].Item.Name, currentItems[i].Count);
            }
        }
    }
}
