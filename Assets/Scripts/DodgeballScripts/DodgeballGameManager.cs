using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DodgeballGameManager : MonoBehaviour
{
    public static DodgeballGameManager instance;

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
        GameObject player = Instantiate(PlayerManager.Instance.playerPrefabs[prefabIndex], spawnPoints[0].position, Quaternion.identity); // Default to the target spawn point initially

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.playerID = playerID;

        // Assign inputs (for movement and aiming)
        playerMovement.AssignInputs(playerID);
        PlayerAiming playerAiming = player.GetComponent<PlayerAiming>();
        playerAiming.AssignInputs(playerID);

        // Add to players list
        players.Add(playerMovement);
    }


    private void StartRound()
    {
        if (players.Count > 0)
        {
            roundTimer = roundDuration;
            ReassignPlayerRolesAndSpawnPoints();
            RespawnPlayers(); // Make sure roles are set up before the round starts
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

    private void ReassignPlayerRolesAndSpawnPoints()
    {
        // Custom logic based on player count
        if (players.Count == 2)
        {
            AssignTwoPlayerSpawns();
        }
        else if (players.Count == 3)
        {
            AssignThreePlayerSpawns();
        }
        else if (players.Count == 4)
        {
            AssignFourPlayerSpawns();
        }
    }

    private void AssignTwoPlayerSpawns()
    {
        if (currentTargetIndex == 0)
        {
            // Player 1 becomes shooter, Player 2 becomes target
            players[0].transform.position = spawnPoints[2].position; // Move to Point C
            players[1].transform.position = spawnPoints[0].position; // Move to Point A
        }
        else
        {
            // Player 2 becomes shooter, Player 1 becomes target
            players[0].transform.position = spawnPoints[0].position; // Move to Point A
            players[1].transform.position = spawnPoints[2].position; // Move to Point C
        }
    }

    private void AssignThreePlayerSpawns()
    {
        if (currentTargetIndex == 0)
        {
            players[0].transform.position = spawnPoints[1].position; // Move to Point B
            players[1].transform.position = spawnPoints[3].position; // Move to Point D
            players[2].transform.position = spawnPoints[0].position; // Move to Point A
        }
        else if (currentTargetIndex == 1)
        {
            players[0].transform.position = spawnPoints[3].position; // Move to Point D
            players[1].transform.position = spawnPoints[0].position; // Move to Point A
            players[2].transform.position = spawnPoints[1].position; // Move to Point B
        }
        else
        {
            players[0].transform.position = spawnPoints[0].position; // Move to Point A
            players[1].transform.position = spawnPoints[1].position; // Move to Point B
            players[2].transform.position = spawnPoints[3].position; // Move to Point D
        }
    }

    private void AssignFourPlayerSpawns()
    {
        Transform temp = spawnPoints[0];
        spawnPoints[0] = spawnPoints[3];
        spawnPoints[3] = spawnPoints[2];
        spawnPoints[2] = spawnPoints[1];
        spawnPoints[1] = temp;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoints[i].position;
        }
    }

    private void SetTargetPlayer(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerAiming playerAiming = players[i].GetComponent<PlayerAiming>();
            PlayerMovement playerMovement = players[i].GetComponent<PlayerMovement>();

            if (i == index)
            {
                // Make this player the target
                players[i].isTarget = true;
                playerAiming.isShooter = false;
                playerMovement.EnableMovementControls(); // Ensure they can move

                // Ensure the player spawns at Point A
                players[i].transform.position = spawnPoints[0].position;
                players[i].transform.rotation = spawnPoints[0].rotation;
            }
            else
            {
                // Make other players shooters
                players[i].isTarget = false;
                playerAiming.isShooter = true;
                playerMovement.DisableMovementControls(); // Shooters should not move

                // Update spawn point for shooters based on rotation logic
                int shooterSpawnIndex = (i < index) ? i + 1 : i;
                players[i].transform.position = spawnPoints[shooterSpawnIndex].position;
                players[i].transform.rotation = spawnPoints[shooterSpawnIndex].rotation;
            }

            playerAiming.SetupControls(); // Ensure controls are set correctly for each role
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

        // Calculate new target index and continue the game or end it
        currentTargetIndex = (currentTargetIndex + 1) % players.Count;
        roundsPlayed++;

        if (roundsPlayed >= players.Count)
        {
            EndGame();
        }
        else
        {
            RotateSpawnPointsDown(); // Swap the spawn points before respawning players
            RespawnPlayers(); // Reassign roles and reset controls
            StartRound();
        }
    }

    private void RotateSpawnPointsDown()
    {
        // Store the last spawn point temporarily
        Transform lastSpawn = spawnPoints[spawnPoints.Count - 1];

        // Shift all spawn points downward by one position
        for (int i = spawnPoints.Count - 1; i > 0; i--)
        {
            spawnPoints[i] = spawnPoints[i - 1];
        }

        // Move the original last spawn point to the first position
        spawnPoints[0] = lastSpawn;
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