using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuUpdater : MonoBehaviour
{
    public GameObject[] UI_Characters;

    public void UpdateMenu()
    {
        List<Hero> party = GameManager.GM.PartyContent();
        if (party.Count > 0)
        {
            for (int i = 0; i < UI_Characters.Length; i++)
            {
                if (i < party.Count)
                {
                    UI_Characters[i].transform.GetChild(0).gameObject.SetActive(true);
                    UI_Characters[i].GetComponentInChildren<MainMenuCharacter>().UpdateCharacter(party[i]);
                }
                else
                    UI_Characters[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
