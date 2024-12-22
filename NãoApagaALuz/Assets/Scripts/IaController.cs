using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static RoomsControl;

public class IaController : MonoBehaviour
{
    public int idSort, oldSort;
    private NavMeshAgent navmesh;
    public Transform TARGET;
    public IaState currentIaState=IaState.SearchRoom;
    public RoomController currentRoom;
    public int searchCount, maxCount;
    bool startGame=false;
    public enum IaState { 
        SearchRoom,
        SearchLigth,
        LockDoor
        };
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        //RandomizeTarget();
    }
    public void SetTarget(Transform target)
    {
        if (navmesh != null)
        {
            Debug.Log("SETOU");
            navmesh.SetDestination(target.position);
        }
    }
    public void RandomizeTarget()
    {
        if (!startGame) startGame=true;
        idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        while(idSort==oldSort)
        {
            idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        }
        oldSort = idSort;
        currentRoom = RoomsControl.Instance.ReturnTargetRoom(idSort);
        SetTarget(RoomsControl.Instance.ReturnTargetRoom(idSort).transform);
        
    }
    void Update()
    {
        if (HasReachedDestination()&& startGame)
        {
            DoSomething();
        }
    }
    private void DoSomething()
    {
        switch(currentIaState)
        {
            case IaState.SearchRoom:
                currentIaState = IaState.SearchLigth;
                SearchForLigthsOn();
                break;
            case IaState.SearchLigth:
                if (!SearchForLigthsOn())
                {
                    currentIaState = IaState.SearchRoom;
                    RandomizeTarget();
                }
                else
                {
                    SearchForLigthsOn();
                }

                break;
        }
    }
    private bool HasReachedDestination()
    {
        if (navmesh.pathPending)
            return false;

        if (navmesh.remainingDistance > navmesh.stoppingDistance)
            return false;

        // Verifica se o agente está parado.
        return !navmesh.hasPath || navmesh.velocity.sqrMagnitude == 0f;
    }
    public bool SearchForLigthsOn()
    {
        searchCount++;
        StateManager state= RoomsControl.Instance.FindLigthOn(currentRoom);
        if(state!=null && searchCount< maxCount)
        {
            //manda apagar aqui
            state.SetState(SwitchBehauviour.SwitchState.OFF);
            return true;
        }
        else
        {

            searchCount = 0;
            return false;
        }
    }

     private void OnGUI()
    {

        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (GUILayout.Button("SetTarget")) RandomizeTarget();
            // if (GUILayout.Button("Calculate")) CalculatePercent();
        }
        GUILayout.EndArea();

    }
}
