using UnityEngine;
using System.Collections;

// DM
public class CharacterInPartyCondition : MonoBehaviour
{
    [SerializeField]
    private Party party = null; // необходимость под вопросом
    [SerializeField]
    private string heroName;
    [SerializeField]
    private bool isPresent = false;

    [SerializeField]
    private Quest ifTrueQuest = null;
    [SerializeField]
    private int ifTrueNewValue = 1;
    [SerializeField]
    private Quest ifElseQuest = null;
    [SerializeField]
    private int ifElseNewValue = 1;

    public void OnEventAction()
    {
        if (party != null && heroName != "" && ifTrueQuest != null && ifElseQuest != null)
        {
            bool tmp = false;
            foreach (var item in GameManager.GM.PartyContent())
            {
                if (item.HeroPropetries.Name == heroName)
                    tmp = true;
            }
            if (isPresent == tmp)
                ifTrueQuest.CurrentStep = ifTrueNewValue;
            else
                ifElseQuest.CurrentStep = ifElseNewValue;
        }
        else
            Debug.LogWarning("Ошибка !!! Объект " + gameObject.name + " Есть незаполненные поля!");
    }
}
