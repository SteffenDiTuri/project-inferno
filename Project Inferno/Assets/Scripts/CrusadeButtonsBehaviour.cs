using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrusadeButtonsBehaviour : MonoBehaviour
{
    public GameObject CrusadeConfirmationScreen;
    public GameObject CrusadeSelectionScreen;
    
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

        SceneManager.LoadScene("DungeonStartScene");
    }

    public void ReturnToHomeBaseButton(){

        SceneManager.LoadScene("HomeBase");
    }

}
