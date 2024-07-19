using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int XP;

    public void generateInventory()
    {
        foreach(Item item in allItems)
        {
            Item newItem = Instantiate<Item>(item);
            newItem.transform.position = newItem.transform.position + new Vector3(0, 0, 5);
            newItem.generateAmount(1,5);
            Debug.Log(newItem.itemName);
            Debug.Log(newItem.amount);
            inventory.Add(newItem);
        }
    }
}
