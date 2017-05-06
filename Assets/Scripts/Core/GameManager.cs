using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Vector3 initialPosition;    //где будет размещён ГГ после загрузки сцены

    public static GameManager GM;
    float gameTime = 0;
    double gold;
    public Dictionary<string, int> questProgress;

    public GameObject MainCharacter;    //ГГ
    public List<GameObject> Vagons;     //список GO для отображения "паровозика"
    public GameObject VagonPrefab;      //префаб "вагона"
    public Hero[] Heroes = new Hero[4];
    public Hero _Leader = null;

    [Space(10)]
    //Списки всех предметов доступных в игре хранятся в объектах ScriptableObject по типам предметов.
    //В списках наличествующего инвентаря хранятся индексы строк в общих списках.
    [SerializeField]
    List<InventoryItem<ConsumableProperties>> consumables = new List<InventoryItem<ConsumableProperties>>();
    List<InventoryItem<MaterialProperties>> materials = new List<InventoryItem<MaterialProperties>>();
    List<InventoryItem<KeyProperties>> keys = new List<InventoryItem<KeyProperties>>();
    List<InventoryItem<EquipmentProperties>> equipments = new List<InventoryItem<EquipmentProperties>>();

    public SOConsumables AllConsumables;
    public SOMaterials AllMaterials;
    public SOKeys AllKeys;
    public SOEquipments AllEquipments;
    public QuestList QuestList; //ScripableObject со списком всех квестов. Нужен для начального заполнения.

    public DialogMenuScript DialogPanel;

    public void Awake()
    {
        //singletone
        if (GM == null)
            GM = this;
        else
        {
            DestroyImmediate(gameObject);
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
            Leader = FindHeroByName("MainHero");

        if (initialPosition != Vector3.zero)
            MainCharacter.transform.position = initialPosition;

        if (questProgress == null)
        //3аполним прогресс по квестам из начальных данных
        {
            InitQuestProgress();
        }
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

    //Поиск героя по имени.
    //UseIndexName - указывает, какое имя использовать для поиска
    //IndexName - это условные имена членов партии (MainHero, Healer, Tank, Mage)
    public Hero FindHeroByName(string heroName, bool UseIndexName = true)
    {
        Hero h = null;

        foreach (var item in Heroes)
        {
            if ((UseIndexName? item.HeroPropetries.IndexName: item.HeroPropetries.Name) == heroName)
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
        StartCoroutine(SaveManager.Load("Current", SceneManager.GetActiveScene().name));
        //!Временная мера. Убрать.
        Time.timeScale = 1;
    }

    public void Save()
    {
        SaveManager.Save("Current");
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

    //Добавление предмета в инвентарь. 
    //Если в инвентаре уже есть подобные предметы, увеличивается счётчик, если нет - добавляется элемент в список.
    public void AddInventory(Properties inv, int cnt = 1)
    {
        if (inv is ConsumableProperties)
        {
            InventoryItem<ConsumableProperties> item = consumables.Find(x => x.Item == inv);
            if (item != null)
                item.Count += cnt;
            else
                consumables.Add(new InventoryItem<ConsumableProperties>(AllConsumables, (inv as ConsumableProperties).index, cnt));
        }
        if (inv is MaterialProperties)
        {
            InventoryItem<MaterialProperties> item = materials.Find(x => x.Item == inv);
            if (item != null)
                item.Count += cnt;
            else
                materials.Add(new InventoryItem<MaterialProperties>(AllMaterials, (inv as MaterialProperties).index, cnt));
        }
        if (inv is EquipmentProperties)
        {
            InventoryItem<EquipmentProperties> item = equipments.Find(x => x.Item == inv);
            if (item != null)
                item.Count += cnt;
            else
                equipments.Add(new InventoryItem<EquipmentProperties>(AllEquipments, (inv as EquipmentProperties).index, cnt));
        }
        if (inv is KeyProperties)
        {
            InventoryItem<KeyProperties> item = keys.Find(x => x.Item == inv);
            if (item != null)
                item.Count += cnt;
            else
                keys.Add(new InventoryItem<KeyProperties>(AllKeys, (inv as KeyProperties).index, cnt));
        }
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

    public SavedGMdata GetGMdata()
    {
        SavedGMdata sGMd = new SavedGMdata();
        sGMd.sceneName = SceneManager.GetActiveScene().name;
        sGMd.initialPosition = JsonUtility.ToJson(GM.MainCharacter.transform.position);
        //sGMd.initialDirection = ;
        sGMd.gameTime = gameTime;
        sGMd.gold = gold;
        sGMd.leaderName = _Leader.HeroPropetries.IndexName;
        //Заполнение данных партии
        sGMd.heroParams = new string[Heroes.Length];
        for (int i = 0; i < Heroes.Length; i++)
        {
            sGMd.heroParams[i] = JsonUtility.ToJson(Heroes[i].GetDataToSave());
        }
        //Инвентарь
        sGMd.consumables = new string[Consumables.Count];
        for (int i = 0; i < Consumables.Count; i++)
        {
            //Чтобы не создавать для сохранения предметов инвентаря (Item, Count) дополнительной структуры или класса, 
            //воспользуемся для хранения этих двух значений типом Vector2.
            sGMd.consumables[i] = JsonUtility.ToJson(new Vector2(Consumables[i].Item.index, Consumables[i].Count));
        }

        sGMd.equipments = new string[Equipments.Count];
        for (int i = 0; i < Equipments.Count; i++)
        {
            sGMd.equipments[i] = JsonUtility.ToJson(new Vector2(Equipments[i].Item.index, Equipments[i].Count));
        }

        sGMd.materials = new string[Materials.Count];
        for (int i = 0; i < Equipments.Count; i++)
        {
            sGMd.materials[i] = JsonUtility.ToJson(new Vector2(Materials[i].Item.index, Materials[i].Count));
        }

        sGMd.keys = new string[Keys.Count];
        for (int i = 0; i < Equipments.Count; i++)
        {
            sGMd.keys[i] = JsonUtility.ToJson(new Vector2(Keys[i].Item.index, Keys[i].Count));
        }

        sGMd.questProgress = questProgress;

        return sGMd;
    }

    public void InitQuestProgress()
    {
        questProgress = new Dictionary<string, int>();
        foreach (var questState in QuestList.dataArray)
        {
            questProgress.Add(questState.Questid, questState.Result);
        }
    }

    public string QuestDescription(string QuestID)
    {
        foreach (var item in QuestList.dataArray)
        {
            if (item.Questid == QuestID)
                return item.Description;
        }

        return "---";
    }

    public void SetGMdata(SavedGMdata sGMd)
    {
        //ChangeScene(sGMd.sceneName, JsonUtility.FromJson<Vector3>(sGMd.initialPosition));
        //SceneManager.LoadScene(sGMd.sceneName);
        MainCharacter.transform.position = JsonUtility.FromJson<Vector3>(sGMd.initialPosition);
        gameTime = sGMd.gameTime;
        gold = sGMd.gold;
        Leader = FindHeroByName(sGMd.leaderName);
        //Восстановление данных партии
        for (int i = 0; i < sGMd.heroParams.Length; i++)
        {
            Hero.HeroParamsToSave hps = JsonUtility.FromJson<Hero.HeroParamsToSave>(sGMd.heroParams[i]);
            if(hps.isActive)
                ConnectToParty(Heroes[i]);
            Heroes[i].SetSavedData(hps);
        }
        //Инвентарь
        Consumables.Clear();
        foreach (var item in sGMd.consumables)
        {
            Vector2 inv = JsonUtility.FromJson<Vector2>(item);
            AddInventory(GM.AllConsumables.Get((int)inv.x), (int)inv.y);
        }

        Equipments.Clear();
        foreach (var item in sGMd.equipments)
        {
            Vector2 inv = JsonUtility.FromJson<Vector2>(item);
            AddInventory(GM.AllEquipments.Get((int)inv.x), (int)inv.y);
        }

        Materials.Clear();
        foreach (var item in sGMd.materials)
        {
            Vector2 inv = JsonUtility.FromJson<Vector2>(item);
            AddInventory(GM.AllMaterials.Get((int)inv.x), (int)inv.y);
        }

        Keys.Clear();
        foreach (var item in sGMd.keys)
        {
            Vector2 inv = JsonUtility.FromJson<Vector2>(item);
            AddInventory(GM.AllKeys.Get((int)inv.x), (int)inv.y);
        }

        questProgress = sGMd.questProgress;

    }

    //Есть ли заполненный QuestProgress
    public bool HasQuestProgress()
    {
        return (questProgress != null && questProgress.Count > 0);
    }

    //Возвращает состояние квеста. Если передан несуществующий индекс, возвращает 0.
    //0 - квест не взят
    //>0 - квест на какой-то стадии выполнения
    //<0 - квест выполнен с каким-то результатом
    public int GetQuestProgress(string qID)
    {
        if (questProgress.ContainsKey(qID))
            return questProgress[qID];
        else
            return 0;
    }

    //Устанавливает состояние квеста. Если нет квеста с указаннымм индексом, создаёт.
    public void SetQuestProgress(string qID, int qProgress)
    {
        if (questProgress.ContainsKey(qID))
            questProgress[qID] = qProgress;
        else
            questProgress.Add(qID, qProgress);
    }

    //Очищает данные, которые были необходимы только для диалога.
    public void ClearDialogQuestData()
    {
        List<string> keys = new List<string>();

        foreach (var item in questProgress)
        {
            if(item.Key.Length >= 3 && item.Key.Substring(0, 3) == "dlg")
                keys.Add(item.Key);
        }

        foreach (string key in keys)
        {
            questProgress.Remove(key);
        }
    }

    public void StartDialog(List<DialogMember> dlgMembers, ScriptableObject dlgDescription, string[] startRepID, CSEvent[] actions)
    {
        DialogPanel.Initialization(dlgMembers, dlgDescription, startRepID, actions);
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

[System.Serializable]
public class SavedGMdata
{
    public string sceneName;
    public string initialPosition;
    //public Direction initialDirection;
    public float gameTime;
    public double gold;
    public string leaderName;
    public string[] heroParams;
    public string[] consumables;
    public string[] equipments;
    public string[] materials;
    public string[] keys;
    public Dictionary<string, int> questProgress;

    //public bool isActive;   //персонаж в игре (присоединился к партии)
    //public int level;       //уровень
    //public int expToLevelUp;//опыт до следуюющего уровня
    //public float curHp = 0; //текущие очки здоровья
    //public float curMp = 0; //текущие очки маны
    //public float curCr = 0; //текущие очки коррупции
    ////Экипировка
    //public int weapon;
    //public int armor;
    //public int helmet;
    //public int[] accessory = new int[2];


}