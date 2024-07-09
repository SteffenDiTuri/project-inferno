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
    public int MP;
    public int SP;

    // public float[] position;

    public PlayerData (Player player){
        characterName = player.characterName;
        characterLevel = player.characterLevel;
        characterDamage = player.characterDamage;
        maxHP = player.maxHP;
        currentHP = player.currentHP;
        MP = player.MP;
        SP = player.SP;

        // position = new float[3];
        // position[0] = player.transform.position.x;
        // position[1] = player.transform.position.y;
        // position[2] = player.transform.position.z;
    }
}
