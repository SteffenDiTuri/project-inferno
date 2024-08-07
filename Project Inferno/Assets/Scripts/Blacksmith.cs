using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    public List<WeaponUpgrade> weaponUpgrades;

    public void buyUpgrade(int upgrade)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        WeaponUpgrade weaponUpgrade = weaponUpgrades[upgrade];
        if (player.goldenSpoonsAmount >= weaponUpgrade.goldenSpoons && player.redCoinsAmount >= weaponUpgrade.redCoins && player.obsidianAmount >= weaponUpgrade.obsidian && player.coalAmount >= weaponUpgrade.coal && player.metalAmount >= weaponUpgrade.metal)
        {
            player.goldenSpoonsAmount -= weaponUpgrade.goldenSpoons;
            player.redCoinsAmount -= weaponUpgrade.redCoins;
            player.obsidianAmount -= weaponUpgrade.obsidian;
            player.coalAmount -= weaponUpgrade.coal;
            player.metalAmount -= weaponUpgrade.metal;
            weaponUpgrade.buyable = true;
            player.weaponLevel ++;
        }
    }
}
