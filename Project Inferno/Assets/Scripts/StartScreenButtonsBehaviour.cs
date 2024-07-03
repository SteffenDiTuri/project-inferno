using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtonsBehaviour : MonoBehaviour
{
    public void StartGameButtonAction(){
        SceneManager.LoadScene("HomeBase");
    }

    public void LoadSaveButtonAction(){
        Debug.Log("Load Save Button Clicked...");
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
