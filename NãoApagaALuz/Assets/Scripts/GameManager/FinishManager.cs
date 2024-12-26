using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    bool hasCallEndDialogue = false; public string[] texts; // Textos do di�logo
    public TMP_Text dialogueText; // Campo de texto para exibir o di�logo
    public float typingSpeed = 0.05f; // Velocidade de digita��o (em segundos por letra)

    public int currentTextIndex = -1; // �ndice do texto atual
    private bool isTyping = false; // Controle para saber se est� digitando no momento

    public static FinishManager instance;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (HandleDialogue())
            {
                // Ap�s o di�logo, inicia o fade e outras a��es
                if (!hasCallEndDialogue)
                {
                    hasCallEndDialogue = true;
                    FadeCutscene.Instance.FadeIn(false, () =>
                    {
                        ThingsToDoToStartGame();
                    });
                    Debug.Log("acabou");
                }

            }

        }
    }
    public void ThingsToDoToStartGame()
    {
        Cursor.lockState = CursorLockMode.None;
        BackMenu();
    }
    public void BackMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public bool HandleDialogue()
    {
        if (isTyping)
        {
            // Completa o texto atual se ainda estiver digitando
            StopAllCoroutines();
            dialogueText.text = texts[currentTextIndex]; // Mostra o texto completo
            dialogueText.GetComponent<TextMeshProEffect>().enabled = false;
            isTyping = false;
            return false; // N�o avan�a para o pr�ximo texto
        }
        else
        {
            dialogueText.GetComponent<TextMeshProEffect>().enabled = true;
            if (currentTextIndex == texts.Length-1)
            {
                return true;
            }
            else
            {

                return ShowNextText();
            }
        }
    }

    private bool ShowNextText()
    {
        if (currentTextIndex < texts.Length)
        {
            currentTextIndex++;
            StartCoroutine(TypeText(texts[currentTextIndex]));
            return false;
        }
        else
        {
            dialogueText.text = "";
            return true; // Indica que o di�logo terminou
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter; // Adiciona uma letra por vez
            yield return new WaitForSeconds(typingSpeed); // Aguarda antes de adicionar a pr�xima
        }
        dialogueText.GetComponent<TextMeshProEffect>().enabled = false;
        isTyping = false;
    }

    public void ResetDialogue()
    {
        currentTextIndex = 0;
        dialogueText.text = "";
    }
   
}
