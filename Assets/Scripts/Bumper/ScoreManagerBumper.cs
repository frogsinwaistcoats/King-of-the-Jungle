using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerBumper : MonoBehaviour
{
    public static ScoreManagerBumper instance;
    public Text scoreText; // UI Text to display the score
    public string playerTag = "Player"; // Tag for the player GameObject

    void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("has entered");
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag(playerTag))  // Check if the object entering is the player
        {
            // Get the PlayerTimer component from the player and stop the timer
            PlayerTimer playerTimer = other.GetComponent<PlayerTimer>();
            if (playerTimer != null)
            {
                playerTimer.StopTimer(); // Stop the player's timer
            }
            HandleScore();// Handle the score (you can apply some score penalty or logic here)
        }
    }

    private void HandleScore()
    {
       Debug.Log ("Player has fallen!");
    }
}