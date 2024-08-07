using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public bool inCombat;
    public bool inDungeon;

    public int goldenSpoonsAmount;
    public int redCoinsAmount;
    public int obsidianAmount;
    public int coalAmount;
    public int metalAmount;
    public int HPPotionAmount;
    public int MPPotionAmount;
    public int SPPotionAmount;

    public int weaponLevel;

    void Start(){
        LoadPlayer();
    }

    public bool IsInCombat()
    {
        return inCombat;
    }

    public bool IsInDungeon()
    {
        return inDungeon;
    }

    public void SetCombatState(bool state)
    {
        inCombat = state;
    }

    public void SetDungeonState(bool state)
    {
        inDungeon = state;
    }

    // save and load
    public void SavePlayer()
    {
        // Debug.Log("SavePlayer Method Called");
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        // Debug.Log("LoadPlayer Method Called");
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            // super class
            characterName = data.characterName;
            characterLevel = data.characterLevel;
            characterDamage = data.characterDamage;
            maxHP = data.maxHP;
            currentHP = data.currentHP;
            maxSP = data.maxSP;
            currentSP = data.currentSP;
            maxMP = data.maxMP;
            currentMP = data.currentMP;

            goldenSpoonsAmount = data.goldenSpoonsAmount;
            redCoinsAmount = data.redCoinsAmount;
            obsidianAmount = data.obsidianAmount;
            coalAmount = data.coalAmount;
            metalAmount = data.metalAmount;
            HPPotionAmount = data.HPPotionAmount;
            MPPotionAmount = data.MPPotionAmount;
            SPPotionAmount = data.SPPotionAmount;

            weaponLevel = data.weaponLevel;
        }
        else
        {
            Debug.LogError("Failed to load player data into player object.");
        }
    }

    public void ExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        // This line is to stop play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
