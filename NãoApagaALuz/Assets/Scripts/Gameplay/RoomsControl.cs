using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomsControl : MonoBehaviour
{
    public RoomController[] rooms;
    public List<GameObject> allDoors= new List<GameObject>();
    public GameObject player;

    

    bool canCloseDoor = false;

    [SerializeField] private float totalTime = 15f;  
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
            allDoors.Add(go.door);
        }
    }

    void Update()
    {
        if (!FindAnyObjectByType<IaController>().startGame) return;
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
        GameObject closestDoor = allDoors[0];
        float distance = Vector3.Distance(player.transform.position, closestDoor.transform.position);
        foreach (GameObject go in allDoors)
        {
            float aux = Vector3.Distance(player.transform.position, go.transform.position);
            if (aux < distance)
            {
                distance = aux;
                closestDoor = go;
            }
        }
        if (canCloseDoor)
        {
            ResetTimerLockDoor();
            StartCoroutine(closestDoor.GetComponent<Door>().ActiveDoor());
        }
    }
   

}
