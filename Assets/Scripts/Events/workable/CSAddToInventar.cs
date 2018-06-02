using UnityEngine;

[AddComponentMenu("Cut Scenes/Добавление в инвентарь")]
public class CSAddToInventar: CSEvent
{
    [SerializeField]
    BoxItem[] ConsumableContent;
    [SerializeField]
    BoxItem[] MaterialContent;
    [SerializeField]
    BoxItem[] EquipmentContent;
    [SerializeField]
    BoxItem[] KeyContent;
    [SerializeField]
    double GoldAmount;

    public override void OnEventAction()
    {
        GameManager GM = GameManager.GM;
        foreach (BoxItem item in ConsumableContent)
        {
            GM.AddInventory(GM.AllConsumables.Get(item.ItemIndex), item.Count);
        }
        foreach (BoxItem item in MaterialContent)
        {
            GM.AddInventory(GM.AllMaterials.Get(item.ItemIndex), item.Count);
        }
        foreach (BoxItem item in EquipmentContent)
        {
            GM.AddInventory(GM.AllEquipments.Get(item.ItemIndex), item.Count);
        }
        foreach (BoxItem item in KeyContent)
        {
            GM.AddInventory(GM.AllKeys.Get(item.ItemIndex), item.Count);
        }

        GM.AddGold(GoldAmount);

        if (NextStep != null)
            NextStep();
    }

    [System.Serializable]
    class BoxItem
    {
        public int ItemIndex = 0;
        public int Count = 0;
    }
}
