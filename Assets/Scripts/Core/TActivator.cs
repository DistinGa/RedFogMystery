using UnityEngine;

public class TActivator : MonoBehaviour
{
    [SerializeField]
    CSEvent Script;
    [SerializeField]
    bool oneShot;   //Если true, объект активатора отключается после использования.
    [SerializeField]
    bool useButton; //Действие выполняется по нажатию кнопки "Use"
    bool checkButton = false;   //признак того, что триггер сработал и нужно теперь проверять нажатие кнопки

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "MainHero") //проверка на персонажа
        {
            if (Script == null)
            {
                Debug.LogError("Не назначен скрипт CSEvent", this);
                return;
            }

            if (useButton)
                checkButton = true;
            else
                doAction();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "MainHero") //проверка на персонажа
        {
            if (useButton)
                checkButton = false;
        }
    }

    public void Start()
    {
        //В режиме редактора на активаторе отображается спрайт, для того чтобы его было видно. В игре этот спрайт не должен быть виден.
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        if (spr != null)
            spr.enabled = false;
    }

    void Update()
    {
        if (checkButton)
        {
            if (Input.GetAxisRaw("Submit") > 0)
            {
                doAction();
            }
        }
    }

    void doAction()
    {
        Script.OnEventAction();
        if (oneShot)
            gameObject.SetActive(false);
    }
}
