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
        for(int i = 0; i < total; i++)
        {

        }
    }
}
