using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthController : MonoBehaviour
{
    [SerializeField] GameObject l1, l2, l3;
    public void SwitchState(bool value)
    {
       l1.SetActive(value);
       l2.SetActive(value);
       l3.SetActive(!value);
    }
}
