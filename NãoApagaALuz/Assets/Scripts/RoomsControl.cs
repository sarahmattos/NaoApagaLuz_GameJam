using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomsControl : MonoBehaviour
{
    public RoomController[] rooms;

    public static RoomsControl Instance;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        rooms = GetComponentsInChildren<RoomController>();
    }
    public RoomController ReturnTargetRoom(int id)
    {
        return rooms[id];
    }
    public StateManager FindLigthOn(RoomController room)
    {
        foreach (StateManager state in room.ligths)
        {
            if(state.IsAcesso())return state;
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
