using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerBumper : MonoBehaviour
{
    public static ScoreManagerBumper instance;
    public Text scoreText; // UI Text to display the score
    public string playerTag = "Player"; // Tag for the player GameObject
    public string onectTag = "Rock";
    void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the fall zone or leaves the arena
        if (other.CompareTag("Player"))
        {
            // Get the PlayerSurvivalTimer component from the player
            PlayerTimer playerTimer = other.GetComponent<PlayerTimer>();
            if (playerTimer != null)
            {
                // Stop the player's survival timer
                playerTimer.StopSurvival();
            }
            Debug.Log("has entered");
            if (other.tag == "Player")
            {
                Destroy(other.gameObject);
            }
            if (other.tag == "Rock")
            {
                Destroy(other.gameObject);

            }
        }

 

    }
}