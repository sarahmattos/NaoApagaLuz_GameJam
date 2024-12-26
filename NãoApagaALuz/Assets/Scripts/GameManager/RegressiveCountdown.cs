using System.Collections;
using UnityEngine;
using TMPro;

public class RegressiveCountdown : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int startMinutes = 1;
    [SerializeField] private int startSeconds = 0;
    [SerializeField] private Color defaultColor = Color.white; // Cor padrão do texto
    [SerializeField] private Color warningColor = Color.red;   // Cor de alerta
    [SerializeField] private float warningTime = 10f;          // Tempo em segundos para começar o alerta

    private float remainingTime;
    private bool isWarningActive = false;

    bool setSoundRemaing = false;

    private void Start()
    {
        remainingTime = (startMinutes * 60) + startSeconds;
        timerText.color = defaultColor; // Inicia com a cor padrão
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

            // Ativa o modo de alerta quando o tempo está próximo do fim
            if (remainingTime <= warningTime && !isWarningActive)
            {
                isWarningActive = true;
                StartCoroutine(FlashWarning());
                if (!setSoundRemaing)
                {
                    Soundmanager.Instance.TimeRemaingSound();
                    setSoundRemaing = true;
                }
             }

                yield return new WaitForSeconds(1);
            remainingTime--;
        }

        remainingTime = 0;
        UpdateTimerText();
        Debug.Log("Contagem regressiva finalizada!");
        GameManager.instance.FinishGame();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private IEnumerator FlashWarning()
    {
        while (remainingTime > 0 && remainingTime <= warningTime)
        {
            timerText.color = warningColor;
            yield return new WaitForSeconds(0.5f);
            timerText.color = defaultColor;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
