using UnityEngine;

public class ConnectToParty : MonoBehaviour
{
  [SerializeField] private string heroName;

  public void OnEventAction()
  {
    if (heroName != "")
    {
      GameManager.GM.ConnectToParty(heroName);
      //Destroy(hero.gameObject);
    }
    else
      Debug.LogWarning("Объект " + gameObject.name + " не указано имя героя!");
  }
}
