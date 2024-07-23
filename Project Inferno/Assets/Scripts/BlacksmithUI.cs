using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}
