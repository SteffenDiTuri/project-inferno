using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrusadeButtonsBehaviour : MonoBehaviour
{
    public void StartCrusadeButton(){

        SceneManager.LoadScene("DungeonStartScene");
    }

    public void ReturnToHomeBaseButton(){

        SceneManager.LoadScene("HomeBase");
    }

}
