using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryGridLayout : MonoBehaviour
{
    public int row;
    void Start()
    {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width / 2, parent.rect.height / row);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
