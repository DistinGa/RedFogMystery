using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;
    private CharacterMoving _controller;

    void Start()
    {
        _controller = FindObjectOfType<CharacterMoving>();
    }

    void Update()
    {
        #region Tab Key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (CurrentMenu.IsOpen)
            {
                BlockMoving(false);
                CurrentMenu.IsOpen = false;
            }
            else
            {
                BlockMoving(true);
                CurrentMenu = gameObject.transform.FindChild("MainMenu").GetComponent<Menu>();
                CurrentMenu.UpdateMenuData();
                CurrentMenu.IsOpen = true;
            }
        }
        #endregion
        #region Escape Key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentMenu.IsOpen)
            {
                if (CurrentMenu.name == "MainMenu")
                {
                    BlockMoving(false);
                    CurrentMenu.IsOpen = false;
                }
                else
                {
                    CurrentMenu.IsOpen = false;
                    CurrentMenu = gameObject.transform.FindChild("MainMenu").GetComponent<Menu>();
                    StartCoroutine(PauseAnimation());

                }
            }
        }
        #endregion
    }

    /// <summary> получаем новое окно меню для его отображения</summary>
    public void ShowMenu(Menu menu)
    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        if (CurrentMenu.name == "MainMenu")
        {
            CurrentMenu = menu;
            StartCoroutine(PauseAnimation());
        }
        else
        {
            CurrentMenu = menu;
            CurrentMenu.IsOpen = true;
        }
    }

    /// <summary> блокируем управление персонажем и "замараживаем" игру</summary>
    private void BlockMoving(bool block)
    {
        if (block)
        {
            _controller.KeyboardControl = false;
            //Time.timeScale = 0.0f;
        }
        else
        {
            _controller.KeyboardControl = true;
            //Time.timeScale = 1.0f;
        }
    }

    /// задержка между сменами окон
    IEnumerator PauseAnimation()
    {
        yield return new WaitForSeconds(1);
        CurrentMenu.IsOpen = true;
    }
}
