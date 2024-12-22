using System.Collections;
using UnityEngine;
using TMPro;

public class RegressiveCountdown : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText; 
    [SerializeField] private int startMinutes = 1; 
    [SerializeField] private int startSeconds = 0; 

    private float remainingTime;

    private void Start()
    {
        remainingTime = (startMinutes * 60) + startSeconds;
    }
    public void StartCouroutineTime()
    {

        StartCoroutine(StartCountdown());
    }
    private IEnumerator StartCountdown()
    {
        while (remainingTime > 0)
        {
            UpdateTimerText();

            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        remainingTime = 0;
        UpdateTimerText();
        Debug.Log("Contagem regressiva finalizada!");
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
