using UnityEngine;

[AddComponentMenu("Cut Scenes/Присоединить к партии")]
public class CSConnectToParty : CSEvent
{
  [SerializeField] private string heroName;

  public override void OnEventAction()
  {
        if (heroName != "")
        {
            GameManager.GM.ConnectToParty(heroName);
            if (NextStep != null)
                NextStep();

        }
        else
            Debug.LogWarning("Не указано имя героя!");
    }
}
