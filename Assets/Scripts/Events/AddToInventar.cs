using UnityEngine;

public class AddToInventar: MonoBehaviour
{
  [SerializeField] private string itemName;
  [SerializeField] private int count = 1;
  private Inventar inventar = null;
  private BaseOfInventar baseOfInventar = null;
  private bool searchIsSeccessfull = false;
  private void Start()
  {
    inventar = FindObjectOfType<Inventar>();
    baseOfInventar = FindObjectOfType<BaseOfInventar>(); ;
  }

  public void OnEventAction()
  {
    if (baseOfInventar != null)
    {
      foreach (var item in baseOfInventar.Items)
      {
        if (itemName == item.Name)
        {
          item.Count = count;
          inventar.AddItem(item);
          searchIsSeccessfull = true;
          break;          
        }
      }
      if (!searchIsSeccessfull)
        Debug.LogWarning("Предмет " + itemName + " не найден в базе инвентаря!");
    }
    else
    {
      Debug.LogWarning("База инвентаря не загружена!");
    }
  }  
}
