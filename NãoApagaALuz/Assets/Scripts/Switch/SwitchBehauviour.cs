using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class SwitchBehauviour : MonoBehaviour
{
    private Animator AnimSwitch; 
    [SerializeField] private GameObject Ligth;

    void Start()
    {
        AnimSwitch = GetComponentInChildren<Animator>();
    }


    [System.Serializable]
    public enum SwitchState
    {
        ON, OFF
    }


    public void SetLigthOn(bool stateOn)
    {
        
        Ligth.GetComponent<LigthController>().SwitchState(stateOn);
    }
    
}
