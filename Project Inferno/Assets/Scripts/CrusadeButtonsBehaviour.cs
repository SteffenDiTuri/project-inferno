using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrusadeButtonsBehaviour : MonoBehaviour
{
    public void StartCrusadeButton(){
        Debug.Log("You Successfully Started Another Crusade!");

        SceneManager.LoadScene("DungeonStartScene");
    }

    public void ReturnToHomeBaseButton(){
        Debug.Log("You Successfully Went To Home Base!");

        SceneManager.LoadScene("HomeBase");
    }

}
