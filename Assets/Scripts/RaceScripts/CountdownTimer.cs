using System.Collections;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
   public float countdownDuration = 10f;
   private float countdownTimer;
   private bool canMove = false;

   void Start()
   {
        countdownTimer = countdownDuration;
        StartCoroutine(StartCountdown());
   }

   IEnumerator StartCountdown()
   {
        while (countdownTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            countdownTimer -= 1f;
        }
        countdownTimer = 2f;
        canMove = true;
   }

   public bool CanMove()
   {
        return canMove;
   }


}
