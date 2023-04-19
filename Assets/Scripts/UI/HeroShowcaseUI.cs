using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroShowcaseUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text typeText;
    public UnityEngine.UI.Image heroTypeBackground;
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text endText;
    public TMP_Text intText;
    public int heroIndex;
    public HeroSelectionMenu heroSelectionMenu;
    public UnityEngine.UI.Button selectButton;

    public void DeleteHero()
    {
        GameManager.instance.heroesManager.DeleteHero(heroIndex);
        heroSelectionMenu.LoadHeroesShowcase();
    }
    public void SelectHero()
    {
        GameManager.instance.SelectHero(heroIndex);
    }
}
