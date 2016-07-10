using UnityEngine;

public class TActivator : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour Script;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "MainHero") //проверка на персонажа
            Script.SendMessage("OnEventAction");
    }

    public void Start()
    {
        //В режиме редактора на активаторе отображается спрайт, для того чтобы его было видно. В игре этот спрайт не должен быть виден.
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        if (spr != null)
            spr.enabled = false;
    }
}
