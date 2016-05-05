using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
  public Text NameText = null;
  [SerializeField] private Text countText = null;
  [HideInInspector] public bool IsBusy = false;
  /*[HideInInspector] */public ThingPropetries ThingPropetries = new ThingPropetries();
  private Inventar inventar = null;  
  private Image thisImage = null;

  private void Start()
  {
    inventar = FindObjectOfType<Inventar>();
  }

  public void Load(ThingPropetries tPropetries)
  {
    ThingPropetries.Name = tPropetries.Name;
    ThingPropetries.Type = tPropetries.Type;
    ThingPropetries.Portrait = tPropetries.Portrait;
    ThingPropetries.Description = tPropetries.Description;
    ThingPropetries.Count = tPropetries.Count;
    ThingPropetries.Hp = tPropetries.Hp;
    ThingPropetries.Mhp = tPropetries.Mhp;
    ThingPropetries.Mp = tPropetries.Mp;
    ThingPropetries.Mmp = tPropetries.Mmp;
    ThingPropetries.Cr = tPropetries.Cr;
    ThingPropetries.Mcr = tPropetries.Mcr;
    ThingPropetries.Atk = tPropetries.Atk;
    ThingPropetries.Def = tPropetries.Def;
    ThingPropetries.Mat = tPropetries.Mat;
    ThingPropetries.Mdf = tPropetries.Mdf;
    NameText.text = ThingPropetries.Name;
    countText.text = ThingPropetries.Count.ToString();
    thisImage = GetComponent<Image>();
    thisImage.sprite = ThingPropetries.Portrait;
    thisImage.color = Color.white;
    IsBusy = true;
  }

  public void UpdateCount(int count)
  {
    ThingPropetries.Count += count;
    countText.text = ThingPropetries.Count.ToString();
  }
  
  public void OnPress()
  {
    if (IsBusy && !inventar.IsReadyAddPower)
    {
      if (ThingPropetries.Type == ThingType.Thing)
        AddPropetriesOnHero();
      if (ThingPropetries.Type == ThingType.Armor)      
        AddPropetriesOnHero();      
    }        
  }

  private void AddPropetriesOnHero()
  {
    HeroPropetries newHprop = new HeroPropetries();
    newHprop.Hp = ThingPropetries.Hp;
    newHprop.Mhp = ThingPropetries.Mhp;
    newHprop.Mp = ThingPropetries.Mp;
    newHprop.Mmp = ThingPropetries.Mmp;
    newHprop.Cr = ThingPropetries.Cr;
    newHprop.Mcr = ThingPropetries.Mcr;
    newHprop.Atk = ThingPropetries.Atk;
    newHprop.Def = ThingPropetries.Def;
    newHprop.Mat = ThingPropetries.Mat;
    newHprop.Mdf = ThingPropetries.Mdf;
    if (ThingPropetries.Type == ThingType.Armor)
      newHprop.Armors.Add(ThingPropetries.Name); 
    inventar.HeroPropetries = newHprop;    
    inventar.HeroesPanel.IsBlock = true;
    UpdateCount(-1);
    if (ThingPropetries.Count == 0)
      Clear();
    inventar.IsReadyAddPower = true;
    inventar.ShowHeroes();
  }

  public void Clear()
  {    
    if (ThingPropetries.Type == ThingType.Armor)
    {
      GetComponent<Button>().interactable = false;
    }
    else
    {
      ThingPropetries = new ThingPropetries();
      NameText.text = "";
      countText.text = "";
      thisImage.sprite = null;
      IsBusy = false;
      thisImage.color = new Color(1, 1, 1, 0.04f);//Цвет неактивной кнопки в меню
    }    
  }
  
  public void ShowDescription()
  {
    if (IsBusy)
      inventar.DescriptionField.text = ThingPropetries.Description;
  }

  public void HideDescription()
  {
    inventar.DescriptionField.text = "";
  }
}
