using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopKeeperUI : MonoBehaviour
{
    private Player player;
    public TMP_Text componentDetailTitleText;
    public TMP_Text redCoins;
    public Slider slider;
    public TMP_Text maxItemAmount;
    public TMP_Text chosenAmount;

    public TMP_Text goldenSpoonsText;
    public TMP_Text redCoinsText;
    public TMP_Text obsidianText;
    public TMP_Text coalText;
    public TMP_Text metalText;

    public Image itemIcon;
    public TMP_Text totalCost;
    public int price;

    private Item currentItem;

    public TMP_Text hpPotionStockAmount;
    public TMP_Text spPotionStockAmount;
    public TMP_Text mpPotionStockAmount;

    public List<Item> shopkeeperInventory;
    

    private void OnEnable()
    {
        updateShopKeeperUI();
    }

    public void Update(){
        chosenAmount.text = "x " + slider.value.ToString();
        totalCost.text = (slider.value * price).ToString();
    }

    public void updateShopKeeperUI()
    {
        findPlayer();

        goldenSpoonsText.text = player.goldenSpoonsAmount.ToString();
        redCoinsText.text = player.redCoinsAmount.ToString();
        obsidianText.text = player.obsidianAmount.ToString();
        coalText.text = player.coalAmount.ToString();
        metalText.text = player.metalAmount.ToString();

        for (int i = 0; i<shopkeeperInventory.Count; i++){
            if (i == 0){
                hpPotionStockAmount.text = "x " + shopkeeperInventory[i].amount.ToString();
            }
            if (i == 1){
                spPotionStockAmount.text = "x " + shopkeeperInventory[i].amount.ToString();
            }
            if (i == 2){
                mpPotionStockAmount.text = "x " + shopkeeperInventory[i].amount.ToString();
            }
        }
    }

     private void findPlayer()
    {
        GameObject playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<Player>();
    }

    public void showDetails(Item item){
        currentItem = item;
        componentDetailTitleText.text = item.itemName;
        maxItemAmount.text = item.amount.ToString();
        slider.maxValue = item.amount;
        slider.value = 0;   
        itemIcon.sprite = item.sprite.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void buyItems(){
        if (player.redCoinsAmount >= slider.value * price){
            currentItem.amount -= (int)slider.value;

            if (currentItem.itemName.Equals("HP Potion")){
                player.HPPotionAmount += (int)slider.value;
                hpPotionStockAmount.text = "x " + currentItem.amount.ToString();
            }
            if (currentItem.itemName.Equals("SP Potion")){
                player.SPPotionAmount += (int)slider.value;
                spPotionStockAmount.text = "x " + currentItem.amount.ToString();
            }
            if (currentItem.itemName.Equals("MP Potion")){
                player.MPPotionAmount += (int)slider.value;
                mpPotionStockAmount.text = "x " + currentItem.amount.ToString();
            }

            player.redCoinsAmount -= (int)slider.value * price;
            showDetails(currentItem);
            updateShopKeeperUI();
        }
    }
}
