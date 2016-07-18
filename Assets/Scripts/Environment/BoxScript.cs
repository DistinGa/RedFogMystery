using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour
{
    [SerializeField]
    Collider2D trigger;

    [Space(10)]
    [SerializeField]
    int[] ConsumableBoxContent;
    [SerializeField]
    int[] MaterialBoxContent;
    [SerializeField]
    int[] EquipmentBoxContent;
    [SerializeField]
    int[] KeyBoxContent;

    // Use this for initialization
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open");

        GameManager GM = GameManager.GM;
        if (collision.gameObject == GM.MainCharacter)
        {
            foreach (int item in ConsumableBoxContent)
            {
                GM.AddInventory(GM.AllConsumables.Get(item));
            }
            foreach (int item in MaterialBoxContent)
            {
                GM.AddInventory(GM.AllMaterials.Get(item));
            }
            foreach (int item in EquipmentBoxContent)
            {
                GM.AddInventory(GM.AllEquipments.Get(item));
            }
            foreach (int item in KeyBoxContent)
            {
                GM.AddInventory(GM.AllKeys.Get(item));
            }
        }

        trigger.enabled = false;
    }
}
