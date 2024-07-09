using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : ScriptableObject
{
    public GameObject[,] map;
    public List<GameObject> roomSequence = new List<GameObject>();

    public void createMap(int size)
    {
        int dimension = size * 2 + 1;
        map = new GameObject[dimension, dimension];

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                map[i, j] = null;
            }
        }
    }

    public void setElement(int x, int y, GameObject obj)
    {
        // Check bounds before setting the element
        if (IsWithinBounds(x, y))
        {
            map[y, x] = obj;
            roomSequence.Add(obj);
        }
        else
        {
            Debug.LogWarning("Attempted to set element out of bounds.");
        }
    }

    public GameObject getElement(int x, int y)
    {
        // Check bounds before getting the element
        if (IsWithinBounds(x, y))
        {
            return map[y, x];
        }
        else
        {
            Debug.LogWarning("Attempted to get element out of bounds.");
            return null;
        }
    }

    public void cleanRoomSequence()
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (GameObject room in roomSequence)
        {
            if (room != null)
            {
                newList.Add(room);
            }
        }
        roomSequence = newList;
    }

    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0);
    }
}
