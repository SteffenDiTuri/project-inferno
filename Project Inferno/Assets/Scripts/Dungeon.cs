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
    public GameObject finalRoom;
    public GameObject startRoom;

    public Map map;

    public int mapX;
    public int mapY;

    void Start()
    {
        generateRooms(10, startRoom);
    }

    void Update()
    {
    }

    public void generateRooms(int total, GameObject firstRoom)
    {
        map = new Map();
        map.createMap(total);
        Room previousRoom = firstRoom.GetComponent<Room>();
        mapX = total + (int)firstRoom.GetComponent<Room>().location.x;
        mapY = total + (int)firstRoom.GetComponent<Room>().location.y;
        map.setElement(mapX, mapY, firstRoom);
        GameObject room = null;

        for (int i = 0; i < total; i++)
        {
            int index = 0;
            int spawnAmount = 1;
            int maxAttempts = 50;
            int attempts = 0;

            while (spawnAmount != 0 && attempts < maxAttempts)
            {
                switch (previousRoom.primaryExit)
                {
                    case 0:
                        Debug.Log("entrance: up");
                        mapY -= 1;
                        index = Random.Range(0, roomsDoorUp.Count);
                        room = Instantiate(roomsDoorUp[index].GetComponent<Room>().generate(false, 2, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(0, -10.8f, 0);
                        break;
                    case 1:
                        Debug.Log("entrance: right");
                        mapX -= 1;
                        index = Random.Range(0, roomsDoorRight.Count);
                        room = Instantiate(roomsDoorRight[index].GetComponent<Room>().generate(false, 3, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(-19.25f, 0, 0);
                        break;
                    case 2:
                        Debug.Log("entrance: down");
                        mapY += 1;
                        index = Random.Range(0, roomsDoorDown.Count);
                        room = Instantiate(roomsDoorDown[index].GetComponent<Room>().generate(false, 0, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(0, 10.8f, 0);
                        break;
                    case 3:
                        Debug.Log("entrance: left");
                        mapX += 1;
                        index = Random.Range(0, roomsDoorLeft.Count);
                        room = Instantiate(roomsDoorLeft[index].GetComponent<Room>().generate(false, 1, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(19.25f, 0, 0);
                        break;
                }

                if (map.getElement(mapX, mapY) == null)
                {
                    map.setElement(mapX, mapY, room);
                    previousRoom = room.GetComponent<Room>();
                    spawnAmount -= 1;
                }
                else
                {
                    Destroy(room);
                    Debug.Log("oopsie whoopsie");
                    if (map.roomSequence.Count >= 2)
                    {
                        GameObject lastRoom = map.roomSequence[map.roomSequence.Count - 1];

                        map.roomSequence.Remove(lastRoom);

                        map.setElement((int)lastRoom.GetComponent<Room>().location.x, (int)lastRoom.GetComponent<Room>().location.y, null);

                        map.cleanRoomSequence();
                        spawnAmount = 2;

                        Destroy(lastRoom);

                        if (map.roomSequence.Count > 0)
                        {
                            Debug.Log(map.roomSequence[map.roomSequence.Count - 1]);
                            previousRoom = map.roomSequence[map.roomSequence.Count - 1].GetComponent<Room>();
                            mapX = (int)previousRoom.location.x;
                            mapY = (int)previousRoom.location.y;
                        }
                        else
                        {
                            Debug.LogWarning("No rooms left to backtrack.");
                            break;
                        }
                    }
                }

                attempts++;
            }

            if (spawnAmount != 0)
            {
                Debug.LogWarning("Failed to place a room after multiple attempts.");
                break;
            }
        }
    }
}
