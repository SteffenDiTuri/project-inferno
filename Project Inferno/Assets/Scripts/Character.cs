using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
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

    public List<Item> inventory;
    public List<Item> allItems;
    
    public bool TakeDamage(int dmg){
        currentHP -= dmg;

        if(currentHP <= 0){
            return true;
        }
        else {
            return false;
        }
    }

    public bool ReduceEndurance(int amount){
        currentSP -= amount;

        if(currentSP <= 0){
            return true;
        }
        else {
            return false;
        }
    }

    public void RestoreHP(int amount){
        currentHP += amount;
        if (currentHP > maxHP){
            currentHP = maxHP;
        }
    }

    public void RestoreSP(int amount){
        currentSP += amount;
        if (currentSP > maxSP){
            currentSP = maxSP;
        }
    }

    public void RestoreMP(int amount){
        currentMP += amount;
        if (currentMP > maxMP){
            currentMP = maxMP;
        }
    }

    public void ResetHP(){
        currentHP = maxHP;
    }
}
