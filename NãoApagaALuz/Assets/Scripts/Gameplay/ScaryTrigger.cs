using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryTrigger : MonoBehaviour
{
    [SerializeField] GameObject scaryObject;
    bool canActive = false;
   public void ActiveScary()
    {
        if (canActive) StartCoroutine(ScaryPrank());
    }
    private IEnumerator ScaryPrank()
    {
        Debug.Log("ativou");
        canActive = false;
        scaryObject.SetActive(true);
        yield return new WaitForSeconds(2);
        scaryObject.SetActive(false);
    }
    public void SetBoolActive()
    {
        canActive = true;
    }
}
