using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Door : MonoBehaviour
{
    int clickCount = 10;
    int maxClick = 10;
    [SerializeField] private TMP_Text countText;
    private void OnEnable()
    {
        countText.text ="Clicks remaining "+ clickCount.ToString();
    }
    private void OnMouseDown()
    {
        clickCount--;
        countText.text = "Clicks remaining " + clickCount.ToString();
        if (clickCount == 0)
        {
            Reset();
        }
    }
    public void Reset()
    {
        clickCount = maxClick;
        this.gameObject.SetActive(false);
    }
}
