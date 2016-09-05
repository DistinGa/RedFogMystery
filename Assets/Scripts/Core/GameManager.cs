using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Vector3 initialPosition;    //где будет размещён ГГ после загрузки сцены

    public static GameManager GM;
    float gameTime = 0;
    double gold;

    public GameObject MainCharacter;    //ГГ
    public List<GameObject> Vagons;     //список GO для отображения "паровозика"
    public GameObject VagonPrefab;      //префаб "вагона"
    public Hero[] Heroes = new Hero[4];
    public Hero _Leader = null;

    [Space(10)]
    //Списки всех предметов доступных в игре хранятся в объектах ScriptableObject по типам предметов.
    //В списках наличествующего инвентаря хранятся индексы строк в общих списках.
    //!!!public для тестов. Потом убрать
    [SerializeField]
    public List<InventoryItem<ConsumableProperties>> consumables = new List<InventoryItem<ConsumableProperties>>();
    public List<InventoryItem<MaterialProperties>> materials = new List<InventoryItem<MaterialProperties>>();
    public List<InventoryItem<KeyProperties>> keys = new List<InventoryItem<KeyProperties>>();
    public List<InventoryItem<EquipmentProperties>> equipments = new List<InventoryItem<EquipmentProperties>>();
    //!!!public для тестов. Потом убрать

    public SOConsumables AllConsumables;
    public SOMaterials AllMaterials;
    public SOKeys AllKeys;
    public SOEquipments AllEquipments;

    public void Awake()
    {
        //singletone
        if (GM == null)
            GM = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitializeGM();
    }

    public void Start()
    {
        if (AllConsumables == null)
            Debug.LogError("GameManager: Не назначен список Consumables", AllConsumables);
        if (AllMaterials == null)
            Debug.LogError("GameManager: Не назначен список Materials", AllMaterials);
        if (AllKeys == null)
            Debug.LogError("GameManager: Не назначен список Keys", AllKeys);
        if (AllEquipments == null)
            Debug.LogError("GameManager: Не назначен список Equipments", AllEquipments);
    }

    void InitializeGM()
    {
        if (MainCharacter == null)
            MainCharacter = FindObjectOfType<CharacterMoving>().gameObject;

        if (Leader.lp == null)
            Leader = FindHeroByName("Gehend");

        if (initialPosition != Vector3.zero)
            MainCharacter.transform.position = initialPosition;

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
                    MainCharacter.GetComponent<Animator>().runtimeAnimatorController = curHero.HeroPropetries.AnimatorController;
                }
                else
                    //все остальные
                    Vagons[i++].GetComponent<Animator>().runtimeAnimatorController = curHero.HeroPropetries.AnimatorController;
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
            tempGO.GetComponent<Animator>().runtimeAnimatorController = newHero.HeroPropetries.AnimatorController;
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
            if (item != null)
                item.SendMessage("SetWind", sw);
        }
    }

    public void Load()
    {

    }

    public void Save()
    {

    }

    public List<InventoryItem<ConsumableProperties>> Consumables
    {
        get { return consumables; }
    }

    public List<InventoryItem<MaterialProperties>> Materials
    {
        get { return materials; }
    }

    public List<InventoryItem<KeyProperties>> Keys
    {
        get { return keys; }
    }

    public List<InventoryItem<EquipmentProperties>> Equipments
    {
        get { return equipments; }
    }

    public double Gold
    {
        get { return gold; }
    }

    public void AddGold(double Amount)
    {
        gold += Amount;
    }

    public void AddInventory(Properties inv, int cnt = 1)
    {
        if (inv is ConsumableProperties)
        {
            consumables.Add(new InventoryItem<ConsumableProperties>(AllConsumables, (inv as ConsumableProperties).index, cnt));
        }
        if (inv is MaterialProperties)
            materials.Add(new InventoryItem<MaterialProperties>(AllMaterials, (inv as MaterialProperties).index, cnt));
        if (inv is EquipmentProperties)
            equipments.Add(new InventoryItem<EquipmentProperties>(AllEquipments, (inv as EquipmentProperties).index, cnt));
        if (inv is KeyProperties)
            keys.Add(new InventoryItem<KeyProperties>(AllKeys, (inv as KeyProperties).index, cnt));
    }

    public void ChangeScene(string sceneName, Vector3 initPos)
    {
        initialPosition = initPos;
        SceneManager.LoadScene(sceneName);
    }

    public void OnLevelWasLoaded(int level)
    {
        InitializeGM();
    }

    public void OverheadText(Transform target, string text, float delay = 5000f)
    {
        //OverheadTextScript script = target.Find("OverheadText").GetComponent<OverheadTextScript>();
        OverheadTextScript script = target.GetComponent<OverheadTextScript>();
        if (script == null)
        {
            Debug.LogError("Отсутствует объект для отображения всплывающего текста.", target);
            return;
        }

        script.ShowOverheadText(text, delay);
    }
}

[System.Serializable]
public class InventoryItem<T> where T : class//where T : Properties
{
    int index;
    IInventorySO ItemsBase;
    public int Count;

    public InventoryItem(IInventorySO SO, int i, int cnt)
    {
        ItemsBase = SO;
        index = i;
        Count = cnt;
    }

    public T Item
    {
        get
        {
            return (ItemsBase.Get(index)) as T;
        }
    }
}
