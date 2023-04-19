using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{    
    public List<GameObject> allMenus = new List<GameObject>();
    private int currentlySelectedMenu = 0;

    /// <summary>
    /// Change to selected menu.
    /// </summary>
    /// <param name="selectedMenu">Which menu to change to?</param>
    public void ChangeMenu(int selectedMenu)
    {
        allMenus[currentlySelectedMenu].SetActive(false);
        allMenus[selectedMenu].SetActive(true);
        currentlySelectedMenu = selectedMenu;
    }
}
