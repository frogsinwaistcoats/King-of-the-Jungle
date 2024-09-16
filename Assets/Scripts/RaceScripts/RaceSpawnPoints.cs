using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceSpawnPoints : MonoBehaviour
{
    //MazeCountdown mazeCountdown;

    public GameObject playerPrefab;
    public List<Camera> cameraObjects;
    public Transform[] spawnPoints;
    public Vector2[] cameraRect;

    public int playerCount;
    bool debugging;

    private void Awake()
    {
        //mazeCountdown = MazeCountdown.instance;

        if (GameManager.instance != null)
        {
            playerCount = GameManager.instance.players.Count;
        }
        else
        {
            SceneManager.LoadSceneAsync("ManagerScene", LoadSceneMode.Additive);
            debugging = true;
        }

        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject obj = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
            if (debugging ==false)
            {
                obj.GetComponentInChildren<PlayerStats>().UpdatePlayer(GameManager.instance.players[i]);
            }
            cameraObjects.Add(obj.GetComponentInChildren<Camera>());
            HandleCamera(i);
        }
    }

    void HandleCamera(int index)
    {
        cameraObjects[index].rect = new Rect(cameraRect[index].x, cameraRect[index].y, 0.5f, 0.5f);
    }
}
