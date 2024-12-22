using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehauviour : MonoBehaviour
{
    private Animator AnimSwitch; 
    [SerializeField] private GameObject Ligth; 
    // Start is called before the first frame update
    [System.Serializable]
    public enum SwitchState
    {
        ON, OFF
    }

    
    void Start()
    {
        AnimSwitch = GetComponentInChildren<Animator>();
    }
    public void SetLigthOn(bool stateOn)
    {
        //AnimSwitch.gameObject.SetActive(stateOn);
        Ligth.SetActive(stateOn);
    }
    // Update is called once per frame
   
}
