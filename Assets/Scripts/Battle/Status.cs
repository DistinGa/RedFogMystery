using UnityEngine;
using UnityEngine.UI;

public class Status : HeroUI
{
  [SerializeField] private Text atkIndicator = null;
  [SerializeField] private Text defIndicator = null;
  [SerializeField] private Text matIndicator = null;
  [SerializeField] private Text mdfIndicator = null;
  [SerializeField] private Text agiIndicator = null;
  [SerializeField] private GameObject armoIndicatorsParent = null;
  private ArmoButton[] armoButtons = null;
  private BaseOfInventar baseOfInventar = null;

  private void Start()
  {
    armoButtons = armoIndicatorsParent.GetComponentsInChildren<ArmoButton>();
    baseOfInventar = FindObjectOfType<BaseOfInventar>();
  }

  public override void UpdateUI ()
  {
    base.UpdateUI();
    atkIndicator.text = hero.HeroPropetries.Atk.ToString();
    defIndicator.text = hero.HeroPropetries.Def.ToString();
    matIndicator.text = hero.HeroPropetries.Mat.ToString();
    mdfIndicator.text = hero.HeroPropetries.Mdf.ToString();
    agiIndicator.text = hero.HeroPropetries.Agi.ToString();
    int i = 0;
    foreach (var armobutton in armoButtons)
    {
      armobutton.ThingPropetries = null;
    }

    foreach (var armo in hero.HeroPropetries.Armors)
    {
      foreach (var item in baseOfInventar.Items)
      {
        if (armo == item.Name)
        {
          armoButtons[i].ThingPropetries = item;
          i++;
        }
      }
    }
  }  
}
