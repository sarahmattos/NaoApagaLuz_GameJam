using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startedGame = false;
    public static GameManager instance;
    [SerializeField] private  GameObject winPanel;
    [SerializeField] private  GameObject loosePanel;
    [SerializeField] private  GameObject uiGame;

    [SerializeField] private TMP_Text timeRemaningStartText;

    private float timeStartRemaining;

    float timeStart = 6;
    public void StartGame()
    {
        startedGame = true;//load gun, load tp, move player, loock doors power countincrease, checkFinalGame, ia check target reached
        FindAnyObjectByType<IaController>().RandomizeTarget();//ia started
        FindAnyObjectByType<RegressiveCountdown>().StartCouroutineTime();//regressivecountstarted

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeCutscene.Instance.FadeIn(true, () =>
            {
                StartCoroutine(TimeToStart());
            });

            //call fade in and fade out to call regressive started coutdowns. betwwen that call things
        }
    }
    public void ThingsToDoToStartGame()
    {
        SwitchsManager.Instance.RandomizeStateSwitchs();
        uiGame.gameObject.SetActive(true);
        //mudar a camera e o pato, active ui, randomize switchs
    }
    private void Start()
    {
        instance = this;
    }
    public void FinishGame()
    {
        FindAnyObjectByType<CharacterController>().enabled = false;
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        GameObject aux = null;
        aux = SwitchsManager.Instance.WinCondition() ? winPanel : loosePanel;
        aux.SetActive(true);

    }
    public void Backmenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public IEnumerator TimeToStart()
    {
        gameObject.SetActive(true);

        timeStartRemaining = timeStart;
        timeRemaningStartText.gameObject.SetActive(true);
        while (timeStartRemaining > 1.1f)
        {
            timeStartRemaining -= Time.deltaTime;
            float aux = Mathf.Floor(timeStartRemaining);
            timeRemaningStartText.text = aux.ToString();


            yield return null;

        }
        timeRemaningStartText.gameObject.SetActive(false);
        StartGame();
    }

}
