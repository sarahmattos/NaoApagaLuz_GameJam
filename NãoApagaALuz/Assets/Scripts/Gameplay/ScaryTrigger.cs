using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryTrigger : MonoBehaviour
{
    [SerializeField] GameObject scaryObject;
    bool canActive = false;
   public void ActiveScary()
    {
        if (canActive)
        {
            StartCoroutine(ScaryPrank());
            Soundmanager.Instance.ScarySound();
        }

        }
        private IEnumerator ScaryPrank()
    {

        int id = Random.Range(0, GameManager.instance.scaryUI.Length);
        canActive = false;
        GameManager.instance.scaryUI[id].SetActive(true);
        // scaryObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.scaryUI[id].SetActive(false);
        Soundmanager.Instance.ScarySound(false) ;
        //scaryObject.SetActive(false);
    }
    public void SetBoolActive()
    {
        canActive = true;
    }
}
