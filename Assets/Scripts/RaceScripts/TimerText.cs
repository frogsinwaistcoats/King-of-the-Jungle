using System.Collections;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] float startDelay = 4f;

    private bool isTiming = false;

    FinishRace finishRace;

    private void Awake()
    {
        finishRace = FinishRace.instance;

        if (finishRace == null )
        {
            finishRace = FindObjectOfType<FinishRace>();
        }
    }

    void Start()
    {
        StartCoroutine(StartTimerWithDelay());
    }

    private IEnumerator StartTimerWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        isTiming = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                finishRace.RaceGameFinish();
                //GameOver();
                timerText.color = Color.red;
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }
}