using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryMenuItemInfo : MonoBehaviour
{
    // добавить место под индексы
    public Text itemName;
    public Text amount;
    public Image mark;
    public int prefabIndex;

    public void ChangeInfo(string _name, int _amount, bool _mark, int _index)
    {
        // учесть максимальную длину строк!?
        itemName.text = _name;
        amount.text = _amount.ToString();
        mark.enabled = _mark;
        prefabIndex = _index;
    }
    public void TurnOnMark(bool turn)
    {
        mark.enabled = turn;
    }
}
