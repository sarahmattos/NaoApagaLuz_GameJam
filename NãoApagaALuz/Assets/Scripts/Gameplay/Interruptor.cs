using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Quaternion finalRotation = Quaternion.Euler(this.transform.eulerAngles.x, -20f, this.transform.eulerAngles.z);

        this.transform.rotation = finalRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
