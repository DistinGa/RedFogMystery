using UnityEngine;

public class StatusGui : GuiMenuBase
{
  [SerializeField] private Status status = null;
  private int currentHero = 0;

  public override void Show()
  {
    base.Show();
    status.Hero = heroesPanel.HeroesUi[currentHero].Hero;
    status.UpdateUI();
  }

  public void OnPressNextHero()
  {
    currentHero++;
    if (currentHero == 4)
      currentHero = 0;    
    if (!heroesPanel.HeroesUi[currentHero].isActiveAndEnabled)
      currentHero = 0;     
    status.Hero = heroesPanel.HeroesUi[currentHero].Hero;
    status.UpdateUI();    
  }
}
