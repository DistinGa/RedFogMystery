using UnityEngine;

public class RemoveFromInventar : MonoBehaviour
{
  [SerializeField] private string itemName;
  //private Inventar inventar = null;
  //private bool searchIsSeccessfull = false;

  //private void Start()
  //{
  //  inventar = FindObjectOfType<Inventar>();    
  //}

  //public void OnEventAction()
  //{
  //  foreach (var itemGroup in inventar.ItemGroups)
  //  {
  //    foreach (var itemButton in itemGroup.itemButtons)
  //    {
  //      if (itemButton.ThingPropetries.Name == itemName)
  //      {
  //        itemButton.Clear();
  //        searchIsSeccessfull = true;
  //      }        
  //    }
  //    if (!searchIsSeccessfull)
  //      Debug.LogWarning("Не удалось найти предмет " + itemName + " в инвентаре, возможно герой ещё не получил его");
  //  }    
  //}
}
    
  