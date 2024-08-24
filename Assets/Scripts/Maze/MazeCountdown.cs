using System.Collections;
using UnityEngine;
using TMPro;

public class MazeCountdown : MonoBehaviour
{
    public float countdownTime = 3f;
    public TextMeshProUGUI countdownText;
    public MonoBehaviour[] scriptsToDisable;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }

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
            countdownText.text = "Go!";
        }

        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = true;
        }

        if (countdownText != null)
        {
            yield return new WaitForSeconds(1f);
            countdownText.text = "";
        }
    }
}
