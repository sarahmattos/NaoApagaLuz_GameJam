using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Transform ReturnTargetRoom(int id)
    {
        return rooms[id].transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
