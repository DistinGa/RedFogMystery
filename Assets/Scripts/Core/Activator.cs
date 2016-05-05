using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour
{
    [SerializeField]
    private Quest quest = null;
    [SerializeField]
    private int NewStepValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.tag == "Player") проверка на персонажа

        if (other.GetComponent<Party>() != null)
        {
            quest.CurrentStep = NewStepValue;
        }        
    }
}
