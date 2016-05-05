using UnityEngine;
using System.Collections.Generic;

public class HeroesPanel : MonoBehaviour
{
  [SerializeField] private GuiMenuBase[] menus = null;
  public List<HeroUI> HeroesUi = new List<HeroUI>(4);
  private int attachedHeroes = 0;
  private Animator thisAnimator = null;
  private bool isChildMenuOpen = false;
  [HideInInspector] public bool IsBlock = true;
  private CharacterMoving characterMoving = null;

  public HeroUI AttachHero (Hero hero)
  {
    if (attachedHeroes < 4)
    {
      HeroesUi[attachedHeroes].gameObject.SetActive(true);
      HeroesUi[attachedHeroes].Hero = hero;      
      ++attachedHeroes;
      return HeroesUi[attachedHeroes - 1];
    }
    Debug.LogWarning("You try attach more than 4 heroes");
    return null;
  }

  private void Start()
  {
    thisAnimator = GetComponent<Animator>();
    characterMoving = FindObjectOfType<CharacterMoving>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      if (thisAnimator.GetBool("IsVisible"))
      {
        Hide();
        IsBlock = true;
      }
      else
      {
        if (!isChildMenuOpen && characterMoving.KeyboardControl)
        {
          IsBlock = false;
          Show();
        }
      }
    }
  }

  public void SelectMenu(int activeMenu)
  {    
    int i = 0;
    foreach (var menu in menus)
    {
      if (i == activeMenu)      
        menu.Show();      
      i++;
    }
    isChildMenuOpen = true;
    Hide();
  }

  public void Show()
  {
    if (!IsBlock)
    {
      thisAnimator.SetBool("IsVisible", true);
      isChildMenuOpen = false;
      characterMoving.KeyboardControl = false;
    }
  }

  public void Hide()
  {
    thisAnimator.SetBool("IsVisible", false);
    characterMoving.KeyboardControl = !isChildMenuOpen;
  }  
}
