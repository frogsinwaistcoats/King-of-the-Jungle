using System.Collections;
using UnityEngine;
using TMPro;

public class CountDownText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float remainingTime = 10f;
    
    private bool isCountingDownFinished = false; // Flag to ensure the coroutine runs only once

    private void Update()
    {
        if (remainingTime <= 0)
        {
            if (!isCountingDownFinished)
            {
                StartCoroutine(ShowGoAndClearTextAfterDelay());
                isCountingDownFinished = true; // Set the flag to prevent re-running the coroutine
            }
            return;
        }

        remainingTime -= Time.deltaTime;
        int timeInt = Mathf.FloorToInt(remainingTime);

        switch (timeInt)
        {
            case 3:
                SetCountdownText("3", Color.red);
                break;
            case 2:
                SetCountdownText("2", Color.yellow);
                break;
            case 1:
                SetCountdownText("1", Color.green);
                break;
            default:
                if (timeInt < 1)
                {
                    SetCountdownText("GO!!!", Color.green);
                }
                break;
        }
    }

    private IEnumerator ShowGoAndClearTextAfterDelay()
    {
        countdownText.text = "GO!!!";
        countdownText.color = Color.green;
        yield return new WaitForSeconds(1f); // Wait for 1 second
        countdownText.text = ""; // Clear the text
    }

    private void SetCountdownText(string text, Color color)
    {
        countdownText.text = text;
        countdownText.color = color;
    }
}
