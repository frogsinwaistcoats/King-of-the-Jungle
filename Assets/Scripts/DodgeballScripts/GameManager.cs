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
        playerActive = new bool[spawnPoints.Count];
        StartRound();
    }

    private void InstantiatePlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.selectedPrefabsIndex.Count; i++)
        {
            int prefabIndex = PlayerManager.Instance.selectedPrefabsIndex[i];
            GameObject player = Instantiate(PlayerManager.Instance.playerPrefabs[prefabIndex], spawnPoints[i].position, Quaternion.identity);
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.playerID = i;
            players.Add(playerMovement);
            playerActive[i] = true;
        }
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
            RespawnPlayers();
            StartRound();
        }
    }

    private void RespawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoints[i].position;
            players[i].transform.rotation = spawnPoints[i].rotation;
        }
    }

    private void SetTargetPlayer(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerAiming playerAiming = players[i].GetComponent<PlayerAiming>();
            playerAiming.isShooter = (i != index);
            players[i].isTarget = (i == index);
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