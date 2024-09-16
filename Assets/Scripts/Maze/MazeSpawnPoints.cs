using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawnPoints : MonoBehaviour
{
    MazeCountdown mazeCountdown;

    public static MazeSpawnPoints instance;

    public GameObject playerPrefab;
    public List<Camera> cameraObjects;
    public Transform[] spawnPoints;
    public Vector2[] cameraRect;

    public int playerCount;

    private void Start()
    {

        mazeCountdown = MazeCountdown.instance;

        if (GameManager.instance != null)
        {
            playerCount = GameManager.instance.players.Count;
        }
        else
        {
            Debug.Log("Game Manager is null");
        }

        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject obj = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
            obj.GetComponentInChildren<PlayerStats>().UpdatePlayer(GameManager.instance.players[i]);
            cameraObjects.Add(obj.GetComponentInChildren<Camera>());
            HandleCamera(i);
        }
    }

    void HandleCamera(int index)
    {
        cameraObjects[index].rect = new Rect(cameraRect[index].x, cameraRect[index].y, 0.5f, 0.5f);
    }
}
