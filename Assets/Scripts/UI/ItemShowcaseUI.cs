using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemShowcaseUI : MonoBehaviour
{
    public HeroItem item;
    public TMP_Text itemNameText;
    public TMP_Text healthModText;
    public TMP_Text manaModText;
    public TMP_Text endModText;
    public TMP_Text intModText;

    public void UpdateText()
    {
        itemNameText.text = item.name;
        if(item.healthModifier > 0)
            healthModText.text = "+" + item.healthModifier.ToString();
        else
            healthModText.text = item.healthModifier.ToString();

        if(item.manaModifier > 0)
            manaModText.text = "+" + item.manaModifier.ToString();
        else
            manaModText.text = item.manaModifier.ToString();

        if(item.enduranceModifier > 0) 
            endModText.text = "+" + item.enduranceModifier.ToString();
        else
            endModText.text = item.enduranceModifier.ToString();

        if(item.intelligenceModifier > 0)
            intModText.text = "+" + item.intelligenceModifier.ToString();
        else
            intModText.text = item.intelligenceModifier.ToString();
    }

    public void ShowUnequip(ItemType itemType)
    {
        item = new HeroItem();
        item.itemType = (int)itemType;
        itemNameText.text = "Unequip";       
        healthModText.text = "";        
        manaModText.text = "";      
        endModText.text = "";
        intModText.text = "";
    }
}
