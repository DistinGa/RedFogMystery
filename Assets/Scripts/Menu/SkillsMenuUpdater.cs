using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsMenuUpdater : MonoBehaviour
{
    public GameObject UI_CharacterInfo;
    public GameObject UI_SkillsList;

    [HideInInspector]
    public Hero CurHero;

    //private bool isMagic;    //текущее состояние списка (магия/способности)


    public void Start()
    {
        CurHero = GameManager.GM.PartyContent()[0];
        //isMagic = true;
    }

    public void UpdateMenu()
    {
        UI_CharacterInfo.GetComponent<SkillsMenuCharacter>().UpdateCharacter(CurHero);
    }
}
