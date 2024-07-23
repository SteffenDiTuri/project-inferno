using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private Player player;

    // Distance the player must travel to trigger a potential encounter
    private float encounterDistanceThreshold = 5.0f;
    // Frequency of random encounters after the distance threshold (higher value = higher chance)
    private float encounterRate = 0.1f;
    private Vector3 lastPosition;
    private float distanceTraveled;

    // Reference to the main camera
    private Camera mainCamera;
    // Position to move the camera to during the battle encounter
    private Vector3 battleCameraPosition = new Vector3(1.5f, -71.6f, -10f); // Adjust the z-axis if needed
    private Vector3 secondairyRoomCameraPosition = new Vector3(0, -200f, -10f); // Adjust the z-axis if needed
    private Vector3 originalCameraPosition;
    private Vector3 originalPlayerPosition;

    // Reference to the battle encounter GameObject
    private GameObject battleEncounter;

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
        }

        // Initialize last position to the player's starting position
        lastPosition = transform.position;
        distanceTraveled = 0.0f;

        // Check if the current scene is "DungeonStartScene"
        if (SceneManager.GetActiveScene().name == "DungeonStartScene")
        {
            // Find the main camera
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found! Make sure there is a camera tagged 'MainCamera'.");
            }

            // Find the battle encounter GameObject
            battleEncounter = GameObject.Find("CombatEncounter");
            if (battleEncounter == null)
            {
                Debug.LogError("CombatEncounter GameObject not found! Make sure it is named 'CombatEncounter'.");
            }
            else
            {
                SetChildrenActive(battleEncounter, false); // Ensure it starts inactive

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && !player.IsInCombat())
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

            transform.Translate(direction * _speed * Time.deltaTime);

            // Update the distance traveled
            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;

            // Check if the player has traveled the required distance
            if (player.IsInDungeon() && distanceTraveled >= encounterDistanceThreshold)
            {
                distanceTraveled = 0.0f; // Reset distance traveled
                if (Random.value < encounterRate)
                {
                    TriggerBattleEncounter();
                }
            }
        }
    }

    private void TriggerBattleEncounter()
    {
        if (SceneManager.GetActiveScene().name == "DungeonStartScene")
        {
            // Store the original camera position
            if (mainCamera != null)
            {
                originalCameraPosition = mainCamera.transform.position;
            }

            // Change location of the camera
            if (mainCamera != null)
            {
                mainCamera.transform.position = battleCameraPosition;
            }

            // Set the BattleEncounter GameObject's children to active state
            if (battleEncounter != null)
            {
                SetChildrenActive(battleEncounter, true);
            }
            BattleSystem battleSystem = battleEncounter.GetComponentInChildren<BattleSystem>();
            battleSystem.Begin();
        }
    }
    public void EndBattle()
    {
        // Change location of the camera back to the original position
        if (mainCamera != null)
        {
            mainCamera.transform.position = originalCameraPosition;
        }

        // Set the BattleEncounter GameObject's children to inactive state
        if (battleEncounter != null)
        {
            SetChildrenActive(battleEncounter, false);
        }
    }

    private void SetChildrenActive(GameObject parent, bool state)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(state);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (collision.gameObject.tag.Equals("Up"))
        {
            player.gameObject.transform.Translate(new Vector3(0, 3, 0));
            camera.transform.Translate(new Vector3(0, 10.8f, 0));
        }
        if (collision.gameObject.tag.Equals("Down"))
        {
            player.gameObject.transform.Translate(new Vector3(0, -3, 0));
            camera.transform.Translate(new Vector3(0, -10.8f, 0));
        }
        if (collision.gameObject.tag.Equals("Left"))
        {
            player.gameObject.transform.Translate(new Vector3(-3, 0, 0));
            camera.transform.Translate(new Vector3(-19.25f, 0, 0));
        }
        if (collision.gameObject.tag.Equals("Right"))
        {
            player.gameObject.transform.Translate(new Vector3(3, 0, 0));
            camera.transform.Translate(new Vector3(19.25f, 0, 0));
        }
        if(collision.gameObject.name.Equals("Portal"))
        {
            collision.gameObject.SetActive(false);
            GoToSecondairyRoom();
        }
        if (collision.gameObject.name.Equals("Portal_Back"))
        {
            GoToPrimaryRoom();
        }
    }

    private void GoToSecondairyRoom()
    {
        if (SceneManager.GetActiveScene().name == "DungeonStartScene")
        {
            // Store the original camera position
            if (mainCamera != null)
            {
                originalCameraPosition = mainCamera.transform.position;
            }

            // Change location of the camera
            if (mainCamera != null)
            {
                mainCamera.transform.position = secondairyRoomCameraPosition;
            }
            player.SetDungeonState(false);
            originalPlayerPosition = player.gameObject.transform.position;
            player.gameObject.transform.position = new Vector3(0, -202, 0);
        }
    }

    private void GoToPrimaryRoom()
    {
        if (SceneManager.GetActiveScene().name == "DungeonStartScene")
        {
            if (mainCamera != null)
            {
                mainCamera.transform.position = originalCameraPosition;
            }
            player.SetDungeonState(true);
            player.gameObject.transform.position = originalPlayerPosition;
        }
    }
}
