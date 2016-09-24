using UnityEngine;
using System.Collections;
using System;

public class tmp_GetProgress : CSEvent
{
    public override void OnEventAction()
    {
        print(GameManager.GM.QuestProgress["1"]);
    }
}
