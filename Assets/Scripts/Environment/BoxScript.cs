using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour
{
    [SerializeField]
    Collider2D trigger;

    [Space(10)]
    [SerializeField]
    BoxItem[] ConsumableBoxContent;
    [SerializeField]
    BoxItem[] MaterialBoxContent;
    [SerializeField]
    BoxItem[] EquipmentBoxContent;
    [SerializeField]
    BoxItem[] KeyBoxContent;
    [SerializeField]
    double GoldAmount;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open");

        GameManager GM = GameManager.GM;
        if (collision.gameObject == GM.MainCharacter)
        {
            foreach (BoxItem item in ConsumableBoxContent)
            {
                GM.AddInventory(GM.AllConsumables.Get(item.ItemIndex), item.Count);
            }
            foreach (BoxItem item in MaterialBoxContent)
            {
                GM.AddInventory(GM.AllMaterials.Get(item.ItemIndex), item.Count);
            }
            foreach (BoxItem item in EquipmentBoxContent)
            {
                GM.AddInventory(GM.AllEquipments.Get(item.ItemIndex), item.Count);
            }
            foreach (BoxItem item in KeyBoxContent)
            {
                GM.AddInventory(GM.AllKeys.Get(item.ItemIndex), item.Count);
            }

            GM.AddGold(GoldAmount);
        }

        trigger.enabled = false;
    }

    [System.Serializable]
    class BoxItem
    {
        public int ItemIndex;
        public int Count;
    }
}
