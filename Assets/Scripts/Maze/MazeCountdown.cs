using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MazeCountdown : MonoBehaviour
{
    public static MazeCountdown instance;

    public float countdownTime = 3f;
    public TextMeshProUGUI countdownText;
    public bool isRunning;
    //public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();

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
        }

        if (countdownText != null)
        {
            yield return new WaitForSeconds(1f);
            countdownText.text = "";
        }
    }
}
