using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public  StateManager[] ligths;
    [SerializeField] private GameObject ligthParent;
    void Start()
    {
        ligths = ligthParent.transform.GetComponentsInChildren<StateManager>();
    }

    void Update()
    {
        
    }
}
