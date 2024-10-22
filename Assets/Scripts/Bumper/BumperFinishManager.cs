using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BumperFinishManager : MonoBehaviour
{
    public static BumperFinishManager instance;

    public TextMeshProUGUI gameFinishText;
    public Image panel;

    private static int finishedPlayers = 0;  // Count the number of fallen players
    private static int totalPlayers;
    private PlayerData lastPlayerStanding;  // Track the last player standing

    GameManager gameManager;
    SceneLoader sceneLoader;

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.instance;
        sceneLoader = SceneLoader.instance;

        panel.enabled = false;
        gameFinishText.enabled = false;
    }

    private void Start()
    {
        totalPlayers = gameManager.players.Count;
        Debug.Log(totalPlayers);
    }

    public int PlayerFinish(int id)
    {
        finishedPlayers++;

        // Get the player based on ID
        PlayerData playerData = gameManager.players[id];

        Debug.Log($"Finished players: {finishedPlayers}");

        // If only one player is left
        if (finishedPlayers == totalPlayers - 1)
        {
            // Set the last player standing
            lastPlayerStanding = gameManager.players.Find(p => p.playerScore == 0 && p != playerData);
            AssignLastPlayerScore(); // Assign score to the last standing player
        }

        // Check if we have reached the end of the game (all players have fallen)
        if (finishedPlayers == totalPlayers)
        {
            GameFinish(); // Call the end of the game
        }

        return finishedPlayers;
    }

    private void AssignLastPlayerScore()
    {
        if (lastPlayerStanding != null)
        {
            // The last player gets the highest score, which is (totalPlayers - 1)
            lastPlayerStanding.SetPlayerScore(totalPlayers - 1);
            lastPlayerStanding.SetTotalScore(totalPlayers - 1);

            Debug.Log("Last player standing: Player " + lastPlayerStanding.playerID + " Score: " + lastPlayerStanding.playerScore);
        }
        else
        {
            Debug.LogError("No last player found to assign score!");
        }
    }

    public void GameFinish()
    {
        // Make sure scores are logged for all players
        foreach (var player in gameManager.players)
        {
            Debug.Log($"Player {player.playerID} Score: {player.playerScore}, Total Score: {player.totalScore}");
        }

        // Proceed to the score screen
        panel.enabled = false;
        gameFinishText.enabled = true;
        StartCoroutine(NextScene());
    }

    public int CalculateScore(int placing)
    {
        // Reverse the scoring so that the last player to fall gets the highest score
        int score = placing - 1;
        return score;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3); // Delay before moving to the score screen
        finishedPlayers = 0;  // Reset for the next game
        sceneLoader.SetPreviousScene();
        SceneManager.LoadScene("Scores");
    }
}