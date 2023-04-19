using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    public List<Hero> heroes = new List<Hero>();
    private GameManager gameManager;
    private string heroesSaveFileLocation;
    [HideInInspector]
    private void Start()
    {
        gameManager = GameManager.instance;
        heroesSaveFileLocation = Application.persistentDataPath + gameManager.HEROES_SAVE_FILE;
        gameManager.heroesManager = this;

        heroes = LoadHeroes();
    }

    /// <summary>
    /// Creates a new hero and saves it to available heroes list to choose from.
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_health"></param>
    /// <param name="_mana"></param>
    /// <param name="_endurance"></param>
    /// <param name="_intelligence"></param>
    /// <returns>Created hero object.</returns>
    public Hero CreateHero(string _name, int _health, int _mana, int _endurance, int _intelligence)
    {
        Hero hero = new Hero()
        {
            name = _name,
            health = _health,
            mana = _mana,
            endurance = _endurance,
            intelligence = _intelligence
        };
        heroes.Add(hero);

        return hero;
    }
    public Hero CreateHero(Hero _hero)
    {
        Hero hero = _hero;
        heroes.Add(hero);
        return hero;
    }
    public void SaveHeroes()
    {
        HeroesList heroesList = new HeroesList();
        heroesList.heroes = heroes;
        string heroesjsonString = JsonUtility.ToJson(heroesList);
        File.WriteAllText(heroesSaveFileLocation, heroesjsonString);
    }

    public List<Hero> LoadHeroes()
    {
        List<Hero> heroesTemp = new List<Hero>();
        if (File.Exists(heroesSaveFileLocation))
        {
            string heroesFileContents = File.ReadAllText(heroesSaveFileLocation);
            HeroesList heroesListTemp = JsonUtility.FromJson<HeroesList>(heroesFileContents);
            heroesTemp = heroesListTemp.heroes;
        }
        else
        {
            Debug.LogError("Heroes save file not found in " + heroesSaveFileLocation);
        }
        return heroesTemp;
    }

    /// <summary>
    /// Deletes a hero at an index
    /// </summary>
    /// <param name="indexAt"></param>
    /// <returns>Success?</returns>
    public bool DeleteHero(int indexAt)
    {
        try
        {
            heroes.RemoveAt(indexAt);
            SaveHeroes();
            LoadHeroes();
            return true;
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to delete hero. Error: " + e);
            return false;
        }
    }

    public Hero GetHeroFromType(HeroType heroType)
    {
        Hero hero = new Hero();
        if(heroType == HeroType.warrior)
        {
            hero.health = 200;
            hero.mana = 25;
            hero.endurance = 150;
            hero.intelligence = 25;
            hero.heroType = (int)HeroType.warrior;
        }
        else if (heroType == HeroType.mage)
        {
            hero.health = 50;
            hero.mana = 150;
            hero.endurance = 25;
            hero.intelligence = 100;
            hero.heroType = (int)HeroType.mage;
        }
        else if (heroType == HeroType.thief)
        {
            hero.health = 75;
            hero.mana = 75;
            hero.endurance = 75;
            hero.intelligence = 75;
            hero.heroType = (int)HeroType.thief;
        }
        else if (heroType == HeroType.bard)
        {
            hero.health = 100;
            hero.mana = 100;
            hero.endurance = 50;
            hero.intelligence = 50;
            hero.heroType = (int)HeroType.bard;
        }
        return hero;
    }

    public string GetHeroTypeName(HeroType heroType)
    {
        string typeName = "";
        if(heroType == HeroType.warrior)
        {
            typeName = "Warrior";
        }
        else if (heroType == HeroType.mage)
        {
            typeName = "Mage";
        }
        else if (heroType == HeroType.thief)
        {
            typeName = "Thief";
        }
        else if (heroType == HeroType.bard)
        {
            typeName = "Bard";
        }
        return typeName;
    }
    public Color GetHeroTypeColor(HeroType heroType)
    {
        Color typeName = Color.grey;
        if (heroType == HeroType.warrior)
        {
            typeName = Color.red;
        }
        else if (heroType == HeroType.mage)
        {
            typeName = Color.blue;
        }
        else if (heroType == HeroType.thief)
        {
            typeName = Color.yellow;
        }
        else if (heroType == HeroType.bard)
        {
            typeName = Color.green;
        }
        return typeName;
    }
}
public enum HeroType
{
    warrior = 0,
    mage = 1,
    thief = 2,
    bard = 3
}

[System.Serializable]
public class HeroesList
{
    public List<Hero> heroes = new List<Hero>();
}

[System.Serializable]
public class Hero
{
    public string name = string.Empty;
    public int health = 0;
    public int mana = 0;
    public int endurance = 0;
    public int intelligence = 0;
    public int heroType = 0;
}

public enum ItemType
{
    hat = 0,
    weapon = 1,
    token = 2
}

[System.Serializable]
public class HeroItem
{
    public string name = string.Empty;
    public int healthModifier = 0;
    public int manaModifier = 0;
    public int enduranceModifier = 0;
    public int intelligenceModifier = 0;
    public int itemType = 0;
}