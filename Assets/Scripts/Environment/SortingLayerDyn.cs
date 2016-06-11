using UnityEngine;
using System.Collections;

public class SortingLayerDyn : MonoBehaviour {
    //Скрипт управляет сортировкой спрайтов внутри одного слоя
    //Скрипт для объектов перемещающихся по карте
    SpriteRenderer SprRenderer;

    void Start()
    {
        SprRenderer = GetComponent<SpriteRenderer>();
        SprRenderer.sortingOrder = (int)(-transform.position.y * 2);
    }

    // Update is called once per frame
    void Update()
    {
            SprRenderer.sortingOrder = (int)(-transform.position.y * 2);
    }
}
