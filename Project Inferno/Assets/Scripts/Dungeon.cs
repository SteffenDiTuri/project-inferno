using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public bool isBossDungeon;
    public List<GameObject> roomsDoorLeft = new();
    public List<GameObject> roomsDoorRight = new();
    public List<GameObject> roomsDoorUp = new();
    public List<GameObject> roomsDoorDown = new();

    public List<GameObject> secondaryRooms = new();
    public GameObject FinalRoom;
    public GameObject startRoom;

    public Map map;

    // Start is called before the first frame update
    void Start()
    {
        generateRooms(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateRooms(int total)
    {
        //Gamemap.create(total);
        int previousExit = startRoom.GetComponent<Room>().primaryExit;
        GameObject room = null;
        for(int i = 0; i < total; i++)
        {
            int index = 0;
            switch (previousExit) {
                case 0:
                    index = Random.Range(0, roomsDoorUp.Count - 1);
                    room = Instantiate(roomsDoorUp[index].GetComponent<Room>().generate(false, 2));
                    previousExit = room.GetComponent<Room>().primaryExit;
                    Debug.Log(previousExit);
                    break;
                case 1:
                    index = Random.Range(0, roomsDoorRight.Count - 1);
                    room = Instantiate(roomsDoorRight[index].GetComponent<Room>().generate(false, 3));
                    previousExit = room.GetComponent<Room>().primaryExit;
                    Debug.Log(previousExit);
                    break;
                case 2:
                    index = Random.Range(0, roomsDoorDown.Count - 1);
                    room = Instantiate(roomsDoorDown[index].GetComponent<Room>().generate(false, 0));
                    previousExit = room.GetComponent<Room>().primaryExit;
                    Debug.Log(previousExit);
                    break;
                case 3:
                    index = Random.Range(0, roomsDoorLeft.Count - 1);
                    room = Instantiate(roomsDoorLeft[index].GetComponent<Room>().generate(false, 1));
                    previousExit = room.GetComponent<Room>().primaryExit;
                    Debug.Log(previousExit);
                    break;
            }

        }
    }
}
