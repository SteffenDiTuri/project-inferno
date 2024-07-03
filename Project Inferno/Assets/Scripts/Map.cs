using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : ScriptableObject
{
    public GameObject[,] map;
    public List<GameObject> roomSequence = new List<GameObject>();

    public void createMap(int size)
    {
        map = new GameObject[size * 2 + 1, size * 2 + 1];
        for (int i = 0; i<size*2; i++)
        {
            for (int j = 0; j < size * 2; j++)
            {
                map[i, j] = null;
            }
        }
    }

    public void setElement(int x, int y, GameObject obj)
    {
        map[y, x] = obj;
        roomSequence.Add(obj);
    }

    public GameObject getElement(int x, int y)
    {
        return map[y, x];
    }

    public void cleanRoomSequence()
    {
        GameObject[] newList = roomSequence.ToArray();
        roomSequence = new List<GameObject>();
        foreach (GameObject i in newList)
        {
            if (i)
            {
                roomSequence.Add(i);
            }
        }
    }
}
