using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool startedGame = false;
    public static GameManager instance;
    public void StartGame()
    {
       startedGame = true;
       SwitchsManager.Instance.RandomizeStateSwitchs();
       FindAnyObjectByType<IaController>().RandomizeTarget();
       FindAnyObjectByType<RegressiveCountdown>().StartCouroutineTime();
    }
 
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    private void Start()
    {
        instance = this;
    }
}
