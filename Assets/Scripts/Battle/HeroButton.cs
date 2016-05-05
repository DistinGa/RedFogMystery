using UnityEngine;

public class HeroButton : MonoBehaviour
{
  [SerializeField] private HeroUI heroUI = null;
  [SerializeField] private Inventar inventar = null;

  public void OnPress ()
  {
    if (inventar.IsReadyAddPower)
    {
      heroUI.Hero.AddPower(inventar.HeroPropetries);
      inventar.IsReadyAddPower = false;
      inventar.HeroesPanel.IsBlock = false;
    }
  }
}
