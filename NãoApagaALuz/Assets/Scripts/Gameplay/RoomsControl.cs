using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomsControl : MonoBehaviour
{
    public RoomController[] rooms;
    public List<GameObject> allDoors= new List<GameObject>();
    public GameObject player;

    

    bool canCloseDoor = false;

    [SerializeField] private float totalTime = 10f;  
    private float timeElapsed = 0f; 

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
        foreach (RoomController go in rooms)
        {
            if(go.door!=null) allDoors.Add(go.door);
        }
    }

    void Update()
    {
        if (!GameManager.instance.startedGame) return;
        //door closed system
        if (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            canCloseDoor = true;
        }

    }

    void ResetTimerLockDoor()
    {
        timeElapsed = 0f;  
        canCloseDoor = false; 
    }
    public RoomController ReturnTargetRoom(int id)
    {
        return rooms[id];
    }
    public List<StateManager> FindAllLigthsOn(RoomController room)
    {
        List<StateManager> lightsOn = new List<StateManager>();

        foreach (StateManager state in room.ligths)
        {
            if (state.IsAcesso()) 
            {
                lightsOn.Add(state);
            }
        }

        return lightsOn;
    }
    public void SearchNearstDoorFromPlayer()
    {
        GameObject closestDoor = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject door in allDoors)
        {
            
            float distance3D = Vector3.Distance(player.transform.position, door.transform.position);

           
            float heightDifference = Mathf.Abs(player.transform.position.y - door.transform.position.y);

           
            if (heightDifference > 2.0f) 
            {
                distance3D += heightDifference * 10.0f; 
            }

            if (distance3D < closestDistance)
            {
                closestDistance = distance3D;
                closestDoor = door;
            }
            Debug.Log("fechou porta "+ closestDoor.transform.parent.name);
        }

        if (closestDoor != null && canCloseDoor)
        {
            ResetTimerLockDoor();
            StartCoroutine(closestDoor.GetComponent<Door>().ActiveDoor());
        }
    }



}
