using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {
    SpriteRenderer SprRenderer;

    void Start () {
        SprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        SprRenderer.sortingOrder = (int)(-transform.position.y * 2);
    }
}
