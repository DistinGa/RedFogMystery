using UnityEngine;
using System.Collections;

public class StatusMenuUpdater : MonoBehaviour
{
    public GameObject UI_CharacterInfo;

    [HideInInspector]
    public Hero CurHero;


    // Use this for initialization
    void Start()
    {
        CurHero = GameManager.GM.PartyContent()[0];
    }

    // Update is called once per frame
    void UpdateMenu()
    {
        UI_CharacterInfo.GetComponent<StatusMenuCharacter>().UpdateCharacter(CurHero);
    }
}
