using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text hpText;
    public TMP_Text levelText;

    public void SetHUD(Character character)
    {
        if (character == null)
        {
            Debug.LogError("Character is null");
            return;
        }

        if (nameText != null)
        {
            nameText.text = character.characterName != null ? character.characterName : "Unknown";
        }

        if (hpText != null)
        {
            hpText.text = character.currentHP.ToString() + " HP";
        }

        if (levelText != null)
        {
            levelText.text = "Lvl. " + character.characterLevel.ToString();
        }

        // if (hpSlider != null)
        // {
        //     hpSlider.maxValue = character.maxHP;
        //     hpSlider.value = character.currentHP;
        // }
    }
}
