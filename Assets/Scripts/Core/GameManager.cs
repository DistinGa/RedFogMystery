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
    public List<PartyMember> Party;
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

    public Hero Leader
    {
        get { return _Leader; }
        set
        {
            _Leader = value;
            
            for (int i = 0; i < Party.Count; i++)
            {
                PartyMember curHero = Party[i];
                if (curHero.Hero == value)
                    //Лидер
                    MainCharacter.GetComponent<Animator>().runtimeAnimatorController = curHero.Hero.AnimatorController;
                else
                    //все остальные
                    curHero.GO.GetComponent<Animator>().runtimeAnimatorController = curHero.Hero.AnimatorController;
            }
        }
    }

    public float GameTime
    {
        get {return gameTime;}
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
        //Добавление в интерфейс
        //....

        Hero newHero = FindHeroByName(heroName);
        if (newHero == null)
        {
            Debug.LogWarning("Не найден герой по имени " + heroName);
            return;
        }

        GameObject tempGO = (GameObject)Instantiate(VagonPrefab, Vagons[Vagons.Count - 1].transform.position, Quaternion.identity);
        tempGO.GetComponent<Animator>().runtimeAnimatorController = newHero.AnimatorController;
        Vagons.Add(tempGO);
        Party.Add(new PartyMember(newHero, tempGO));
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

        foreach (var item in Party)
        {
            pc.Add(item.Hero);
        }
        pc.Add(_Leader);

        return pc;
    }

    [System.Serializable]
    public struct PartyMember
        //public - для тестовых нужд, потом убрать
    {
        public Hero Hero;
        public GameObject GO;

        public PartyMember(Hero h, GameObject go)
        {
            Hero = h;
            GO = go;
        }
    }
}
