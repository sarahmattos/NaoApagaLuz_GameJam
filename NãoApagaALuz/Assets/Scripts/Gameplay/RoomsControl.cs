using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomsControl : MonoBehaviour
{
    public RoomController[] rooms;
    public List<GameObject> allDoors= new List<GameObject>();
    public GameObject player;
    bool canCloseDoor = true;
    public float timeDoor = 10;

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
        Debug.Log("chamou door");
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
        if(canCloseDoor)StartCoroutine(ActiveDoor(closestDoor));
    }
    public IEnumerator ActiveDoor(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(timeDoor);
        if(go.activeSelf)go.GetComponent<Door>().Reset();
    }
}
