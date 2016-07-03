using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuUpdater : MonoBehaviour
{
    public GameObject[] UI_Characters;
    public Text UI_Money;

    public void UpdateMenu()
    {
        List<Hero> party = GameManager.GM.PartyContent();
        UI_Money.text = GameManager.GM.Gold.ToString();
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
