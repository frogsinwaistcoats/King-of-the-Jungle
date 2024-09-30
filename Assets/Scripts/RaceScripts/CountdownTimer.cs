using System.Collections;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer instance;
   
    public float countdownDuration = 3f;
    public bool canMove;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        canMove = false;

        float currentTime = countdownDuration;

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        canMove = true;

    }


}
