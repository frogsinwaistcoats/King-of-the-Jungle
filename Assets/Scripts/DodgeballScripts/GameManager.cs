using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<PlayerMovement> players = new List<PlayerMovement>();
    public float roundDuration = 30f;
    private float roundTimer;
    private int currentTargetIndex = 0;
    private int roundsPlayed = 0;

    public List<Transform> spawnPoints;  // Reference the actual spawn points (children)
    public GameObject[] playerPrefabs; // The player prefabs
    private bool[] playerActive; // Track active players

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        players = new List<PlayerMovement>(FindObjectsOfType<PlayerMovement>());

        playerActive = new bool[spawnPoints.Count];

        // Move all players out of the scene initially
        foreach (GameObject player in playerPrefabs)
        {
            player.transform.position = new Vector3(1000, 1000, 1000); // Move players out of the scene
        }
        StartRound();
    }

    private void StartRound()
    {
        if (players.Count > 0)
        {
            roundTimer = roundDuration;
            SetTargetPlayer(currentTargetIndex);
            StartCoroutine(RoundTimer());
        }
    }

    private IEnumerator RoundTimer()
    {
        while (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
            yield return null;
        }

        EndRound();
    }

    private void EndRound()
    {
        players[currentTargetIndex].AddScore(roundDuration - roundTimer);
        DebugScores();

        currentTargetIndex = (currentTargetIndex + 1) % players.Count;
        roundsPlayed++;

        if (roundsPlayed >= players.Count)
        {
            EndGame();
        }
        else
        {
            StartRound();
        }
    }

    public void SetTargetPlayer(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerAiming playerAiming = players[i].GetComponent<PlayerAiming>();

            if (i == index)
            {
                players[i].isTarget = true;
                playerAiming.isShooter = false;
            }
            else
            {
                players[i].isTarget = false;
                playerAiming.isShooter = true;
            }

            // Set the player position based on the number of players
            players[i].transform.position = spawnPoints[i].position;
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int playerIndex = playerInput.playerIndex;

        if (playerIndex < spawnPoints.Count && !playerActive[playerIndex])
        {
            Debug.Log("Player " + playerIndex + " joining at spawn point: " + spawnPoints[playerIndex].position);

            // Activate the player object
            playerPrefabs[playerIndex].SetActive(true);

            // Set the player to the correct spawn point
            playerPrefabs[playerIndex].transform.position = spawnPoints[playerIndex].position;
            playerPrefabs[playerIndex].transform.rotation = spawnPoints[playerIndex].rotation;

            // Mark the player as active
            playerActive[playerIndex] = true;
        }
        else
        {
            Debug.LogWarning("Player " + playerIndex + " could not join. Either out of spawn points or already active.");
        }
    }


    public void PlayerHitTarget(int shooterID)
    {
        players[shooterID].AddScore(5f);
        EndRound();
    }

    public void EndGame()
    {
        Debug.Log("Game Over!");
        foreach (PlayerMovement player in players)
        {
            Debug.Log($"Player {player.playerID} score: {player.GetScore()}");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DebugScores()
    {
        foreach (PlayerMovement player in players)
        {
            Debug.Log($"Player {player.playerID} score: {player.GetScore()}");
        }
    }
}