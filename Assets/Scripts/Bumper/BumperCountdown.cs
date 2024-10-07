using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BumperCountdown : MonoBehaviour
{
    public static BumperCountdown instance;

    public float countdownTime = 3f;
    public TextMeshProUGUI countdownText;
    public bool isRunning;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        isRunning = true;

        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = currentTime.ToString("0");
            }
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        if (countdownText != null)
        {
            isRunning = false;
            countdownText.text = "Go!";
            BumperTimer.instance.timerIsRunning = true;
        }

        if (countdownText != null)
        {
            yield return new WaitForSeconds(1f);
            countdownText.text = "";
        }
    }
}
