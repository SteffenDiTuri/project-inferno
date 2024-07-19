using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, SURRENDERED }

public class BattleSystem : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    public GameObject enemyPrefab;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public TMP_Text dialogueText;

    public BattleState state;

    public GameObject deathScreen;
    public GameObject victoryScreen;

    public GameObject lootItem;

    public void Begin()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }

    IEnumerator SetupBattle() {
        // find player in scene
        GameObject playerGO = GameObject.Find("Player");
        if (playerGO == null)
        {
            Debug.LogError("Player GameObject not found! Make sure it is named 'Player'.");
        }
        else
        {
            player = playerGO.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player component not found on the Player GameObject.");
            }
            player.SetCombatState(true);
        }

        // place enemy (enemies) on wright place in scene
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation); // 1 enemy spawned at the moment
        enemy = enemyGO.GetComponent<Enemy>();
        //enemy.ResetHP();
        enemy.generateInventory();

        dialogueText.text = "commence the battle!";

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN; // assuming the player starts the battle
        PlayerTurn();
    }

    IEnumerator PlayerAttack(){
        // damage enemy
        bool isDead = enemy.TakeDamage(player.characterDamage);
        player.ReduceEndurance(5);

        // enemyHUD.SetHP(enemy.currentHP);
        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);
        dialogueText.text = "the attack is successful!";

        yield return new WaitForSeconds(2f);

        // check if enemy is dead
        if (isDead){
            // end battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else {
            // enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn(){
        // enemy logic for attacking (AI)

        dialogueText.text = enemy.characterName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = player.TakeDamage(enemy.characterDamage);
        enemy.ReduceEndurance(5);

        // playerHUD.SetHP(player.currentHP);
        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(1f);

        if (isDead){
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    
    IEnumerator EndBattle(){
        if (state == BattleState.WON){
            dialogueText.text = "you slayed them all!";
            yield return new WaitForSeconds(3f);
            victoryScreen.SetActive(true);
            showLoot();
        }
        else if (state == BattleState.LOST){
            dialogueText.text = "you are dead.";
            yield return new WaitForSeconds(3f);
            deathScreen.SetActive(true);
        }
        else if (state == BattleState.SURRENDERED){
            dialogueText.text = "you cowered away.";
            yield return new WaitForSeconds(2f);
            // Instead of loading a new scene, call the EndBattle method in PlayerMovement
            GameObject playerGO = GameObject.Find("Player");
            if (playerGO != null)
            {
                PlayerMovement playerMovement = playerGO.GetComponent<PlayerMovement>();
                FindEnemy();
                playerMovement.EndBattle();
                player.SetCombatState(false);
            }
        }
    }

    void FindEnemy(){
        GameObject enemyGO = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyGO == null)
        {
            Debug.LogError("Enemy GameObject not found! Make sure it is named 'Enemy'.");
        }
        else
        {
            Destroy(enemyGO);
        }

    }

    void PlayerTurn(){
        dialogueText.text = player.characterName + ", choose your action...";
    }

    IEnumerator PlayerRestoreHP(){
        player.RestoreHP(10);
        playerHUD.SetHUD(player);

        dialogueText.text = "the gods lend you some vitality!";
        
        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerRestoreSP(){
        player.RestoreSP(10);
        playerHUD.SetHUD(player);

        dialogueText.text = "the gods lend you some endurance!";
        
        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerRestoreMP(){
        player.RestoreMP(10);
        playerHUD.SetHUD(player);

        dialogueText.text = "the gods lend you some spirit!";
        
        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnRestoreHPButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerRestoreHP());
    }

    public void OnRestoreSPButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerRestoreSP());
    }

    public void OnRestoreMPButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerRestoreMP());
    }

    public void OnSurrenderButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        state = BattleState.SURRENDERED;
        StartCoroutine(EndBattle());
    }

    // game over screen methods
    public void RestartCrusadeButton(){

        SceneManager.LoadScene("DungeonStartScene");
    }

    public void ReturnToHomeBaseButton(){

        SceneManager.LoadScene("HomeBase");
    }

    // victory screen methods
    public void ClaimSpoils(){
        // give player rewards
        foreach(Item item in enemy.inventory)
        {
            switch(item.itemName)
            {
                case "Golden Spoon":
                    player.goldenSpoonsAmount += item.amount;
                    break;
                case "Red Coins":
                    player.redCoinsAmount += item.amount;
                    break;
                case "Obsidian":
                    player.obsidianAmount += item.amount;
                    break;
                case "Coal":
                    player.coalAmount += item.amount;
                    break;
                case "Metal":
                    player.metalAmount += item.amount;
                    break;
                case "HP Potion":
                    player.HPPotionAmount += item.amount;
                    break;
                case "MP Potion":
                    player.MPPotionAmount += item.amount;
                    break;
                case "SP Potion":
                    player.SPPotionAmount += item.amount;
                    break;
            }
            Destroy(item);
        }

        // Instead of loading a new scene, call the EndBattle method in PlayerMovement
            GameObject playerGO = GameObject.Find("Player");
            if (playerGO != null)
            {
                PlayerMovement playerMovement = playerGO.GetComponent<PlayerMovement>();
                FindEnemy();
                playerMovement.EndBattle();
                player.SetCombatState(false);
            }
    }

    public void showLoot()
    {
        float y = 0;
        foreach (Item item in enemy.inventory)
        {
            GameObject itemInstance = Instantiate(lootItem, victoryScreen.transform.Find("StartCrusadeElement").Find("Image").Find("Loot"));
            itemInstance.transform.position = itemInstance.transform.position + new Vector3(0, y, 0);
            itemInstance.GetComponentInChildren<Image>().sprite = item.sprite.GetComponent<SpriteRenderer>().sprite;
            itemInstance.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + " " + item.amount.ToString();
            y -= 45;
        }
    }
}
