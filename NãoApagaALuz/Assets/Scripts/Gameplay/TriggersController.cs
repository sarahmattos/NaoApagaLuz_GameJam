using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TriggersController : MonoBehaviour
{
    ScaryTrigger[] allTriggers;
    ScaryTrigger currentTrigger;
    int idSort, oldSort;
    public static TriggersController instance;
    void Start()
    {
        instance = this;
        allTriggers = GetComponentsInChildren<ScaryTrigger>();
    }
    public void RandomizeTrigger()
    {
        idSort = Random.Range(0, allTriggers.Length);
        while (idSort == oldSort)
        {
            idSort = Random.Range(0, allTriggers.Length);
        }
        oldSort = idSort;
        currentTrigger = allTriggers[idSort];
        currentTrigger.SetBoolActive();
    }

}
