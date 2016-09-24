using UnityEngine;
using System.Collections;
using System;

public class tmp_ChangeProgress : CSEvent {
    public override void OnEventAction()
    {
        GameManager.GM.QuestProgress["1"] = 5;
    }
}
