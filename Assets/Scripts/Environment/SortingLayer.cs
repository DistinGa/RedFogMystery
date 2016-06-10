using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {
    //Скрипт управляет сортировкой спрайтов внутри одного слоя
    //Скрипт для стационарных объектов на карте
    SpriteRenderer SprRenderer;

    void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 2);
    }
}
