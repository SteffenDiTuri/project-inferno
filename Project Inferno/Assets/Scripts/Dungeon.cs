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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateRooms(int total)
    {
        //Gamemap.create(total);
        int startposition = startRoom.GetComponent<Room>().primaryExit;
        for(int i = 0; i < total; i++)
        {
            int index = 0;
            switch (startposition) {
                case 0:
                    index = Random.Range(0, roomsDoorUp.Count - 1);
                    Instantiate(roomsDoorUp[index].GetComponent<Room>().generate(false, 2));
                    break;
                case 1:
                    index = Random.Range(0, roomsDoorRight.Count - 1);
                    Instantiate(roomsDoorRight[index].GetComponent<Room>().generate(false, 2));
                    break;
                case 2:
                    index = Random.Range(0, roomsDoorDown.Count - 1);
                    Instantiate(roomsDoorDown[index].GetComponent<Room>().generate(false, 2));
                    break;
                case 4:
                    index = Random.Range(0, roomsDoorLeft.Count - 1);
                    Instantiate(roomsDoorLeft[index].GetComponent<Room>().generate(false, 2));
                    break;
            }

        }
    }
}
