using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionMenu : MonoBehaviour
{
    public GameObject heroShowcasePrefab;
    public Transform showcaseRoot;
    private List<Hero> heroes = new List<Hero>();
    private List<GameObject> allHeroShowcaseUIs = new List<GameObject>();
    public MenusManager menusManager;

    private void OnEnable()
    {
        LoadHeroesShowcase();
    }

    public void LoadHeroesShowcase()
    {
        for (int i = 0; i < allHeroShowcaseUIs.Count; i++)
        {
            Destroy(allHeroShowcaseUIs[i].gameObject);
        }
        allHeroShowcaseUIs = new List<GameObject>();
        HeroesManager heroesManager = GameManager.instance.heroesManager;
        try
        {
            heroes = heroesManager.LoadHeroes();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load heroes. Error: " + e);
        }

        for (int i = 0; i < heroes.Count; i++)
        {
            HeroShowcaseUI showcaseUI = Instantiate(heroShowcasePrefab, showcaseRoot).GetComponent<HeroShowcaseUI>();
            Hero retrievedHero = heroes[i];
            showcaseUI.nameText.text = retrievedHero.name;
            showcaseUI.typeText.text = heroesManager.GetHeroTypeName((HeroType)retrievedHero.heroType);
            showcaseUI.heroTypeBackground.color = heroesManager.GetHeroTypeColor((HeroType)retrievedHero.heroType);
            showcaseUI.healthText.text = "Health: " + retrievedHero.health;
            showcaseUI.manaText.text = "Mana: " + retrievedHero.mana;
            showcaseUI.endText.text = "Endurance: " + retrievedHero.endurance;
            showcaseUI.intText.text = "Intelligence: " + retrievedHero.intelligence;
            showcaseUI.heroIndex = i;
            showcaseUI.heroSelectionMenu = this;
            showcaseUI.selectButton.onClick.AddListener(() => menusManager.ChangeMenu(3));
            allHeroShowcaseUIs.Add(showcaseUI.gameObject);
        }
    }
}
