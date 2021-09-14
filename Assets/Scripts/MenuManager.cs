using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField]
    public Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string _menuName)
    {
        foreach (Menu menu in menus)
        {
            if (menu.menuName == _menuName)
            {
                menu.Open();
            } else if (menu.isOpen)
            {
                CloseMenu(menu);
            }
        }
    }

    public void OpenMenu(Menu _menu)
    {
        foreach (Menu menu in menus)
        {
            if (menu.isOpen)
            {
                menu.Close();
            }
        }
        _menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
