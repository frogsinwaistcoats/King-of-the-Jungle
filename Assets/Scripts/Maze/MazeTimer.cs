using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeTimer : MonoBehaviour
{
    public static MazeTimer instance;

    public TextMeshProUGUI timerText;
    float startingTime;
    public float timeRemaining;
    public bool timerIsRunning;

    private void Awake()
    {
        instance = this;
        startingTime = timeRemaining;
        timerIsRunning = false;
        timerText.enabled = false;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining <= startingTime - 1)
            {
                timerText.enabled = true;
            }
            

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timerText.enabled = false;
                MazeFinishManager.instance.GameFinish();
                MazeFinishManager.instance.gameFinishText.enabled = true;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
