using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public GameObject UI_ItemGrid;
    public GameObject UI_ItemPrefab;

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
        List<int> currentItems = GameManager.GM.consumables;

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
                if (i == markPosition)
                {
                    ConsumableProperties cp = GameManager.GM.AllConsumables.Consumables[currentItems[i]];
                    UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(cp.Name, 1, true);
                }
                else
                {
                    ConsumableProperties cp = GameManager.GM.AllConsumables.Consumables[currentItems[i]];
                    UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(cp.Name, 1);
                }
            }
        }



        //// Inventory List Update
        //if (currentItems.Count > 0)
        //{
        //    for (int i = 0; i < UI_Inventory.Count; i++)
        //    {
        //        if (i < currentItems.Count)
        //        {
        //            if (i == markPosition)
        //            {
        //                ConsumableProperties cp = GameManager.GM.AllConsumables.Consumables[currentItems[i]];
        //                UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(cp.Name, 1, true);
        //            }
        //            else
        //            {
        //                ConsumableProperties cp = GameManager.GM.AllConsumables.Consumables[currentItems[i]];
        //                UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().ChangeInfo(cp.Name, 1);
        //            }
        //        }
        //        else
        //        {
        //            UI_Inventory[i].GetComponent<InventoryMenuItemInfo>().Empty();
        //        }
        //    }

        //}
    }
}
