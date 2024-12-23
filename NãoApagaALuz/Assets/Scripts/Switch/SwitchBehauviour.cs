using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwitchBehauviour : MonoBehaviour
{
    public Animator AnimSwitch;
    private GameObject interruptor;

    [SerializeField]private Material matOn;
    [SerializeField]private Material matOff;
    [SerializeField] private GameObject Ligth;

    void Start()
    {
        interruptor = gameObject.transform.GetChild(0).gameObject;
    }


    [System.Serializable]
    public enum SwitchState
    {
        ON, OFF
    }


    public void SetLigthOn(bool stateOn, bool isPLayer)
    {
        //change de ligth in scene
        Ligth.GetComponent<LigthController>().SwitchState(stateOn);

        //handle interruptor
        HandleInterruptor(stateOn,isPLayer);
    }

    public void HandleInterruptor(bool value, bool isPlayer)
    {
        if(value)
        {
            interruptor.GetComponent<MeshRenderer>().material = matOn;
            AnimSwitch.SetTrigger("On");
            if(isPlayer) Soundmanager.Instance.ClickLigthOn();
        }
        else
        {
            interruptor.GetComponent<MeshRenderer>().material = matOff;
            AnimSwitch.SetTrigger("Off");
            if(isPlayer)Soundmanager.Instance.ClickLigthOff();
        }

    }
   

}
