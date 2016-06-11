using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuCharactersInfo : MonoBehaviour
{
    public TestParty party;
    public GameObject[] UI_Characters;

    // Use this for initialization
    void Start()
    {
        UpdateMenu();
    }

    public void UpdateMenu(List<Hero> party = null)
    {
        if (party == null)
            party = GamaManager.GM.PartyContent();

        for (int i = 0; i < UI_Characters.Length; i++)
        {
            if (party[i] != null)
            {
                UI_Characters[i].GetComponentInChildren<CharacterInfo>().hero = party.Characters[i];
            }
            else
                UI_Characters[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
