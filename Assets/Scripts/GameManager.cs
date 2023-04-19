using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HeroesManager heroesManager;
    public string HEROES_SAVE_FILE = "/allHeroes.json";
    public List<HeroItem> itemsDatabase = new List<HeroItem>();

    public Hero currentHeroVanilla;
    public Hero currentHeroModified;
    public HeroItem currentHat;
    public HeroItem currentWeapon;
    public HeroItem currentToken;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SelectHero(int indexOfHero)
    {
        Hero selectedHero = heroesManager.heroes[indexOfHero];
        currentHeroVanilla = new Hero() {
            name = selectedHero.name,
            health = selectedHero.health,
            mana = selectedHero.mana,
            endurance = selectedHero.endurance,
            intelligence = selectedHero.intelligence,
            heroType = selectedHero.heroType
        };
        currentHeroModified = new Hero()
        {
            name = selectedHero.name,
            health = selectedHero.health,
            mana = selectedHero.mana,
            endurance = selectedHero.endurance,
            intelligence = selectedHero.intelligence,
            heroType = selectedHero.heroType
        };
    }

    public void RecalculateHeroStats()
    {
        int healthModTotal = 0;
        int manaModTotal = 0;
        int endModTotal = 0;
        int intModTotal = 0;
        if(currentHat != null)
        {
            healthModTotal += currentHat.healthModifier;
            manaModTotal += currentHat.manaModifier;
            endModTotal += currentHat.enduranceModifier;
            intModTotal += currentHat.intelligenceModifier;
        }
        if (currentWeapon != null)
        {
            healthModTotal += currentWeapon.healthModifier;
            manaModTotal += currentWeapon.manaModifier;
            endModTotal += currentWeapon.enduranceModifier;
            intModTotal += currentWeapon.intelligenceModifier;
        }
        if (currentToken != null)
        {
            healthModTotal += currentToken.healthModifier;
            manaModTotal += currentToken.manaModifier;
            endModTotal += currentToken.enduranceModifier;
            intModTotal += currentToken.intelligenceModifier;
        }
        currentHeroModified = new Hero()
        {
            name = currentHeroVanilla.name,
            health = currentHeroVanilla.health,
            mana = currentHeroVanilla.mana,
            endurance = currentHeroVanilla.endurance,
            intelligence = currentHeroVanilla.intelligence,
            heroType = currentHeroVanilla.heroType
        }; ;
        currentHeroModified.health += healthModTotal;
        currentHeroModified.mana += manaModTotal;
        currentHeroModified.endurance += endModTotal;
        currentHeroModified.intelligence += intModTotal;
    }

    public void ResetGame()
    {
        currentHeroVanilla = null;
        currentHeroModified = null;
    }
}
