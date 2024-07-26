using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlacksmithCanvasInitializer : MonoBehaviour
{
    public GameObject BlacksmithConfirmationScreen;
    
    public void Start(){
        BlacksmithConfirmationScreen.SetActive(false);
    }

    public void ShowConfirmationScreenCanvas(){
        BlacksmithConfirmationScreen.SetActive(true);
    }

    public void CloseConfirmationScreenCanvas(){
        BlacksmithConfirmationScreen.SetActive(false);
    }
}
