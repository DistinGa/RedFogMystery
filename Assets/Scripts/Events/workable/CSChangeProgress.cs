using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Cut Scenes/Статус квеста")]
public class tmp_ChangeProgress : CSEvent {
    public string QuestID;
    public int QuestValue;

    public override void OnEventAction()
    {
        GameManager.GM.SetQuestProgress(QuestID, QuestValue);
    }
}
