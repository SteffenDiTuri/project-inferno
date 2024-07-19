using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCanvasBehaviour : MonoBehaviour
{
    // Reference to the canvas
    public GameObject saveCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the canvas is initially inactive
        if (saveCanvas != null)
        {
            saveCanvas.SetActive(false);
            // Debug.Log("Save Canvas Set to Inactive");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "Escape" key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the canvas active state
            if (saveCanvas != null)
            {
                saveCanvas.SetActive(!saveCanvas.activeSelf);
                // Debug.Log("Escape Key Pressed, Toggling Canvas");
                updatePlayerUI();
            }
        }
    }

    public void updatePlayerUI()
    {
        InventoryUI UI = this.transform.Find("SaveSystemCanvas").Find("InventoryUI").GetComponent<InventoryUI>(); ;
        if (UI)
        {
            UI.updateInventoryUI();
        }
    }
}
