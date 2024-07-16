using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStartSettings : MonoBehaviour
{
    private Player player; 

    void Start() {
        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject == null)
        {
            Debug.LogError("Player GameObject not found! Make sure it is named 'Player'.");
        }
        else
        {
            player = playerGameObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player component not found on the Player GameObject.");
            }

            player.SetDungeonState(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
