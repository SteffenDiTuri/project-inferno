using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string characterName;
    public int characterLevel;

    public int characterDamage;
    
    public int maxHP;
    public int currentHP;
    public int maxSP;
    public int currentSP;
    public int maxMP;
    public int currentMP;

    // public float[] position;

    public PlayerData (Player player){
        characterName = player.characterName;
        characterLevel = player.characterLevel;
        characterDamage = player.characterDamage;
        maxHP = player.maxHP;
        currentHP = player.currentHP;
        maxSP = player.maxSP;
        currentSP = player.currentSP;
        maxMP = player.maxMP;
        currentMP = player.currentMP;

        // position = new float[3];
        // position[0] = player.transform.position.x;
        // position[1] = player.transform.position.y;
        // position[2] = player.transform.position.z;
    }
}
