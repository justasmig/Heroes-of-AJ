using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HeroCreationMenu : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text endText;
    public TMP_Text intText;
    public Selector typeSelector;
    public Hero currentHeroStats;
    public GameObject nameTooShortWarning;
    public GameObject heroTypeMissingWarning;
    private bool selectedHeroTypeOnce = false;

    public void UpdateSelection()
    {

        int indexOfCurrentSelection = typeSelector.allSelectables.FindIndex(x => x.selected);
        currentHeroStats = GameManager.instance.heroesManager.GetHeroFromType((HeroType)indexOfCurrentSelection);
        healthText.text = "Health: " + currentHeroStats.health;
        manaText.text = "Mana: " + currentHeroStats.mana;
        endText.text = "Endurance: " + currentHeroStats.endurance;
        intText.text = "Intelligence: " + currentHeroStats.intelligence;
        selectedHeroTypeOnce = true;
        heroTypeMissingWarning.SetActive(false);
    }

    public void CreateHero()
    {
        if (selectedHeroTypeOnce == false)
        {
            heroTypeMissingWarning.SetActive(true);
            return;
        }
        if(nameInput.text.Length < 1)
        {
            nameTooShortWarning.SetActive(true);
            return;
        }
        nameTooShortWarning.SetActive(false);
        currentHeroStats.name = nameInput.text;
        GameManager.instance.heroesManager.CreateHero(currentHeroStats);
        GameManager.instance.heroesManager.SaveHeroes();
        typeSelector.ResetSelector();
        nameInput.text = "";
    }    
}
