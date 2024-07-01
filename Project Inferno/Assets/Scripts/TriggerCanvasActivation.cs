using UnityEngine;

public class TriggerCanvasActivation : MonoBehaviour
{
    public GameObject canvas; // Assign the Canvas GameObject in the Inspector

    public void Start(){
        canvas.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            if (canvas != null)
            {
                canvas.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }
}
