using UnityEngine;
using System.Collections;
using System;

public class tmp_GetProgress : CSEvent
{
    public string QuestID;

    public override void OnEventAction()
    {
        print(GameManager.GM.GetQuestProgress(QuestID));
    }
}
