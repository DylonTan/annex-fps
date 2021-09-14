using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Singleton instance
    public static MenuManager Instance;

    [SerializeField]
    public Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string _menuName)
    {
        // Loop through each menu in list of menus, open if menu is found, close otherwise
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
        // Loop through each menu in list of menus and close
        foreach (Menu menu in menus)
        {
            if (menu.isOpen)
            {
                menu.Close();
            }
        }
        // Open menu provided
        _menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        // Close menu provided
        menu.Close();
    }
}
