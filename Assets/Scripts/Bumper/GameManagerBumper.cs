using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManagerBumper : MonoBehaviour
{
   
    public GameObject Boundary;

    public static GameManagerBumper instance;

    public TextMeshProUGUI[] playerFinishText;
    public TextMeshProUGUI gameFinishText;
    private static int finishedPlayers = 0;
    private static int totalPlayers;

    
    GameManager gameManager;
    SceneLoader sceneLoader;
    BumperTimer timer;
    public string playerTag = "Player"; // Tag for the player GameObject

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("has entered");
        if (other.tag == "Player" )
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag(playerTag))  // Check if the object entering is the player
        {
            // Get the PlayerTimer component from the player and stop the timer
            ScoreManagerBumper Scorebump = other.GetComponent<ScoreManagerBumper>();
            if (Scorebump != null)
            {
                Scorebump.StopTimer(); // Stop the player's timer
            }

        }
  
    }

  
}