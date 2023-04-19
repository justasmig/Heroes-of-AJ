using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public List<SelectorButton> allSelectables = new List<SelectorButton>();
    public HeroCreationMenu heroCreationMenu;
    public InGameMenu inGameMenu;

    public void SelectThisType(int index)
    {
        for(int i = 0; i < allSelectables.Count; i++)
        {
            allSelectables[i].checkmarkIcon.SetActive(i == index);
            allSelectables[i].selected = i == index;
        }
        if(heroCreationMenu)
            heroCreationMenu.UpdateSelection();
    }


    public void SelectThisType(int index, HeroItem item)
    {
        for (int i = 0; i < allSelectables.Count; i++)
        {
            allSelectables[i].checkmarkIcon.SetActive(i == index);
            allSelectables[i].selected = i == index;
        }
       
        if (inGameMenu)
            inGameMenu.EquipItem(item);
    }
    public void ResetSelector()
    {
        for (int i = 0; i < allSelectables.Count; i++)
        {
            allSelectables[i].checkmarkIcon.SetActive(false);
            allSelectables[i].selected = false;
        }
    }
}
