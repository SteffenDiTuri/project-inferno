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

    public void Begin()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        deathScreen.SetActive(false);
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


        dialogueText.text = "Commence the Battle!";

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN; // assuming the player starts the battle
        PlayerTurn();
    }

    IEnumerator PlayerAttack(){
        // damage enemy
        bool isDead = enemy.TakeDamage(player.characterDamage);

        // enemyHUD.SetHP(enemy.currentHP);
        enemyHUD.SetHUD(enemy);
        dialogueText.text = "The attack is successful!";

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

        // playerHUD.SetHP(player.currentHP);
        playerHUD.SetHUD(player);

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
            dialogueText.text = "You slayed them all!";
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
        else if (state == BattleState.LOST){
            dialogueText.text = "You are dead.";
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

        dialogueText.text = "The gods lend you some vitality!";
        
        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerRestoreSP(){
        player.RestoreSP(10);
        playerHUD.SetHUD(player);

        dialogueText.text = "The gods lend you some endurance!";
        
        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerRestoreMP(){
        player.RestoreMP(10);
        playerHUD.SetHUD(player);

        dialogueText.text = "The gods lend you some spirit!";
        
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
}
