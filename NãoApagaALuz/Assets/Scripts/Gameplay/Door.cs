using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Door : MonoBehaviour
{
    int clickCount = 20;
    int maxClick = 20;
    float timeDoor = 12;

    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text timeRemaningDoorText;

    private float timeDoorRemaining;
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
    public IEnumerator ActiveDoor()
    {
        gameObject.SetActive(true);

        timeDoorRemaining = timeDoor;

        while (timeDoorRemaining > 0)
        {
            timeDoorRemaining -= Time.deltaTime;
            timeRemaningDoorText.text = "Time remaining " + Mathf.Floor(timeDoorRemaining).ToString();

            yield return null;
        }


        if (gameObject.activeSelf)
        {
            Reset();
        }
    }
}
