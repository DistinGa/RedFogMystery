using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    float gameTime = 0;
    public int Gold;

    public GameObject MainCharacter;    //ГГ
    public List<GameObject> Vagons;     //список GO для отображения "паровозика"
    public GameObject VagonPrefab;      //префаб "вагона"
    public Hero[] Heroes = new Hero[4];
    public Hero _Leader = null;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //singletone
        if (GM == null)
            GM = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Start()
    {
        if (MainCharacter == null)
            MainCharacter = FindObjectOfType<CharacterMoving>().gameObject;

        if (Leader.HeroPropetries.Name == "")
            Leader = FindHeroByName("Gehend");
    }

    public Hero Leader
    {
        get { return _Leader; }
        set
        {
            _Leader = value;

            int i = 0;
            foreach (Hero curHero in PartyContent())
            {
                if (curHero == value)
                //Лидер
                {
                    MainCharacter.GetComponent<Animator>().runtimeAnimatorController = curHero.AnimatorController;
                }
                else
                    //все остальные
                    Vagons[i++].GetComponent<Animator>().runtimeAnimatorController = curHero.AnimatorController;
            }
        }
    }

    public float GameTime
    {
        get { return gameTime; }
    }

    public void AddGameTime(float dt)
    {
        gameTime += dt;
    }

    public string GetGameTimeStr()
    {
        int hours = (int)gameTime / 3600;
        int minutes = (int)((gameTime - hours * 3600) / 60);
        string minutesText = minutes.ToString();
        if (minutes < 10)
            minutesText = "0" + minutesText;

        return hours.ToString() + ":" + minutesText;
    }

    public void ConnectToParty(string heroName)
    {
        Hero newHero = FindHeroByName(heroName);
        if (newHero == null)
        {
            Debug.LogWarning("Не найден герой по имени " + heroName);
            return;
        }
        ConnectToParty(newHero);
    }

    public void ConnectToParty(Hero newHero)
    {
        GameObject target;
        if (Vagons.Count > 0)
            target = Vagons[Vagons.Count - 1];
        else
            target = MainCharacter;

        //Присоединяем, если героя ещё нет в партии
        if (!newHero.isActive)
        {
            newHero.isActive = true;
            GameObject tempGO = (GameObject)Instantiate(VagonPrefab, target.transform.position, Quaternion.identity);
            tempGO.GetComponent<Animator>().runtimeAnimatorController = newHero.AnimatorController;
            tempGO.GetComponent<Party>().FollowTo = target;
            Vagons.Add(tempGO);
        }
    }

    public Hero FindHeroByName(string heroName)
    {
        Hero h = null;

        foreach (var item in Heroes)
        {
            if (item.HeroPropetries.Name == heroName)
            {
                h = item;
                break;
            }
        }

        return h;
    }

    //Возвращает список героев входящих в партию
    public List<Hero> PartyContent()
    {
        List<Hero> pc = new List<Hero>();

        foreach (var Hero in Heroes)
        {
            if (Hero.isActive)
                pc.Add(Hero);
        }

        return pc;
    }

    //Вкл/выкл "ветренных" анимаций у партии
    public void SetPartyWind(bool sw)
    {
        MainCharacter.GetComponent<CharacterMoving>().SetWind(sw);
        foreach (var item in Vagons)
        {
            if(item != null)
                item.SendMessage("SetWind", sw);
        }
    }

    public void Load()
    {

    }

    public void Save()
    {

    }
}
