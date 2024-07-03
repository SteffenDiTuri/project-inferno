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

    public Vector2 location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject generate(bool chest, int entrance, Vector2 location)
    {
        //GameObject chestObj = transform.Find("Chest").gameObject;
        //chestObj.SetActive(chest);
        primaryEntrance = entrance;
        int exit = Random.Range(0, 4);
        List<int> doors = possibleDoors();
        this.location = location;

        while(exit == primaryEntrance || !doors.Contains(exit))
        {
            exit = Random.Range(0, 4);
        }
        primaryExit = exit;
        
        return this.gameObject;
    }

    public List<int> possibleDoors()
    {
        List<int> doors = new List<int>();
        if (roomsLeft)
        {
            doors.Add(1);
        }
        if (roomsDown)
        {
            doors.Add(0);
        }
        if (roomsRight)
        {
            doors.Add(3);
        }
        if (roomsUp)
        {
            doors.Add(2);
        }
        return doors;
    }
}
