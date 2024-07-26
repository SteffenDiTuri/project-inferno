using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtonsBehaviour : MonoBehaviour
{
    public void StartGameButtonAction(){
        Player player = gameObject.AddComponent<Player>();
        player.characterName = "Jos";
        player.characterLevel = 0;
        player.characterDamage = 10;
        player.maxHP = 25;
        player.maxMP = 25;
        player.maxSP = 25;
        player.currentHP = 25;
        player.currentMP = 25;
        player.currentSP = 25;
        player.weaponLevel = 0;
        SaveSystem.SavePlayer(player);
        SceneManager.LoadScene("HomeBase");
    }

    public void LoadSaveButtonAction(){
        SceneManager.LoadScene("HomeBase");
        SaveSystem.LoadPlayer();
    }

    public void SettingsButtonAction(){
        Debug.Log("Settings Button Clicked...");
    }

    public void ExitButtonAction(){
        Application.Quit();

        #if UNITY_EDITOR
        // This line is to stop play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
