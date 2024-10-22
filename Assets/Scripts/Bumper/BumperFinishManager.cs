using System.Collections;
using System.Linq;
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
        //Debug.Log(totalPlayers);
    }

    public int PlayerFinish(int id)
    {
        finishedPlayers++;

        // Get the player based on ID
        PlayerData playerData = gameManager.players[id];

        //Debug.Log($"Player {playerData.playerID} fell! They are placed {finishedPlayers}");

        // Set the score for the current player based on when they fell
        playerData.SetPlayerScore(finishedPlayers - 1);  // Assign the score based on when the player fell
        playerData.SetTotalScore(finishedPlayers - 1);

        // Check if there's only one player left
        if (finishedPlayers == totalPlayers - 1)
        {
            AssignLastPlayerScore();  // Assign score for the last player standing
            GameFinish();  // End the game
        }

        return finishedPlayers;
    }

    private void AssignLastPlayerScore()
    {
        // Find the last player standing who hasn't been assigned a score yet
        PlayerData lastPlayer = gameManager.players.FirstOrDefault(p => p.playerScore == 0 && p.totalScore == 0);

        if (lastPlayer != null)
        {
            // The last player gets the highest score, which is (totalPlayers - 1)
            lastPlayer.SetPlayerScore(totalPlayers - 1);
            lastPlayer.SetTotalScore(totalPlayers - 1);

            //Debug.Log("Last player standing: Player " + lastPlayer.playerID + " Score: " + lastPlayer.playerScore);
        }
    }
    public void GameFinish()
    {
        // Log scores for debugging
        foreach (var player in gameManager.players)
        {
            //Debug.Log($"Player {player.playerID} final score: {player.playerScore}, Total Score: {player.totalScore}");
        }

        // Proceed to the score screen
        panel.enabled = false;
        gameFinishText.enabled = true;
        StartCoroutine(NextScene());
    }

    public int CalculateScore(int placing)
    {
        // Reverse the scoring so that last player to fall gets the highest score
        int score = placing-1;
        return score;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3); // Delay before moving to score scene
        finishedPlayers = 0;  // Reset for the next game
        sceneLoader.SetPreviousScene();
        SceneManager.LoadScene("Scores");
    }
}