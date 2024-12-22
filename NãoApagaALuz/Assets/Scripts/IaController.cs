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
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        SetTarget(TARGET);
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
        idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        while(idSort==oldSort)
        {
            idSort = Random.Range(0, RoomsControl.Instance.rooms.Length);
        }
        oldSort = idSort;
        SetTarget(RoomsControl.Instance.ReturnTargetRoom(idSort));
        
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
