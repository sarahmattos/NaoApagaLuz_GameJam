using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    bool hasCallEndDialogue = false; public string[] texts; // Textos do diálogo
    public TMP_Text dialogueText; // Campo de texto para exibir o diálogo
    public float typingSpeed = 0.05f; // Velocidade de digitação (em segundos por letra)

    public int currentTextIndex = -1; // Índice do texto atual
    private bool isTyping = false; // Controle para saber se está digitando no momento

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
                // Após o diálogo, inicia o fade e outras ações
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
            return false; // Não avança para o próximo texto
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
            return true; // Indica que o diálogo terminou
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter; // Adiciona uma letra por vez
            yield return new WaitForSeconds(typingSpeed); // Aguarda antes de adicionar a próxima
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
