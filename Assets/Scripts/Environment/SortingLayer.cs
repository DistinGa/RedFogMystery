using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {
    public bool Dyn;
    SpriteRenderer SprRenderer;

    void Start () {
        SprRenderer = GetComponent<SpriteRenderer>();
        SprRenderer.sortingOrder = (int)(-transform.position.y * 2);
    }

    // Update is called once per frame
    void Update () {
        if(Dyn)
            SprRenderer.sortingOrder = (int)(-transform.position.y * 2);
    }
}
