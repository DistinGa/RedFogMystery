using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class QuestListMenu : MonoBehaviour {
    public GameObject MainPanel;
    public GameObject QuestItem;
    public Color ActiveQuestColor, CompletedQuestColor;

	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            MainPanel.SetActive(!MainPanel.activeSelf);
            UpdateQuestList();
        }

    }

    void UpdateQuestList()
    {
        QuestListData[] QList = GameManager.GM.QuestList.dataArray.Where(x => x.Result != 0).ToArray(); //берём все квесты, кроме ещё невзятых

        if (MainPanel.activeSelf)
        {
            Text Qtxt;
            foreach (var item in QList)
            {
                GameObject newQI = Instantiate(QuestItem);
                newQI.SetActive(true);
                newQI.transform.SetParent(MainPanel.transform);
                Qtxt = newQI.GetComponent<Text>();
                Qtxt.color = item.Result < 0 ? CompletedQuestColor: ActiveQuestColor;
                Qtxt.text = item.Description;
            }
        }
        else
        {
            while (MainPanel.transform.childCount > 1)
            {
                DestroyImmediate(MainPanel.transform.GetChild(1).gameObject);
            }
        }
    }
}
