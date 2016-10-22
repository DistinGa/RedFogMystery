using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class DialogPanel : MonoBehaviour
{
  public Text MainText = null;
  public Image PortraitImage = null;
  public Image PortraitBackground = null;
  public int CurrentLanguage = 0;//0-russian, 1-english
  private Animator thisAnimator = null;  

  private void Start()
  {
    thisAnimator = GetComponent<Animator>();    
  }

  public void Show()
  {
    thisAnimator.SetBool("IsVisible", true);    
    PortraitImage.enabled = false;
    PortraitBackground.enabled = false;
  }

  public void Hide()
  {
    thisAnimator.SetBool("IsVisible", false);
  }

  public void SetLanguage(int lang)
  {
    CurrentLanguage = lang;
  }
}
