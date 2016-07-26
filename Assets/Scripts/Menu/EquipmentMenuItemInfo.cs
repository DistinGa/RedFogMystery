using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentMenuItemInfo : MonoBehaviour
{

    public Text itemName;
    public Image mark;
    public int prefabIndex;

    public void ChangeInfo(string _name, bool _mark, int _index)
    {
        // учесть максимальную длину строк!?
        itemName.text = _name;
        mark.enabled = _mark;
        prefabIndex = _index;
    }
    // ???
    public void TurnOnMark(bool turn)
    {
        mark.enabled = turn;
    }
}
