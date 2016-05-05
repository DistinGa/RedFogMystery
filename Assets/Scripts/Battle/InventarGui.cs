using UnityEngine;

public class InventarGui : GuiMenuBase
{
  [SerializeField] private Inventar inventar = null;  

  public override void Hide ()
  {
    if (!inventar.IsReadyAddPower)
    {
      base.Hide();
      inventar.HideHeroes();
    }
  }
}
