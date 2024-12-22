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

    [SerializeField] private Slider slider;
    [SerializeField] private float totalTime = 20f;  
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
        slider.value = 0;
    }

    void Update()
    {
        if (!FindAnyObjectByType<IaController>().startGame) return;
        if (slider.value < 1f)
        {
            timeElapsed += Time.deltaTime;
            slider.value = timeElapsed / totalTime;
        }

        if (slider.value >= 1f && !canCloseDoor)
        {
            canCloseDoor = true;
        }
    }

    void ResetSlider()
    {
        timeElapsed = 0f;  
        slider.value = 0;
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
            ResetSlider();
            StartCoroutine(closestDoor.GetComponent<Door>().ActiveDoor());
        }
    }
   

}
