using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private Player player; // Change to Player type

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
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !player.IsInCombat())
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (collision.gameObject.tag.Equals("Up"))
        {
            player.gameObject.transform.Translate(new Vector3(0,3,0));
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
    }
}
