using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject sprite;
    public int dropRate;
    public string itemName;
    public int amount;

    public int generateAmount(int min, int max)
    {
        amount = Random.Range(min, max);
        return amount;
    }
}
