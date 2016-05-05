using UnityEngine;
using UnityEngine.UI;

public class ArmoButton : MonoBehaviour
{
  public Text NameText = null;
  public Image Image = null;
  public Inventar Inventar = null;
  [SerializeField] private Status status = null;
  private ThingPropetries thingPropetries = null;

  public ThingPropetries ThingPropetries
  {
    get { return thingPropetries; }
    set
    {
      thingPropetries = value;
      if (thingPropetries != null)
      {
        NameText.text = thingPropetries.Name;
        Image.sprite = thingPropetries.Portrait;
        Image.color = Color.white;
      }
      else
      {
        NameText.text = "";
        Image.sprite = null;
        Image.color = new Color (0, 0, 0, 0);
      }
    }
  }

  public void OnPress()//Снять оружие в окне "Статус"
  {
    if (ThingPropetries != null)
    {
      Inventar.AddItem(ThingPropetries);      
      status.Hero.HeroPropetries.Armors.Remove(ThingPropetries.Name);
      status.Hero.HeroPropetries.Atk -= ThingPropetries.Atk;
      status.Hero.HeroPropetries.Mat -= ThingPropetries.Mat;
      status.Hero.HeroPropetries.Def -= ThingPropetries.Def;
      status.Hero.HeroPropetries.Mdf -= ThingPropetries.Mdf;
      status.Hero.HeroPropetries.Agi -= ThingPropetries.Agi;
      status.Hero.HeroUi.UpdateUI();
      status.UpdateUI();
      ThingPropetries = null;
    }
  }  
}
