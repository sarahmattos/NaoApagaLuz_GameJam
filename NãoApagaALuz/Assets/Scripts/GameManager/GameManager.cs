using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startedGame = false;
    public bool mmoveCamerae = false;
    public static GameManager instance;
    [SerializeField] private  GameObject winPanel;
    [SerializeField] private  GameObject loosePanel;
    [SerializeField] private  GameObject uiGame;
    [SerializeField] private  GameObject tutorial;
    [SerializeField] private  GameObject tutorialUi;

    bool hasCallEndDialogue = false;

    [SerializeField] private TMP_Text timeRemaningStartText;

    [SerializeField]public GameObject[] scaryUI;

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
            if (Dialogue.instance.HandleDialogue())
            {
                // Após o diálogo, inicia o fade e outras ações
                if (!hasCallEndDialogue)
                {
                    hasCallEndDialogue = true;
                    FadeCutscene.Instance.FadeIn(true, () =>
                    {
                        StartCoroutine(TimeToStart());
                    });
                }
               
            }

        }
    }
    public void ThingsToDoToStartGame()
    {
        mmoveCamerae = true;
        SwitchsManager.Instance.RandomizeStateSwitchs();
        uiGame.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(false);
        tutorialUi.gameObject.SetActive(false);
        FindAnyObjectByType<PlayerMovement>().Camera.SetActive(true);
        Soundmanager.Instance.MusicPLay();
        //mudar a camera e o pato, active ui, randomize switchs
    }
    private void Start()
    {
        instance = this;
    }
    public void FinishGame()
    {
        FindAnyObjectByType<CharacterController>().enabled = false;
        //Time.timeScale = 0.0f;
        string nextScene = "";
        nextScene = SwitchsManager.Instance.WinCondition() ? "Victory" : "Defeat";

        //aux.SetActive(true);
        timeRemaningStartText.text = nextScene;
        timeRemaningStartText.gameObject.SetActive(true);
        FadeCutscene.Instance.FadeIn(false, () =>
        {
            GoScene(nextScene);
        });
    }
    public void GoScene(string nextScene)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(nextScene);
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
