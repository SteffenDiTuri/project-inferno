using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text goldenSpoon;
    public TMP_Text redCoins;
    public TMP_Text obsidian;
    public TMP_Text coal;
    public TMP_Text metal;
    public TMP_Text HPPotion;
    public TMP_Text MPPotion;
    public TMP_Text SPPotion;

    private Player player;

    private void Start()
    {
        updateInventoryUI();
    }

    public void updateInventoryUI()
    {
        findPlayer();
        goldenSpoon.text = player.goldenSpoonsAmount.ToString();
        redCoins.text = player.redCoinsAmount.ToString();
        obsidian.text = player.obsidianAmount.ToString();
        coal.text = player.coalAmount.ToString();
        metal.text = player.metalAmount.ToString();
        HPPotion.text = player.HPPotionAmount.ToString();
        MPPotion.text = player.MPPotionAmount.ToString();
        SPPotion.text = player.SPPotionAmount.ToString();
    }

    private void findPlayer()
    {
        GameObject playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<Player>();
    }
}
