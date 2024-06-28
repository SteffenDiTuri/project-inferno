using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool roomsLeft;
    public bool roomsRight;
    public bool roomsUp;
    public bool roomsDown;
    public int primaryExit;
    public int primaryEntrance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject generate(bool chest, int entrance)
    {
        //GameObject chestObj = transform.Find("Chest").gameObject;
        //chestObj.SetActive(chest);
        primaryEntrance = entrance;
        int exit = Random.Range(0, 3);
        while(exit == primaryEntrance)
        {
            exit = Random.Range(0, 3);
        }
        primaryExit = exit;
        return this.gameObject;
    }
}
