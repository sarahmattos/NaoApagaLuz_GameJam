using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startedGame = false;
    public static GameManager instance;
    [SerializeField] private  GameObject winPanel;
    [SerializeField] private  GameObject loosePanel;
    public void StartGame()
    {
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
    public void FinishGame()
    {
        FindAnyObjectByType<CharacterController>().enabled = false;
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None; // Trava o cursor no centro da tela
        Cursor.visible = true; // Trava o cursor no centro da tela
        GameObject aux = null;
        aux = SwitchsManager.Instance.WinCondition() ? winPanel : loosePanel;
        aux.SetActive(true);

    }
    public void Backmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
