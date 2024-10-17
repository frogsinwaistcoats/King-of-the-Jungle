using System.Collections;
using UnityEngine;
using TMPro;

public class RaceTimerText : MonoBehaviour
{
    public static RaceTimerText instance;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] float startDelay = 4f;

    public bool isTiming = false;

    RaceFinishManager finishRace;

    private void Awake()
    {
        instance = this;

        finishRace = RaceFinishManager.instance;

        if (finishRace == null )
        {
            finishRace = FindObjectOfType<RaceFinishManager>();
        }
        
        timerText.enabled = false;
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
            timerText.enabled = true;

            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0;
                isTiming = false;
                timerText.text = "";
                finishRace.RaceGameFinish();
                
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void CancelTimer()
    {
        timerText.enabled = false;
        remainingTime = 0;
    }
}