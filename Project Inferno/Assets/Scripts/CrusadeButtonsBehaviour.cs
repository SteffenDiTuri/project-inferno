using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrusadeButtonsBehaviour : MonoBehaviour
{
    public GameObject CrusadeConfirmationScreen;
    public GameObject CrusadeSelectionScreen;
    private Player player;
    
    public void Start(){
        CrusadeConfirmationScreen.SetActive(false);
    }

    public void ShowConfirmationScreenCanvas(){
        CrusadeConfirmationScreen.SetActive(true);
    }

    public void CloseConfirmationScreenCanvas(){
        CrusadeConfirmationScreen.SetActive(false);
    }

    public void ShowSelectionScreenCanvas(){
        CrusadeSelectionScreen.SetActive(true);
    }

    public void CloseSelectionScreenCanvas(){
        CrusadeSelectionScreen.SetActive(false);
    }

    public void StartCrusadeButton(){
        findPlayer();
        player.currentHP = player.maxHP;
        player.currentSP = player.maxSP;
        player.currentMP = player.maxMP;
        SaveSystem.SavePlayer(player);
        SceneManager.LoadScene("DungeonStartScene");
    }

    public void ReturnToHomeBaseButton(){
        findPlayer();
        SaveSystem.SavePlayer(player);
        SceneManager.LoadScene("HomeBase");
    }

    private void findPlayer()
    {
        GameObject playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<Player>();
    }
}
