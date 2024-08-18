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

    private bool allPlayersReady = false;

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
        // Ensure that InstantiatePlayers is only called when players join
        MultiplayerInputManager.instance.onPlayerJoined += InstantiatePlayers;

        // Start a coroutine that checks every second if all players are ready
        StartCoroutine(WaitForPlayersToBeReady());
    }

    private IEnumerator WaitForPlayersToBeReady()
    {
        while (!allPlayersReady)
        {
            if (players.Count >= 2) // Minimum two players to start the game
            {
                allPlayersReady = true;
                StartRound();
            }
            else
            {
                Debug.Log("Not enough players, waiting 5 more seconds...");
                yield return new WaitForSeconds(5f);
            }
        }
    }

    //public bool HasValidControls()
    //{
    //    return playerControls != null;
    //}

    void InstantiatePlayers(int playerID)
    {
        // Ensure playerID is within bounds
        if (playerID >= PlayerManager.Instance.selectedPrefabsIndex.Count)
        {
            Debug.LogError("Player ID exceeds the number of selected prefabs.");
            return;
        }

        // Create the player instance
        int prefabIndex = PlayerManager.Instance.selectedPrefabsIndex[playerID];
        GameObject player = Instantiate(PlayerManager.Instance.playerPrefabs[prefabIndex], spawnPoints[playerID].position, Quaternion.identity);

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.playerID = playerID;

        // Assign inputs
        playerMovement.AssignInputs(playerID);

        // Add to players list
        players.Add(playerMovement);
    }

    private void StartRound()
    {
        if (players.Count > 0)
        {
            roundTimer = roundDuration;
            SetTargetPlayer(currentTargetIndex);
            StartCoroutine(RoundTimer());
        }
        else
        {
            Debug.LogError("No players are present to start the round");
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

    private void SetTargetPlayer(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerAiming playerAiming = players[i].GetComponent<PlayerAiming>();
            PlayerMovement playerMovement = players[i].GetComponent<PlayerMovement>();

            if (playerMovement.playerControls == null)
            {
                Debug.LogError($"playerControls is null for player {i} (ID: {players[i].playerID}).");
            }

            if (i == index)
            {
                players[i].isTarget = true;
                playerAiming.isShooter = false;
                playerMovement.EnableMovementControls(); // Make sure the target can move
            }
            else
            {
                players[i].isTarget = false;
                playerAiming.isShooter = true;
                playerMovement.DisableMovementControls(); // Make sure shooters cannot move but can aim
            }

            // Set the player position based on the spawn points
            players[i].transform.position = spawnPoints[i].position;
            players[i].transform.rotation = spawnPoints[i].rotation;
        }
    }

    public void PlayerHitTarget(int shooterID)
    {
        // Check if the shooterID is valid
        if (shooterID >= 0 && shooterID < players.Count)
        {
            // Calculate score
            float scoreToAdd = 5; // 5 base points 
            players[shooterID].AddScore(scoreToAdd);
            Debug.Log($"Shooter {shooterID} hit the target and earned {scoreToAdd} points!");

            // End the round and possibly reset positions
            EndRound();
        }
        else
        {
            Debug.LogError("Invalid shooter ID, cannot assign score.");
        }
    }

    private void EndRound()
    {
        // Score and debug
        players[currentTargetIndex].AddScore(roundDuration - roundTimer);
        DebugScores();

        // Rotate the spawn points for new positions
        RotateSpawnPoints();

        // Calculate new target index and continue the game or end it
        currentTargetIndex = (currentTargetIndex + 1) % players.Count;
        roundsPlayed++;

        if (roundsPlayed >= players.Count)
        {
            EndGame();
        }
        else
        {
            RespawnPlayers();
            StartRound();
        }
    }

    private void RotateSpawnPoints()
    {
        Transform firstSpawn = spawnPoints[0];
        spawnPoints.RemoveAt(0);
        spawnPoints.Add(firstSpawn);  // Move the first to the last

        // Reassign player positions based on new order
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoints[i].position;
            players[i].transform.rotation = spawnPoints[i].rotation;
        }
    }

    private void RespawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoints[i].position;
            players[i].transform.rotation = spawnPoints[i].rotation;

            PlayerAiming playerAiming = players[i].GetComponent<PlayerAiming>();
            PlayerMovement playerMovement = players[i].GetComponent<PlayerMovement>();

            if (i == currentTargetIndex)
            {
                players[i].isTarget = true;
                playerAiming.isShooter = false;
                playerMovement.EnableMovementControls();  // Ensure this method exists and correctly sets up movement controls
            }
            else
            {
                players[i].isTarget = false;
                playerAiming.isShooter = true;
                playerMovement.DisableMovementControls();  // Ensure this method exists and disables movement controls, enabling aiming
            }

            playerAiming.SetupControls();  // Setup controls based on new role
        }
    }

    private void DebugScores()
    {
        foreach (PlayerMovement player in players)
        {
            Debug.Log($"Player {player.playerID} score: {player.GetScore()}");
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("Scores");
    }
}