using UnityEngine;

public class ConnectToParty : MonoBehaviour
{
  [SerializeField] private TriggerBase hero = null;

  public void OnEventAction()
  {
    if (hero != null)
    {
      FindObjectOfType<Party>().Connect(hero.gameObject.name);
      Destroy(hero.gameObject);
    }
    else
      Debug.LogWarning("Дмитрий! Объект " + gameObject.name + " hero не назначен!");
  }
}
