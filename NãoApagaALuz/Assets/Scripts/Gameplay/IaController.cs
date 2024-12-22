using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IaController : MonoBehaviour
{
    public int idSort, oldSort;
    private NavMeshAgent navmesh;
    public Transform TARGET;
    public IaState currentIaState = IaState.SearchRoom;
    public RoomController currentRoom;
    public int searchCount, maxCount;
    public List<StateManager> ligthsOn = new List<StateManager>();
    private int currentLightIndex = 0;
    public bool startGame = false;

    public enum IaState
    {
        SearchRoom,
        SearchLigth,
        LockDoor
    }

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
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
        if (!startGame)
        {
            startGame = true;
        }

        idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        while (idSort == oldSort)
        {
            idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        }
        oldSort = idSort;
        currentRoom = RoomsControl.Instance.ReturnTargetRoom(idSort);
        SetTarget(RoomsControl.Instance.ReturnTargetRoom(idSort).transform);
    }

    void Update()
    {
        if (HasReachedDestination() && startGame)
        {
            DoSomething();
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
                    // Se nenhuma luz foi encontrada, volte a procurar salas
                    currentIaState = IaState.LockDoor;
                    DoSomething();
                }
                break;

            case IaState.SearchLigth:
                if (HasReachedDestination())
                {
                // Apague a luz atual
                    ligthsOn[currentLightIndex].SetState(SwitchBehauviour.SwitchState.OFF);
                    currentLightIndex++;

                    //talvez nessa hora seria bom atualizar a lista
                    if (currentLightIndex < ligthsOn.Count)
                    {
                        SetTarget(ligthsOn[currentLightIndex].transform);
                    }
                    else
                    {
                        // Apagou no máximo 3 luzes, volte a procurar salas
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

        // Verifica se o agente está parado.
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
