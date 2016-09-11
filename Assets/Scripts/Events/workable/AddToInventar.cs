using UnityEngine;

public class AddToInventar: CSEvent
{
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

    public override void OnEventAction()
    {
        GameManager GM = GameManager.GM;
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

    [System.Serializable]
    class BoxItem
    {
        public int ItemIndex = 0;
        public int Count = 0;
    }
}
