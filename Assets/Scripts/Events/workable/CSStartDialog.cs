using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSStartDialog : CSEvent
{
    public List<DialogMember> dlgMembers;
    public ScriptableObject dlgDescription;
    public string[] startRepID;
    public ActionStruct[] Actions;

    public override void OnEventAction()
    {
        CSEvent[] events = new CSEvent[Actions.Length];
        for (int i = 0; i < Actions.Length; i++)
            events[i] = Actions[i].Action;

        GameManager.GM.StartDialog(dlgMembers, dlgDescription, startRepID, events);
    }
}

[System.Serializable]
public struct ActionStruct
{
    public string Description;
    public CSEvent Action;
}