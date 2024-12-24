using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class Dialogue : MonoBehaviour
{
    public string[] texts; // Textos do di�logo
    public TMP_Text dialogueText; // Campo de texto para exibir o di�logo
    public float typingSpeed = 0.05f; // Velocidade de digita��o (em segundos por letra)

    public int currentTextIndex = -1; // �ndice do texto atual
    private bool isTyping = false; // Controle para saber se est� digitando no momento
    private bool hasFinished = false; // Controle para saber se o di�logo terminou

    public static Dialogue instance;

    private void Start()
    {
        instance = this;
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
            if (currentTextIndex == 12)
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
            hasFinished = true;
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
        hasFinished = false;
        dialogueText.text = "";
    }
}



