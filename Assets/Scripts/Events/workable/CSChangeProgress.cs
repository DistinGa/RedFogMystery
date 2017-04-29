using UnityEngine;
using System.Collections;
using System;

public class tmp_ChangeProgress : CSEvent {
    public string QuestID;
    public int QuestValue;

    public override void OnEventAction()
    {
        GameManager.GM.SetQuestProgress(QuestID, QuestValue);
    }
}
