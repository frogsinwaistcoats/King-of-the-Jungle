using System.Collections;
using TMPro;
using UnityEngine;

public class BumperCountdown : MonoBehaviour
{
    public static BumperCountdown instance;

    public float countdownTime = 3f;
    public TextMeshProUGUI countdownText;
    public bool isRunning;
    public bool canMove = false; // Add this flag to control player movement

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
        canMove = false; // Disable player movement initially

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
            canMove = true; // Enable player movement when countdown finishes
            BumperTimer.instance.timerIsRunning = true;
        }

        if (countdownText != null)
        {
            yield return new WaitForSeconds(1f);
            countdownText.text = "";
        }
    }
}