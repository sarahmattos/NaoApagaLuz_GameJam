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
     private void OnGUI()
   {

       {
           GUILayout.BeginArea(new Rect(10, 10, 300, 300));
           if (GUILayout.Button("StartGame")) StartGame();
       }
       GUILayout.EndArea();

   }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartGame();
        }
    }
    private void Start()
    {
        instance = this;
    }
}
