using System.Collections;
using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public static RaceTimer instance;

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
                RaceFinishManager.instance.RaceGameFinish();
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

    public void CancelTimer()
    {
        timeRemaining = 0;
        timerText.enabled = false;
    }

    //public static RaceTimer instance;

    //public TextMeshProUGUI timerText;
    //public float remainingTime;
    //float startDelay = 4f;
    //public bool timerIsRunning;

    //RaceFinishManager finishRace;

    //private void Awake()
    //{
    //    instance = this;
    //    finishRace = RaceFinishManager.instance;

    //    if (finishRace == null )
    //    {
    //        finishRace = FindObjectOfType<RaceFinishManager>();
    //    }

    //    timerIsRunning = false;
    //    timerText.enabled = false;
    //}

    //void Start()
    //{
    //    StartCoroutine(StartTimerWithDelay());
    //}

    //private IEnumerator StartTimerWithDelay()
    //{
    //    yield return new WaitForSeconds(startDelay);
    //    timerIsRunning = true;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (timerIsRunning)
    //    {
    //        if (remainingTime > 0)
    //        {
    //            remainingTime -= Time.deltaTime;
    //        }
    //        else if (remainingTime < 0)
    //        {
    //            remainingTime = 0;
    //            timerIsRunning = false;
    //            timerText.enabled = false;
    //            finishRace.RaceGameFinish();
    //            finishRace.gameFinishText.enabled = true;
    //            //GameOver();
    //            //timerText.color = Color.red;
    //        }
    //        int minutes = Mathf.FloorToInt(remainingTime / 60);
    //        int seconds = Mathf.FloorToInt(remainingTime % 60);
    //        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    //    }
    //}

    //public void CancelTimer()
    //{
    //    remainingTime = 0;
    //    timerText.enabled = false;
    //}
}