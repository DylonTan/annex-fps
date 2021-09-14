using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public string menuName;

    public bool isOpen;

    public void Open()
    {
        isOpen = true;
        // Set current game object to active
        gameObject.SetActive(true);
    }

    public void Close()
    {
        isOpen = false;
        // Set current game object to inactive
        gameObject.SetActive(false);
    }
}
