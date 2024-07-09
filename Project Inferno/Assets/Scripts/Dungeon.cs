using System;
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
        printTotalRooms();
        replaceBadDoors();
        printTotalRooms();
        addSecondaryRooms();
        printTotalRooms();
    }

    void printTotalRooms()
    {
        int totalRooms = 0;
        for (int i = 0; i < map.map.GetLength(0); i++)
        {
            for (int j = 0; j < map.map.GetLength(1); j++)
            {
                if (map.map[i, j])
                {
                    totalRooms += 1;
                }
            }
        }
        Debug.Log(totalRooms);
    }

    void Update()
    {
    }

    public void generateRooms(int total, GameObject firstRoom)
    {
        map = ScriptableObject.CreateInstance<Map>();
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
                        mapY -= 1;
                        index = UnityEngine.Random.Range(0, roomsDoorUp.Count);
                        room = Instantiate(roomsDoorUp[index].GetComponent<Room>().generateWithRandomExit(false, 2, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(0, -10.8f, 0);
                        break;
                    case 1:
                        mapX -= 1;
                        index = UnityEngine.Random.Range(0, roomsDoorRight.Count);
                        room = Instantiate(roomsDoorRight[index].GetComponent<Room>().generateWithRandomExit(false, 3, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(-19.25f, 0, 0);
                        break;
                    case 2:
                        mapY += 1;
                        index = UnityEngine.Random.Range(0, roomsDoorDown.Count);
                        room = Instantiate(roomsDoorDown[index].GetComponent<Room>().generateWithRandomExit(false, 0, new Vector2(mapX, mapY)));
                        room.transform.position = previousRoom.transform.position + new Vector3(0, 10.8f, 0);
                        break;
                    case 3:
                        mapX += 1;
                        index = UnityEngine.Random.Range(0, roomsDoorLeft.Count);
                        room = Instantiate(roomsDoorLeft[index].GetComponent<Room>().generateWithRandomExit(false, 1, new Vector2(mapX, mapY)));
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
                    if (map.roomSequence.Count >= 2)
                    {
                        GameObject lastRoom = map.roomSequence[map.roomSequence.Count - 1];
                        GameObject secondLastRoom = map.roomSequence[map.roomSequence.Count - 2];

                        map.roomSequence.Remove(lastRoom);
                        map.roomSequence.Remove(secondLastRoom);

                        map.setElement((int)lastRoom.GetComponent<Room>().location.x, (int)lastRoom.GetComponent<Room>().location.y, null);
                        map.setElement((int)secondLastRoom.GetComponent<Room>().location.x, (int)secondLastRoom.GetComponent<Room>().location.y, null);

                        map.cleanRoomSequence();
                        spawnAmount = 3;

                        Destroy(lastRoom);
                        Destroy(secondLastRoom);

                        if (map.roomSequence.Count > 0)
                        {
                            previousRoom = map.roomSequence[map.roomSequence.Count - 1].GetComponent<Room>();
                            mapX = (int)previousRoom.location.x;
                            mapY = (int)previousRoom.location.y;
                        }
                        else
                        {
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

    public void replaceBadDoors()
    {
        for (int i = 0; i < map.map.GetLength(0); i++)
        {
            for (int j = 0; j < map.map.GetLength(1); j++)
            {
                GameObject roomObject = map.map[i, j];
                if (roomObject)
                {
                    Room room = roomObject.GetComponent<Room>();
                    List<int> doors = room.possibleDoors();
                    List<int> doorsWithRoom = new List<int>();
                    foreach (int door in doors)
                    {
                        try
                        {
                            switch (door)
                            {
                                case 0:
                                    if (IsInBounds(i + 1, j) && map.map[i + 1, j])
                                    {
                                        doorsWithRoom.Add(door);
                                    }
                                    break;
                                case 1:
                                    if (IsInBounds(i, j - 1) && map.map[i, j - 1])
                                    {
                                        doorsWithRoom.Add(door);
                                    }
                                    break;
                                case 2:
                                    if (IsInBounds(i - 1, j) && map.map[i - 1, j])
                                    {
                                        doorsWithRoom.Add(door);
                                    }
                                    break;
                                case 3:
                                    if (IsInBounds(i, j + 1) && map.map[i, j + 1])
                                    {
                                        doorsWithRoom.Add(door);
                                    }
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.LogWarning(ex.Message);
                        }
                    }
                    if (doorsWithRoom.Count > 2)
                    {
                        List<GameObject>[] roomLists = { roomsDoorDown, roomsDoorLeft, roomsDoorUp, roomsDoorRight };
                        GameObject newRoom = null;
                        foreach (List<GameObject> roomList in roomLists)
                        {
                            foreach (GameObject possibleRoomObject in roomList)
                            {
                                Room possibleRoom = possibleRoomObject.GetComponent<Room>();
                                if (possibleRoom.possibleDoors().Count == 2)
                                {
                                    bool found = true;
                                    foreach (int door in possibleRoom.possibleDoors())
                                    {
                                        if (door != room.primaryEntrance && door != room.primaryExit)
                                        {
                                            found = false;
                                            break;
                                        }
                                    }
                                    if (found)
                                    {
                                        newRoom = possibleRoomObject;
                                        break;
                                    }
                                }
                            }
                            if (newRoom != null)
                            {
                                break;
                            }
                        }
                        if (newRoom != null)
                        {
                            changeRooms(roomObject, newRoom);
                        }
                    }
                }
            }
        }
    }

    void changeRooms(GameObject oldRoom, GameObject newRoom)
    {
        GameObject newRoomInstance = Instantiate(newRoom, oldRoom.transform.position, Quaternion.identity);
        Room oldRoomScript = oldRoom.GetComponent<Room>();
        Room newRoomScript = newRoomInstance.GetComponent<Room>();
        newRoomScript.location = oldRoomScript.location;
        newRoomScript.primaryEntrance = oldRoomScript.primaryEntrance;
        newRoomScript.primaryExit = oldRoomScript.primaryExit;
        map.setElement((int)newRoomScript.location.x, (int)newRoomScript.location.y, newRoomInstance);
        Destroy(oldRoom);
    }

    public void addSecondaryRooms()
    {
        for (int i = 0; i < map.map.GetLength(0); i++)
        {
            for (int j = 0; j < map.map.GetLength(1); j++)
            {
                GameObject roomObject = map.map[i, j];
                if (roomObject == null)
                {
                    List<int> directions = new List<int>();
                    for (int direction = 0; direction < 4; direction++)
                    {
                        try
                        {
                            switch (direction)
                            {
                                case 0:
                                    if (IsInBounds(i + 1, j) && map.map[i + 1, j] && map.map[i + 1, j].GetComponent<Room>().possibleDoors().Contains(2))
                                    {
                                        directions.Add(direction);
                                    }
                                    break;
                                case 1:
                                    if (IsInBounds(i, j - 1) && map.map[i, j - 1] && map.map[i, j - 1].GetComponent<Room>().possibleDoors().Contains(3))
                                    {
                                        directions.Add(direction);
                                    }
                                    break;
                                case 2:
                                    if (IsInBounds(i - 1, j) && map.map[i - 1, j] && map.map[i - 1, j].GetComponent<Room>().possibleDoors().Contains(0))
                                    {
                                        directions.Add(direction);
                                    }
                                    break;
                                case 3:
                                    if (IsInBounds(i, j + 1) && map.map[i, j + 1] && map.map[i, j + 1].GetComponent<Room>().possibleDoors().Contains(1))
                                    {
                                        directions.Add(direction);
                                    }
                                    break;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    if (directions.Count > 0)
                    {
                        Debug.Log($"The amount of doors is: {directions.Count}");
                        List<GameObject>[] roomLists = { secondaryRooms, roomsDoorDown, roomsDoorLeft, roomsDoorUp, roomsDoorRight };
                        GameObject room = null;
                        foreach (List<GameObject> roomList in roomLists)
                        {
                            foreach (GameObject possibleRoomObj in roomList)
                            {
                                Room possibleRoom = possibleRoomObj.GetComponent<Room>();
                                List<int> possibleRoomDoors = possibleRoom.possibleDoors();
                                Debug.Log($"The amount of possible doors is: {possibleRoomDoors.Count}");
                                if (possibleRoomDoors.Count == directions.Count)
                                {
                                    possibleRoomDoors.Sort();
                                    directions.Sort();
                                    bool found = true;
                                    for (int index = 0; index < directions.Count; index++)
                                    {
                                        Debug.Log(possibleRoomDoors[index]);
                                        Debug.Log(directions[index]);
                                        Debug.Log("");
                                        if (possibleRoomDoors[index] != directions[index])
                                        {
                                            found = false;
                                            break;
                                        }
                                    }
                                    if (found)
                                    {
                                        room = possibleRoomObj;
                                        break;
                                    }
                                }
                            }
                            if (room != null)
                            {
                                break;
                            }
                        }
                        if (room != null)
                        {
                            spawnSecondaryRoom(room, i, j);
                        }
                    }
                }
            }
        }
    }

    void spawnSecondaryRoom(GameObject room, int i, int j)
    {
        GameObject roomInstance = Instantiate(room);
        map.setElement(i, j, roomInstance);
        roomInstance.GetComponent<Room>().location = new Vector2(i, j);

        float centerX = (map.map.GetLength(1) - 1) / 2.0f;
        float centerY = (map.map.GetLength(0) - 1) / 2.0f;

        float xPosition = (j - centerX) * 19.25f;
        float yPosition = (centerY - i) * 10.8f;

        roomInstance.transform.position = new Vector2(xPosition, yPosition);
        Debug.Log($"Spawned secondary room at index [{i}, {j}] with position: {roomInstance.transform.position}");
    }


    bool IsInBounds(int i, int j)
    {
        return i >= 0 && i < map.map.GetLength(0) && j >= 0 && j < map.map.GetLength(1);
    }
}
