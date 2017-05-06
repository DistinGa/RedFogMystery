using UnityEngine;

public class TActivator : MonoBehaviour
{
    [SerializeField]
    CSEvent UseActionScript;
    [SerializeField]
    CSEvent Script, ExitScript;
    [SerializeField]
    bool oneShot;   //Если true, объект активатора отключается после использования.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(UseActionScript != null)
            UseActionScript.enabled = true;

        if (other.gameObject.name == "MainHero") //проверка на персонажа
        {
            if (Script == null)
            {
                //Debug.LogError("Не назначен скрипт CSEvent", this);
                return;
            }

            Script.OnEventAction();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (UseActionScript != null)
            UseActionScript.enabled = false;

        if (other.gameObject.name == "MainHero") //проверка на персонажа
        {
            if (ExitScript == null)
                return;

            ExitScript.OnEventAction();

            if (oneShot)
                gameObject.SetActive(false);
        }
    }

    public void Start()
    {
        //В режиме редактора на активаторе отображается спрайт, для того чтобы его было видно. В игре этот спрайт не должен быть виден.
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        if (spr != null)
            spr.enabled = false;
    }
}
