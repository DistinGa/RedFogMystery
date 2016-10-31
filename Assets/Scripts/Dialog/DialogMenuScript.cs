using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum AnswerType
{
    permanent,  //выводится всегда
    temp,       //выводится, пока не будет нажат
    block,      //выводится, пока не будет выбран ответ другого типа
    single,     //выводится только на текущей итерации
    exit        //закрывает все ответы
}

public class DialogMenuScript : MonoBehaviour
{
    const int ansMaxCount = 8;

    public RectTransform[] AnswerButtons = new RectTransform[ansMaxCount];  //кнопки ответов
    public Transform LeftSpeakerPanel;
    public Transform RightSpeakerPanel;
    public RectTransform DialogContent;
    public InputField SecretAnswerPanel;
    public Image MiniGameIndicator;
    public RectTransform PartyPanel;

    Dictionary<string, Answer> AllAnswers = new Dictionary<string, Answer>();
    Dictionary<string, Replica> AllReplicas = new Dictionary<string, Replica>();
    List<Answer> secretAnswers = new List<Answer>();    //список секретных вопросов

    List<DialogMember> members = new List<DialogMember>();  //список участников диалога из триггера
    List<Answer> curAnswers = new List<Answer>();   //список активных ответов
    CSEvent[] allActions;   //список всех доступных действий
    List<CSEvent> selectedActions = new List<CSEvent>();  //список действий выбранных в результате диалога

    string curPartyMember;  //Активный член партии.
    string curSpeakerName;  //Имя говорящего текущую реплику персонажа

    int charisma;
    float timeAfterClick;   //время после нажатия кнопки (отслеживание клика для продолжения вывода реплик)

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void OnDisable()
    {
        Time.timeScale = 1;

        foreach (var item in selectedActions)
        {
            item.OnEventAction();
        }

        GameManager.GM.ClearDialogQuestData();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            timeAfterClick = Time.unscaledTime;
    }

    public void Initialization(List<DialogMember> dlgMembers, ScriptableObject dlgDescription, string[] startRepID, CSEvent[] dlgActions)
    {
        GameManager GM = GameManager.GM;
        gameObject.SetActive(true);

        curSpeakerName = "";
        charisma = 0;
        MiniGameIndicator.fillAmount = 0;
        members = dlgMembers;
        allActions = dlgActions;
        SecretAnswerPanel.text = "";

        //Заполнение общих списков реплик и ответов.
        AllAnswers.Clear();
        AllReplicas.Clear();
        curAnswers.Clear();
        secretAnswers.Clear();
        selectedActions.Clear();

        Answer newAnswer;
        Replica newReplica;
        foreach (var item in ((Test)dlgDescription).dataArray)
        {
            if (item.Answermark == 1)
            {
                //Ответ
                newAnswer = new Answer()
                {
                    ID = item.ID,
                    text = item.Textru,
                    deflt = (item.Deflt == 1),
                    secret = (item.Secret == 1),
                    type = item.ANSWERTYPE,
                    leaderCondition = item.Leader,
                    questIDCondition = item.Questid,
                    questResultCondition = item.Questresult,
                    charismaCondition = item.Charisma,
                    replics = item.Replics,
                    answers = item.Answers,
                    charismaAdd = item.Addcharisma,
                    actionID = item.Actionid,
                    questIDPost = item.Questidpost,
                    questResultPost = item.Questresultpost
                };

                if (newAnswer.secret)
                {
                    if (CheckCondition(newAnswer))
                        secretAnswers.Add(newAnswer);
                }
                else
                    AllAnswers.Add(newAnswer.ID, newAnswer);
            }
            else
            {
                //Реплика
                newReplica = new Replica()
                {
                    ID = item.ID,
                    text = item.Textru,
                    charID = item.Actionid, //ActionID в случае реплик работает как идентификатор участника диалога
                    leaderCondition = item.Leader,
                    questIDCondition = item.Questid,
                    questResultCondition = item.Questresult,
                    charismaCondition = item.Charisma
                };
                AllReplicas.Add(newReplica.ID, newReplica);
            }
        }

        int indx = 0;
        foreach (Hero hero in GM.PartyContent())
        {
            Transform heroPanel = PartyPanel.GetChild(indx);
            //heroPanel.GetComponent<Image>().sprite = hero.HeroPropetries.Portrait;
            heroPanel.FindChild("Background").GetComponent<Image>().sprite = hero.HeroPropetries.Portrait;
            heroPanel.FindChild("Name").GetComponent<Text>().text = hero.HeroPropetries.Name;
            heroPanel.gameObject.SetActive(true);

            if (GM.Leader == hero)
            {
                SelectHero(heroPanel);
                heroPanel.GetComponent<Toggle>().isOn = true;
            }

            indx++;
        }

        ////В стартовом ответе не должно быть условий, его тип должен быть AnswerType.single.
        //if (AllAnswers[startAnsID].type != AnswerType.single || AllAnswers[startAnsID].CharismaCondition > 0 || AllAnswers[startAnsID].LeaderCondition != "" || AllAnswers[startAnsID].QuestIDCondition != "")
        //{
        //    Debug.LogError("В " + dlgDescription.name + " не указан стартовый ответ.", dlgDescription);
        //    return;
        //}

        //curAnswers.Add(AllAnswers[startAnsID]);
        //ClickAnswer(0);

        //добавление стартовых ответов
        int lastAnswer = 0;
        foreach (var ansID in AllAnswers)
        {
            if (lastAnswer < ansMaxCount)
            {
                if (ansID.Value.deflt && CheckCondition(ansID.Value))
                    curAnswers.Add(ansID.Value);
            }

            lastAnswer++;
        }

        //Очистка листа диалога
        DialogContent.GetComponent<Text>().text = "";
        //Вывод стартовой реплики
        StartCoroutine(PrintReplicas(startRepID));
    }

    //Выводит реплики в зависимости от выполняемости условий. В случае нескольких реплик вывод происходит поштучно через пробел
    public IEnumerator PrintReplicas(string[] reps)
    {
        //Отключение кнопок ответов на время вывода
        SecretAnswerPanel.interactable = false;
        foreach (RectTransform item in AnswerButtons)
        {
            item.Find("Text").gameObject.SetActive(false);
            item.GetComponent<Button>().interactable = false;
        }

        //Вывод реплик
        string rep;
        bool f1 = false; //флаг показывающий, что первая реплика выведена 
        for (int i = 0; i < reps.Length; i++)
        {
            rep = reps[i];
            if (rep == "")    //почему-то иногда QuickSheet пустую ячейку считывает, как массив с одним пустым значением. Для обхода эти строки.
                continue;

            if (CheckCondition(AllReplicas[rep]))
            {
                DialogMember dlgMmbr = members[AllReplicas[rep].charID];
                Text dlgText = DialogContent.GetComponent<Text>();

                //Пауза перед выводом следующей реплики. Следующая реплика выводится по нажатию пробела.
                if (f1)
                {
                    yield return new WaitForEndOfFrame();   //чтобы GetKeyUp не срабатывал несколько раз подряд
                    yield return new WaitUntil(() => (Input.GetKeyUp(KeyCode.Space) || ((Time.unscaledTime - timeAfterClick) < 0.3f && Input.GetMouseButtonUp(0))));
                }
                f1 = true;

                //Если говорит не активный член партии, обновляем правый портрет.
                if (dlgMmbr.Name != curPartyMember)
                    SetSpeakerPanel(RightSpeakerPanel, dlgMmbr.Portrait, dlgMmbr.Name);

                //Если говорит другой персонаж, выводим его имя.
                if (curSpeakerName != dlgMmbr.Name)
                {
                    dlgText.text += "\n\n<b><color=" + dlgMmbr.nameColor + ">" + dlgMmbr.Name + "</color></b>";
                    curSpeakerName = dlgMmbr.Name;
                }

                dlgText.text += "\n<color=" + dlgMmbr.repColor + ">" + AllReplicas[rep].text + "</color>";
            }
        }

        //Включение кнопок ответов после вывода последней реплики
        RectTransform ab, txt;
        for (int i = 0; i < curAnswers.Count; i++)
        {
            ab = AnswerButtons[i];
            txt = ab.Find("Text") as RectTransform;
            txt.GetComponent<Text>().text = curAnswers[i].text;
            txt.gameObject.SetActive(true);
            ab.GetComponent<Button>().interactable = true;
        }
        SecretAnswerPanel.interactable = true;

    }

    bool CheckCondition(IConditional dlgObj)
    {
        bool res = true;
        GameManager GM = GameManager.GM;

        //проверка говорящего
        if (dlgObj.LeaderCondition != "")
        {
            string conditionName = GM.FindHeroByName(curPartyMember, false).IndexName;
            //Если перед именем персонажа для условия "!", обрабатываем условие, как "НЕ".
            if (dlgObj.LeaderCondition.Substring(0, 1) == "!" ? dlgObj.LeaderCondition == "!" + conditionName : dlgObj.LeaderCondition != conditionName)
                res = false;
        }
        //проверка харизмы
        if (dlgObj.CharismaCondition > 0 && charisma < dlgObj.CharismaCondition)
            res = false;
        //проверка прогресса квеста
        //Если перед QuestIDCondition стоит "!", обрабатываем условие, как "НЕ".
        if (dlgObj.QuestIDCondition != "" && (dlgObj.QuestIDCondition.Substring(0, 1) == "!" ? GM.GetQuestProgress(dlgObj.QuestIDCondition.Substring(1)) == dlgObj.QuestResultCondition : GM.GetQuestProgress(dlgObj.QuestIDCondition) != dlgObj.QuestResultCondition))
            res = false;

        return res;
    }

    //Обработка нажатия на кнопку ответа
    public void ClickAnswer(int selAnswId)
    {
        ProcessAnswer(curAnswers[selAnswId]);
    }

    void ProcessAnswer(Answer selAnswer)
    {
        //Вывод ответа
        Hero hero = GameManager.GM.FindHeroByName(curPartyMember, false);
        Text dlgText = DialogContent.GetComponent<Text>();

        dlgText.text += "\n\n<b><color=" + hero.Color + ">" + hero.HeroPropetries.Name + "</color></b>";
        dlgText.text += "\n<color=" + hero.Color + ">" + selAnswer.text + "</color>";
        curSpeakerName = hero.HeroPropetries.Name;

        //Выбрали финальный ответ, очищаем список ответов и выходим.
        if (selAnswer.type == AnswerType.exit)
        {
            curAnswers.Clear();
        }
        //обработка списка текущих ответов
        for (int i = curAnswers.Count - 1; i > 0; i--)
        {
            switch (curAnswers[i].type)
            {
                case AnswerType.permanent:  //постоянный ответ - доступен всегда
                    break;
                case AnswerType.temp:   //временный - доступен, пока на него не нажали
                    if (curAnswers[i].ID == selAnswer.ID)
                        curAnswers.RemoveAt(i);
                    break;
                case AnswerType.block:  //блочный - доступен, пока не выбран другой тип ответа
                    if (selAnswer.type != AnswerType.block)
                        curAnswers.RemoveAt(i);
                    break;
                case AnswerType.single: //единичный - доступен только одну итерацию
                    curAnswers.RemoveAt(i);
                    break;
                default:
                    break;
            }

            //добавление новых ответов
            int lastAnswer = curAnswers.Count;

            foreach (string ansID in selAnswer.answers)
            {
                if (ansID == "")    //почему-то иногда QuickSheet пустую ячейку считывает, как массив с одним пустым значением. Для обхода эти строки.
                    continue;

                if (lastAnswer < ansMaxCount)
                {
                    if (CheckCondition(AllAnswers[ansID]) && !curAnswers.Exists(x => (x.ID == ansID)))
                        curAnswers.Add(AllAnswers[ansID]);
                }

                lastAnswer++;
            }
        }

        //Заполнение списка действий
        if (selAnswer.actionID != 0)
        {
            if (selAnswer.actionID > 0 && selAnswer.actionID <= allActions.Length)
                selectedActions.Add(allActions[selAnswer.actionID - 1]);
        }

        //Вывод реплик
        StartCoroutine(PrintReplicas(selAnswer.replics));

        if (selAnswer.charismaAdd > 0)
        {
            charisma += selAnswer.charismaAdd;
            MiniGameIndicator.fillAmount = charisma / 100f;
        }

        //Установка результатов квестов
        if (selAnswer.questIDPost != "")
            GameManager.GM.SetQuestProgress(selAnswer.questIDPost, selAnswer.questResultPost);
    }

    public void SetSpeakerPanel(Transform SpeakerPanel, Sprite Portrait, string Name)
    {
        SpeakerPanel.Find("Portrait").GetComponent<Image>().sprite = Portrait;
        SpeakerPanel.Find("NamePanel/SpeakerName").GetComponent<Text>().text = Name;
        //Если обновляем левую панель, то установим и нового спикера от партии
        if (SpeakerPanel == LeftSpeakerPanel)
            curPartyMember = Name;
    }

    public void SelectHero(Transform heroPanel)
    {
        SetSpeakerPanel(LeftSpeakerPanel, heroPanel.FindChild("Background").GetComponent<Image>().sprite, heroPanel.FindChild("Name").GetComponent<Text>().text);
    }

    //Проверка секретного ответа
    public void GiveSecretAnswer(string str)
    {
        if (secretAnswers.Exists(x => x.text.ToLower() == str.ToLower()))
            ProcessAnswer(secretAnswers.Find(x => x.text.ToLower() == str.ToLower()));
    }
}

public struct Answer : IConditional
{
    public string ID;
    public string text;    //текст, выводимый на кнопке
    public bool deflt;
    public bool secret;
    public AnswerType type;
    public string leaderCondition;
    public string questIDCondition;
    public int questResultCondition;
    public int charismaCondition;
    //public List<string> replics;
    public string[] replics;
    public string[] answers;
    public int charismaAdd;
    public int actionID;
    public string questIDPost;
    public int questResultPost;

    public string LeaderCondition
    {
        get { return leaderCondition; }
    }

    public string QuestIDCondition
    {
        get { return questIDCondition; }
    }

    public int QuestResultCondition
    {
        get { return questResultCondition; }
    }

    public int CharismaCondition
    {
        get { return charismaCondition; }
    }
}

public struct Replica: IConditional
{
    public string ID;
    public string text;
    public int charID; //идентификатор персонажа. Берётся из настроек диалога на триггере (-1 - говорящий член партии, 0 - дефолтный НПЦ)
    public string leaderCondition;
    public string questIDCondition;
    public int questResultCondition;
    public int charismaCondition;

    public string LeaderCondition
    {
        get { return leaderCondition; }
    }

    public string QuestIDCondition
    {
        get { return questIDCondition; }
    }

    public int QuestResultCondition
    {
        get { return questResultCondition; }
    }

    public int CharismaCondition
    {
        get { return charismaCondition; }
    }
}

[System.Serializable]
public struct DialogMember
{
    public string Name;
    public Sprite Portrait;
    public string nameColor;
    public string repColor;
}

public interface IConditional
{
    string LeaderCondition { get; }
    string QuestIDCondition { get; }
    int QuestResultCondition { get; }
    int CharismaCondition { get; }
}
