using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[Serializable] public class ItemsGroup
{
  public GameObject ItemButtonsParent = null;
  public List<ItemButton> itemButtons = new List<ItemButton>();
}

public class Inventar : MonoBehaviour
{
  public ItemsGroup[] ItemGroups = new ItemsGroup[4];  
  [SerializeField] private GameObject baseOfInventar = null;
  public Text DescriptionField = null;
  [SerializeField] private Animator heroesAnimaator = null;
  public HeroesPanel HeroesPanel = null;
  [HideInInspector] public bool IsReadyAddPower = false;
  public HeroPropetries HeroPropetries = null;
  private int groupNum = 0;

  private void Awake()
  {
    if (FindObjectOfType<BaseOfInventar>() == null)
      Instantiate(baseOfInventar, Vector3.zero, Quaternion.identity);
    int i = 0;
    foreach (var itemGroup in ItemGroups)
    {
      itemGroup.itemButtons = new List<ItemButton>(itemGroup.ItemButtonsParent.GetComponentsInChildren<ItemButton>());
      itemGroup.ItemButtonsParent.SetActive(i == 0);
      i++;
    }
  }

  public void AddItem(ThingPropetries thingPropetries)
  {
    bool addToExistButton = false;
    switch (thingPropetries.Type)
    {
      case ThingType.Thing:
        groupNum = 0;
        break;
      case ThingType.Armor:
        groupNum = 1;
        break;
      case ThingType.Material:
        groupNum = 2;
        break;
      case ThingType.Key:
        groupNum = 3;
        break;
    }
    foreach (var itemButton in ItemGroups[groupNum].itemButtons)
    {
      if (itemButton.ThingPropetries.Name == thingPropetries.Name)
      {
        itemButton.UpdateCount(thingPropetries.Count);
        itemButton.GetComponent<Button>().interactable = true;
        addToExistButton = true;
      }
    }
    if (!addToExistButton)
    {
      int buttonIndex = FirstFreeButton;
      ItemGroups[groupNum].itemButtons[buttonIndex].Load(thingPropetries);
    }    
  }

  private int FirstFreeButton
  {
    get
    {
      int i = 0;
      foreach (var itemButton in ItemGroups[groupNum].itemButtons)
      {
        if (!itemButton.IsBusy)
          return i;
        i++;
      }
      Debug.LogError("Has no free buttons");
      return 0;
    }
  }

  public void ShowHeroes()
  {
    heroesAnimaator.SetBool("IsVisible", true);
  }

  public void HideHeroes()
  {
    heroesAnimaator.SetBool("IsVisible", false);
  }
}
