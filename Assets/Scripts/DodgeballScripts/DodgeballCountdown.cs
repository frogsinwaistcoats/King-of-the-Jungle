using System.Collections;
using UnityEngine;
using TMPro;

public class DodgeballCountdown : MonoBehaviour
{
    public static DodgeballCountdown instance;

    public float countdownDuration = 3f;    // Duration of the countdown
    public TextMeshProUGUI countdownText;   // UI text element to display the countdown
    public bool canStartGame;               // Whether the game can start

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    // Coroutine to manage the countdown
    public IEnumerator StartCountdown()
    {
        canStartGame = false;
        DodgeballPlayerManager.instance.SetPlayerControls(false);  // Disable movement and shooting

        float currentTime = countdownDuration;

        while (currentTime > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = currentTime.ToString("0");  // Display countdown
            }
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        if (countdownText != null)
        {
            countdownText.text = "Go!";
        }

        yield return new WaitForSeconds(1f);
        if (countdownText != null)
        {
            countdownText.text = "";
        }

        DodgeballPlayerManager.instance.SetPlayerControls(true);  // Re-enable movement and shooting
        canStartGame = true;        // Game can now start
    }
}

//    // Method to disable player controls (both movement and shooting)
//    private void DisableAllPlayerControls()
//    {
//        DodgeballPlayerMovement.instance.DisableMovementControls();
//        PlayerAiming.instance.DisableAimingControls();  // Assuming your aiming script has this function
//    }

//    // Method to enable player controls after countdown
//    private void EnableAllPlayerControls()
//    {
//        DodgeballPlayerMovement.instance.EnableMovementControls();
//        PlayerAiming.instance.EnableAimingControls();  // Assuming your aiming script has this function
//    }
//}