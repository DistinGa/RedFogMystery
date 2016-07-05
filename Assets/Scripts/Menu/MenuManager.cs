using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;
    private CharacterMoving _controller;

    void Start()
    {
        _controller = GameManager.GM.MainCharacter.GetComponent<CharacterMoving>();
    }

    void Update()
    {
        #region Tab Key
        //if (Input.GetAxisRaw("Tab") == 1)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (CurrentMenu.IsOpen)
            {
                Time.timeScale = 1;
                BlockMoving(false);
                CurrentMenu.IsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                BlockMoving(true);
                CurrentMenu = gameObject.transform.FindChild("MainMenu").GetComponent<Menu>();
                CurrentMenu.UpdateMenuData();
                CurrentMenu.IsOpen = true;
            }
        }
        #endregion
        #region Escape Key
        //if (Input.GetAxisRaw("Cancel") == 1)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentMenu.IsOpen)
            {
                if (CurrentMenu.name == "MainMenu")
                {
                    Time.timeScale = 1;
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
            CurrentMenu.UpdateMenuData();
            CurrentMenu.IsOpen = true;
        }
    }

    /// <summary> блокируем управление персонажем и "замараживаем" игру</summary>
    private void BlockMoving(bool block)
    {
        if (block)
        {
            _controller.KeyboardControl = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            _controller.KeyboardControl = true;
            Time.timeScale = 1.0f;
        }
    }

    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }
    /// задержка между сменами окон
    IEnumerator PauseAnimation()
    {
        CurrentMenu.UpdateMenuData();
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
        CurrentMenu.IsOpen = true;
    }

}
