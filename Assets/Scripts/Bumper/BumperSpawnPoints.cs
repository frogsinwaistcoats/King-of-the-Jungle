using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSpawnPoints : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    public int playerCount;

    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = SceneLoader.instance;
    }

    private void Start()
    {
        if (GameManager.instance != null)
        {
            playerCount = GameManager.instance.players.Count;
        }

        SpawnPlayers();
    }
    
    void SpawnPlayers()
    {
        sceneLoader.SetPreviousScene();

        for (int i = 0; i < playerCount; i++)
        {
            GameObject obj = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
            obj.GetComponentInChildren<PlayerStats>().UpdatePlayer(GameManager.instance.players[i]);
        }
    }
}
