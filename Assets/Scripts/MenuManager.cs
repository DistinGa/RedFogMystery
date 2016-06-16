using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;
    private CharacterMoving _controller;

    void Start()
    {
        _controller = FindObjectOfType<CharacterMoving>();
        ShowMenu(CurrentMenu);
    }

    public void Update()
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
                    CurrentMenu.IsOpen = true;
                }
            }
        }
        #endregion
    }

    public void ShowMenu(Menu menu)
    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }

    private void BlockMoving(bool block)
    {
        _controller.KeyboardControl = !block;
    }
}
