using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class FadeCutscene : MonoBehaviour
{
    public static FadeCutscene Instance { get; private set; }
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    private Coroutine currentCoroutine;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        FadeOut();
    }

    public void FadeIn(bool callFadeOut = false, Action onComplete = null)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(Fade(0f, 1f, callFadeOut, onComplete));
    }

    public void FadeOut(Action onComplete = null)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(Fade(1f, 0f, false, onComplete));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, bool callFadeOut = false, Action onComplete = null)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.interactable = endAlpha > 0;
        canvasGroup.blocksRaycasts = endAlpha > 0;

        if (callFadeOut && endAlpha == 1f)
        {
            FadeOut(onComplete);
            GameManager.instance.ThingsToDoToStartGame();
        }
        else
        {
            onComplete?.Invoke();
        }
    }
    
}