using UnityEngine;
using System.Collections;

public class ShadowArea : MonoBehaviour
//Затенение объекта попадающего в триггер на слое Shadows.
{
    SpriteRenderer Spr;
    Color shadowColor = new Color(0.4f, 0.4f, 0.4f);

    public void Start()
    {
        Spr = GetComponent<SpriteRenderer>();
        if (Spr == null)
            print("GameObject " + gameObject.name + " не имеет SpriteRenderer");
    }

    public void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.layer == 9)   //Shadows
        {
            Spr.color = shadowColor;
        }
    }

    public void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.layer == 9)   //Shadows
        {
            Spr.color = Color.white;
        }
    }
}
