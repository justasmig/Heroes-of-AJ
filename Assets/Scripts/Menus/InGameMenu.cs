using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public Transform hatsSelectionRoot;
    public Selector hatsSelector;

    public Transform weaponsSelectionRoot;
    public Selector weaponsSelector;

    public Transform tokensSelectionRoot;
    public Selector tokensSelector;

    public GameObject itemSelectorButtonPrefab;

    public HeroShowcaseUI heroShowcase;

    private void OnEnable()
    {
        LoadItemsFromDatabase();
    }

    public void LoadItemsFromDatabase()
    {
        for(int i = 0; i < hatsSelector.allSelectables.Count; i++)
        {
            Destroy(hatsSelector.allSelectables[i].gameObject);
        }
        hatsSelector.allSelectables = new List<SelectorButton>();
        
        for (int i = 0; i < weaponsSelector.allSelectables.Count; i++)
        {
            Destroy(weaponsSelector.allSelectables[i].gameObject);
        }
        weaponsSelector.allSelectables = new List<SelectorButton>();
        
        for (int i = 0; i < tokensSelector.allSelectables.Count; i++)
        {
            Destroy(tokensSelector.allSelectables[i].gameObject);
        }
        tokensSelector.allSelectables = new List<SelectorButton>();

        for(int i = 0; i < GameManager.instance.itemsDatabase.Count; i++)
        {
            HeroItem retrievedItem = GameManager.instance.itemsDatabase[i];
            Transform itemButton = Instantiate(itemSelectorButtonPrefab).transform;
            if(retrievedItem.itemType == (int)ItemType.hat)
            {
                itemButton.SetParent(hatsSelectionRoot);
                hatsSelector.allSelectables.Add(itemButton.GetComponent<SelectorButton>());
                itemButton.GetComponent<SelectorButton>().uiButton.onClick.RemoveAllListeners();
                int selectorIndex = hatsSelector.allSelectables.Count - 1;
                itemButton.GetComponent<SelectorButton>().uiButton.onClick.AddListener(()=>hatsSelector.SelectThisType(selectorIndex, retrievedItem));
            }
            else if(retrievedItem.itemType == (int)ItemType.weapon)
            {
                itemButton.SetParent(weaponsSelectionRoot);
                weaponsSelector.allSelectables.Add(itemButton.GetComponent<SelectorButton>());
                itemButton.GetComponent<SelectorButton>().uiButton.onClick.RemoveAllListeners();
                int selectorIndex = weaponsSelector.allSelectables.Count - 1;
                itemButton.GetComponent<SelectorButton>().uiButton.onClick.AddListener(() => weaponsSelector.SelectThisType(selectorIndex, retrievedItem));
            }
            else if (retrievedItem.itemType == (int)ItemType.token)
            {
                itemButton.SetParent(tokensSelectionRoot);
                tokensSelector.allSelectables.Add(itemButton.GetComponent<SelectorButton>());
                itemButton.GetComponent<SelectorButton>().uiButton.onClick.RemoveAllListeners();
                int selectorIndex = tokensSelector.allSelectables.Count - 1;
                itemButton.GetComponent<SelectorButton>().uiButton.onClick.AddListener(() => tokensSelector.SelectThisType(selectorIndex, retrievedItem));
            }
            ItemShowcaseUI itemShowcaseUI = itemButton.GetComponent<ItemShowcaseUI>();
            itemShowcaseUI.item = retrievedItem;
            itemShowcaseUI.UpdateText();
        }
        SelectorButton unequipHatButton = Instantiate(itemSelectorButtonPrefab, hatsSelectionRoot).GetComponent<SelectorButton>();
        hatsSelector.allSelectables.Add(unequipHatButton);
        HeroItem unequipHatItem = new HeroItem();
        unequipHatItem.itemType = (int)ItemType.hat;
        unequipHatButton.uiButton.onClick.RemoveAllListeners();
        unequipHatButton.uiButton.onClick.AddListener(() => hatsSelector.SelectThisType(hatsSelector.allSelectables.Count - 1, unequipHatItem));
        unequipHatButton.GetComponent<ItemShowcaseUI>().ShowUnequip(ItemType.hat);

        SelectorButton unequipWeaponButton = Instantiate(itemSelectorButtonPrefab, weaponsSelectionRoot).GetComponent<SelectorButton>();
        weaponsSelector.allSelectables.Add(unequipWeaponButton);
        HeroItem unequipWeaponItem = new HeroItem();
        unequipWeaponItem.itemType = (int)ItemType.weapon;
        unequipWeaponButton.uiButton.onClick.RemoveAllListeners();
        unequipWeaponButton.uiButton.onClick.AddListener(() => weaponsSelector.SelectThisType(weaponsSelector.allSelectables.Count - 1, unequipWeaponItem));
        unequipWeaponButton.GetComponent<ItemShowcaseUI>().ShowUnequip(ItemType.weapon);

        SelectorButton unequipTokenButton = Instantiate(itemSelectorButtonPrefab, tokensSelectionRoot).GetComponent<SelectorButton>();
        tokensSelector.allSelectables.Add(unequipTokenButton);
        HeroItem unequipTokenItem = new HeroItem();
        unequipTokenItem.itemType = (int)ItemType.token;
        unequipTokenButton.uiButton.onClick.RemoveAllListeners();
        unequipTokenButton.uiButton.onClick.AddListener(() => tokensSelector.SelectThisType(tokensSelector.allSelectables.Count - 1, unequipTokenItem));
        unequipTokenButton.GetComponent<ItemShowcaseUI>().ShowUnequip(ItemType.token);
        UpdateHeroStats();
    }

    public void EquipItem(HeroItem item)
    {
        if(item.itemType == (int)ItemType.hat)
        {
            GameManager.instance.currentHat = item;
        }
        else if (item.itemType == (int)ItemType.weapon)
        {
            GameManager.instance.currentWeapon = item;
        }
        else if (item.itemType == (int)ItemType.token)
        {
            GameManager.instance.currentToken = item;
        }
        GameManager.instance.RecalculateHeroStats();
        UpdateHeroStats();
    }

    private void UpdateHeroStats()
    {
        GameManager gameManager = GameManager.instance;
        heroShowcase.nameText.text = gameManager.currentHeroVanilla.name;
        heroShowcase.typeText.text = gameManager.heroesManager.GetHeroTypeName((HeroType)gameManager.currentHeroVanilla.heroType);
        heroShowcase.heroTypeBackground.color = gameManager.heroesManager.GetHeroTypeColor((HeroType)gameManager.currentHeroVanilla.heroType);
        int healthModifier = gameManager.currentHeroModified.health - gameManager.currentHeroVanilla.health;
        if (healthModifier > 0)        
            heroShowcase.healthText.text = "Health: " + gameManager.currentHeroModified.health + " (+" + healthModifier + ")";        
        else
            heroShowcase.healthText.text = "Health: " + gameManager.currentHeroModified.health + " (" + healthModifier + ")";

        int manaModifier = gameManager.currentHeroModified.mana - gameManager.currentHeroVanilla.mana;
        if (manaModifier > 0)
            heroShowcase.manaText.text = "Mana: " + gameManager.currentHeroModified.mana + " (+" + manaModifier + ")";
        else
            heroShowcase.manaText.text = "Mana: " + gameManager.currentHeroModified.mana + " (" + manaModifier + ")";

        int enduranceModifier = gameManager.currentHeroModified.endurance - gameManager.currentHeroVanilla.endurance;
        if (manaModifier > 0)
            heroShowcase.endText.text = "Endurance: " + gameManager.currentHeroModified.endurance + " (+" + enduranceModifier + ")";
        else
            heroShowcase.endText.text = "Endurance: " + gameManager.currentHeroModified.endurance + " (" + enduranceModifier + ")";

        int intelligenceModifier = gameManager.currentHeroModified.intelligence - gameManager.currentHeroVanilla.intelligence;
        if (intelligenceModifier > 0)
            heroShowcase.intText.text = "Intelligence: " + gameManager.currentHeroModified.intelligence + " (+" + intelligenceModifier + ")";
        else
            heroShowcase.intText.text = "Intelligence: " + gameManager.currentHeroModified.intelligence + " (" + intelligenceModifier + ")";
    }
}
