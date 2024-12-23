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


    public void SetLigthOn(bool stateOn)
    {
        //change de ligth in scene
        Ligth.GetComponent<LigthController>().SwitchState(stateOn);

        //handle interruptor
        HandleInterruptor(stateOn);
    }

    public void HandleInterruptor(bool value)
    {
        if(value)
        {
            interruptor.GetComponent<MeshRenderer>().material = matOn;
            // RotateInterruptor(-20f);
            // interruptor.SetActive(false);
            // Quaternion finalRotation = Quaternion.Euler(this.interruptor.transform.eulerAngles.x, -20f, this.interruptor.transform.eulerAngles.z);
            AnimSwitch.SetTrigger("On");

           // this.interruptor.transform.rotation = finalRotation;
        }
        else
        {
            interruptor.GetComponent<MeshRenderer>().material = matOff;
            AnimSwitch.SetTrigger("Off");
            // RotateInterruptor(0);
        }

    }
    public void RotateInterruptor(float angle)
    {
            StartCoroutine(RotateToAngle(interruptor, angle, 1f)); // Rotaciona para -20° no eixo Y em 1 segundo
    }

    private IEnumerator RotateToAngle(GameObject target, float targetYRotation, float duration)
    {
        Quaternion initialRotation = target.transform.rotation; 
        Quaternion finalRotation = Quaternion.Euler(target.transform.eulerAngles.x, targetYRotation, target.transform.eulerAngles.z); 

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.transform.rotation = finalRotation;
    }

}
