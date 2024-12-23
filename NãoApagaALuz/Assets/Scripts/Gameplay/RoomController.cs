using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public StateManager[] ligths;
    public GameObject door;
    public Transform target;
    [SerializeField] private GameObject ligthParent;
    void Start()
    {
        ligths = ligthParent.transform.GetComponentsInChildren<StateManager>();
    }

    void Update()
    {
        
    }
}
