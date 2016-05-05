using UnityEngine;

public class GuiMenuBase : MonoBehaviour
{
  protected HeroesPanel heroesPanel = null;
  private Animator thisAnimator = null;

  public virtual void Start()
  {
    heroesPanel = FindObjectOfType<HeroesPanel>();
    thisAnimator = GetComponent<Animator>();
  }

  public virtual void Show()
  {
    thisAnimator.SetBool("IsVisible", true);
  }

  public virtual void Hide()
  {
    thisAnimator.SetBool("IsVisible", false);
    heroesPanel.Show();       
  }

  private void Update ()
  {
    if (Input.GetKeyDown(KeyCode.Backspace) && !heroesPanel.IsBlock && thisAnimator.GetBool("IsVisible"))
    {      
      Hide();
    }
	}
}
