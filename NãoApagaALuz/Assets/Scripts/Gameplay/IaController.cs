using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IaController : MonoBehaviour
{
    public int idSort, oldSort;
    private NavMeshAgent navmesh;
    public IaState currentIaState = IaState.SearchRoom;
    public IaState previousIaState = IaState.SearchRoom;
    public RoomController currentRoom;
    public int searchCount, maxCount;
    public List<StateManager> ligthsOn = new List<StateManager>();
    private int currentLightIndex = 0;
    float speed;
    [SerializeField] Material stunnedMat;
    [SerializeField] Material defaultMat;

    [SerializeField] private float totalTime = 20f;
    private float timeElapsed = 0f;

    public enum IaState
    {
        SearchRoom,
        SearchLigth,
        LockDoor,
        Stunned
    }
    public void CallGetStunned(float value)
    {
       if(currentIaState != IaState.Stunned) StartCoroutine(GotStunned(value));
    }
    private IEnumerator GotStunned(float v)
    {
        previousIaState = currentIaState;
        currentIaState = IaState.Stunned;
        navmesh.speed = 0;
       transform.GetChild(0).GetComponent<MeshRenderer>().material = stunnedMat;
        yield return new WaitForSeconds(v);
        currentIaState = previousIaState;
        navmesh.speed = speed;
        transform.GetChild(0).GetComponentInChildren<MeshRenderer>().material = defaultMat;
    }
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        speed = navmesh.speed;
    }

    public void SetTarget(Transform target)
    {
        if (navmesh != null)
        {
            navmesh.SetDestination(target.position);
        }
    }

    public void RandomizeTarget()
    {
        
        idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        while (idSort == oldSort)
        {
            idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        }
        oldSort = idSort;
        currentRoom = RoomsControl.Instance.ReturnTargetRoom(idSort);
        SetTarget(RoomsControl.Instance.ReturnTargetRoom(idSort).target);
    }

    void Update()
    {
        if (!GameManager.instance.startedGame) return;
        if (HasReachedDestination())
        {
            DoSomething();
        }
        if (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
            TriggersController.instance.RandomizeTrigger();
        }
       
    }

    private void DoSomething()
    {
        switch (currentIaState)
        {
            case IaState.SearchRoom:
                currentIaState = IaState.SearchLigth;
                if (!SearchForLigthsOn())
                {
                    currentIaState = IaState.LockDoor;
                    DoSomething();
                }
                break;

            case IaState.SearchLigth:
                if (HasReachedDestination())
                {
                    ligthsOn[currentLightIndex].SetState(SwitchBehauviour.SwitchState.OFF);
                    currentLightIndex++;

                    if (currentLightIndex < ligthsOn.Count)
                    {
                        SetTarget(ligthsOn[currentLightIndex].transform);
                    }
                    else
                    {
                        currentIaState = IaState.LockDoor;
                        DoSomething();
                    }
                    
                }
                break;
            case IaState.LockDoor:
                RoomsControl.Instance.SearchNearstDoorFromPlayer();
                currentIaState = IaState.SearchRoom;
                RandomizeTarget();
                break;
        }
    }

    private bool HasReachedDestination()
    {
        if (navmesh.pathPending)
            return false;

        if (navmesh.remainingDistance > navmesh.stoppingDistance)
            return false;

        return !navmesh.hasPath || navmesh.velocity.sqrMagnitude == 0f;
    }

    public bool SearchForLigthsOn()
    {
        ligthsOn.Clear();
        List<StateManager> allLigthsOn = RoomsControl.Instance.FindAllLigthsOn(currentRoom);

        ligthsOn.AddRange(allLigthsOn.GetRange(0, Mathf.Min(allLigthsOn.Count, maxCount)));

        if (ligthsOn.Count > 0)
        {
            currentLightIndex = 0;
            SetTarget(ligthsOn[currentLightIndex].transform);
            return true;
        }

        return false;
    }

   /* private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (GUILayout.Button("SetTarget")) RandomizeTarget();
        GUILayout.EndArea();
    }
    */
}
