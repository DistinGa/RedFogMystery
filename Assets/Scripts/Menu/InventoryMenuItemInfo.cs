using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryMenuItemInfo : MonoBehaviour
{

    public Text itemName;
    public Text amount;
    public Image mark;

    public void ChangeInfo(string _name, int _amount, bool _mark)
    {
        // учесть максимальную длину строк!?
        itemName.text = _name;
        amount.text = _amount.ToString();
        mark.enabled = _mark;
    }
    public void TurnOnMark(bool turn)
    {
        mark.enabled = turn;
    }
}
