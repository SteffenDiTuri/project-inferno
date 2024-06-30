using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

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

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
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

        // enemyHUD.SetHP(enemy.currentHP);
        enemyHUD.SetHUD(enemy);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        // check if enemy is dead
        if (isDead){
            // end battle
            state = BattleState.WON;
            EndBattle();
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
            EndBattle();
        }
        else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle(){
        if (state == BattleState.WON){
            dialogueText.text = "You slayed them all!";
        }
        else if (state == BattleState.LOST){
            dialogueText.text = "You are dead.";
        }
    }

    void PlayerTurn(){
        dialogueText.text = player.characterName + ", choose your action...";
    }

    IEnumerator PlayerHeal(){
        player.Heal(10);
        playerHUD.SetHUD(player);

        dialogueText.text = "The gods lend you some vitality!";
        
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton(){
        if (state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerHeal());
    }
}
