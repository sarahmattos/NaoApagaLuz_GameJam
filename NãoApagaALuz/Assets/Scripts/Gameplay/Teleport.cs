using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public bool canPass = false;
    public Slider sliderTp;
    public float totalTime = 15f;
    private float timeElapsed = 0f;
    void Start()
    {
        sliderTp.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.startedGame) return;
        if (sliderTp.value < 1f)
        {
            timeElapsed += Time.deltaTime;
            sliderTp.value = timeElapsed / totalTime;
        }

        if (sliderTp.value >= 1f && !canPass)
        {
            canPass = true;
            sliderTp.gameObject.SetActive(false);
        }
    }
    public void ResetTp()
    {
        canPass = false;
        timeElapsed = 0f;
        sliderTp.gameObject.SetActive(true);
        sliderTp.value = 0;
    }
}
