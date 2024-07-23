using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlacksmithUI : MonoBehaviour
{
    public TMP_Text upgradeOneTitleText;
    public TMP_Text upgradeOneLevelText;

    public TMP_Text upgradeTwoTitleText;
    public TMP_Text upgradeTwoLevelText;

    public TMP_Text upgradeThreeTitleText;
    public TMP_Text upgradeThreeLevelText;

    public TMP_Text upgradeFourTitleText;
    public TMP_Text upgradeFourLevelText;

    public TMP_Text upgradeFiveTitleText;
    public TMP_Text upgradeFiveLevelText;

    public TMP_Text upgradeSixTitleText;
    public TMP_Text upgradeSixLevelText;

    public TMP_Text upgradeSevenTitleText;
    public TMP_Text upgradeSevenLevelText;

    public TMP_Text upgradeEightTitleText;
    public TMP_Text upgradeEightLevelText;

    public TMP_Text upgradeNineTitleText;
    public TMP_Text upgradeNineLevelText;
    

    private Player player;
    private Blacksmith blacksmith;

    public List<Item> allItems;
    public GameObject lootItem;

    public TMP_Text componentDetailTitleText;

    public TMP_Text redCoins;

    private WeaponUpgrade currentUpgrade;

    private void Start()
    {
        updateBlacksmithUI();
    }

    public void updateBlacksmithUI()
    {
        findPlayer();
        findBlacksmith();

        upgradeOneTitleText.text = blacksmith.weaponUpgrades[0].title;
        upgradeOneLevelText.text = blacksmith.weaponUpgrades[0].upgradeLevel.ToString();

        upgradeTwoTitleText.text = blacksmith.weaponUpgrades[1].title;
        upgradeTwoLevelText.text = blacksmith.weaponUpgrades[1].upgradeLevel.ToString();

        upgradeThreeTitleText.text = blacksmith.weaponUpgrades[2].title;
        upgradeThreeLevelText.text = blacksmith.weaponUpgrades[2].upgradeLevel.ToString();

        upgradeFourTitleText.text = blacksmith.weaponUpgrades[3].title;
        upgradeFourLevelText.text = blacksmith.weaponUpgrades[3].upgradeLevel.ToString();

        upgradeFiveTitleText.text = blacksmith.weaponUpgrades[4].title;
        upgradeFiveLevelText.text = blacksmith.weaponUpgrades[4].upgradeLevel.ToString();

        upgradeSixTitleText.text = blacksmith.weaponUpgrades[5].title;
        upgradeSixLevelText.text = blacksmith.weaponUpgrades[5].upgradeLevel.ToString();

        upgradeSevenTitleText.text = blacksmith.weaponUpgrades[6].title;
        upgradeSevenLevelText.text = blacksmith.weaponUpgrades[6].upgradeLevel.ToString();

        upgradeEightTitleText.text = blacksmith.weaponUpgrades[7].title;
        upgradeEightLevelText.text = blacksmith.weaponUpgrades[7].upgradeLevel.ToString();

        upgradeNineTitleText.text = blacksmith.weaponUpgrades[8].title;
        upgradeNineLevelText.text = blacksmith.weaponUpgrades[8].upgradeLevel.ToString();
    }

    private void findPlayer()
    {
        GameObject playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<Player>();
    }

    private void findBlacksmith()
    {
        GameObject blacksmithGO = GameObject.Find("BlackSmithArea");
        blacksmith = blacksmithGO.GetComponent<Blacksmith>();
    }

    public void showCost(WeaponUpgrade upgrade)
    {
        currentUpgrade = upgrade;
        Transform basement = this.transform.Find("ComponentDetail").Find("MaterialCost").Find("Items");
        foreach(Transform child in basement)
        {
            Destroy(child.gameObject);
        }

        componentDetailTitleText.text = upgrade.title;
        float y = 0;
        foreach (Item item in allItems)
        {
            if (item.itemName.Equals("Golden Spoon") && upgrade.goldenSpoons > 0)
            {
                GameObject itemInstance = Instantiate(lootItem, this.transform.Find("ComponentDetail").Find("MaterialCost").Find("Items"));
                itemInstance.transform.position = itemInstance.transform.position + new Vector3(0, y, 0);
                itemInstance.GetComponentInChildren<Image>().sprite = item.sprite.GetComponent<SpriteRenderer>().sprite;
                itemInstance.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + " " + upgrade.goldenSpoons.ToString();
                itemInstance.transform.localScale = new Vector3(2, 2, 2);
                y -= 45;
            }
            else if (item.itemName.Equals("Obsidian") && upgrade.obsidian > 0)
            {
                GameObject itemInstance = Instantiate(lootItem, this.transform.Find("ComponentDetail").Find("MaterialCost").Find("Items"));
                itemInstance.transform.position = itemInstance.transform.position + new Vector3(0, y, 0);
                itemInstance.GetComponentInChildren<Image>().sprite = item.sprite.GetComponent<SpriteRenderer>().sprite;
                itemInstance.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + " " + upgrade.obsidian.ToString();
                itemInstance.transform.localScale = new Vector3(2, 2, 2);
                y -= 45;
            }
            else if (item.itemName.Equals("Coal") && upgrade.coal > 0)
            {
                GameObject itemInstance = Instantiate(lootItem, this.transform.Find("ComponentDetail").Find("MaterialCost").Find("Items"));
                itemInstance.transform.position = itemInstance.transform.position + new Vector3(0, y, 0);
                itemInstance.GetComponentInChildren<Image>().sprite = item.sprite.GetComponent<SpriteRenderer>().sprite;
                itemInstance.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + " " + upgrade.coal.ToString();
                itemInstance.transform.localScale = new Vector3(2, 2, 2);
                y -= 45;
            }
            else if (item.itemName.Equals("Metal") && upgrade.metal > 0)
            {
                GameObject itemInstance = Instantiate(lootItem, this.transform.Find("ComponentDetail").Find("MaterialCost").Find("Items"));
                itemInstance.transform.position = itemInstance.transform.position + new Vector3(0, y, 0);
                itemInstance.GetComponentInChildren<Image>().sprite = item.sprite.GetComponent<SpriteRenderer>().sprite;
                itemInstance.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + " " + upgrade.metal.ToString();
                itemInstance.transform.localScale = new Vector3(2, 2, 2);
                y -= 45;
            }
            else if (item.itemName.Equals("Red Coins"))
            {
                redCoins.text = upgrade.redCoins.ToString();
            }
        }
    }

    public void buyUpgrade()
    {
        Debug.Log("clicked");
        if (!currentUpgrade.unlocked && player.goldenSpoonsAmount >= currentUpgrade.goldenSpoons && player.redCoinsAmount >= currentUpgrade.redCoins && player.obsidianAmount >= currentUpgrade.obsidian && player.coalAmount >= currentUpgrade.coal && player.metalAmount >= currentUpgrade.metal)
        {
            player.goldenSpoonsAmount -= currentUpgrade.goldenSpoons;
            player.redCoinsAmount -= currentUpgrade.redCoins;
            player.obsidianAmount -= currentUpgrade.obsidian;
            player.coalAmount -= currentUpgrade.coal;
            player.metalAmount -= currentUpgrade.metal;
            currentUpgrade.unlocked = true;
            player.weaponLevel++;
        }
    }
}
