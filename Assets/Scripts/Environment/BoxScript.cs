using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour, ISave
{
    [SerializeField]
    int objectID;
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

    public void Awake()
    {
        if (objectID == 0)
            Debug.LogError("Объекту не назначен идентификатор.", gameObject);

        SaveManager.Subscribe(this);
    }

    public void OnDestroy()
    {
        SaveManager.Unsubscribe(this);
    }

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

    public SavedData GetDataToSave()
    {
        return new SavedData(GetObjID(), JsonUtility.ToJson(new SerializableData(trigger.enabled)));
    }

    public void SetSavedData(string strJSON)
    {
        SerializableData gotData = JsonUtility.FromJson<SerializableData>(strJSON);

        gameObject.SetActive(gotData.isActive);
    }

    public int GetObjID()
    {
        if (objectID == 0)
            Debug.LogError("Объекту не назначен идентификатор.", gameObject);

        return objectID;
    }

    [System.Serializable]
    class SerializableData
    {
        public bool isActive;

        public SerializableData(bool b)
        {
            isActive = b;
        }
    }

    [System.Serializable]
    class BoxItem
    {
        public int ItemIndex = 0;
        public int Count = 0;
    }
}
