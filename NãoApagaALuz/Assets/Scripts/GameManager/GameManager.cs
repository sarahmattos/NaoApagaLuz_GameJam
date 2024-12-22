using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
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
  
}
