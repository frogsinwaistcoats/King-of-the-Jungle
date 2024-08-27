using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Camera> cameraObjects;
    public Transform[] spawnPoints;
    public Vector2[] cameraRect;

    public int playerCount;

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
        for (int i = 0; i < playerCount; i++)
        {
            GameObject obj = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
            cameraObjects.Add(obj.GetComponentInChildren<Camera>());
            HandleCamera(i);
            //obj.GetComponent<TutorialPlayerStats>().UpdatePlayer(players[player.playerID]);
        }
    }

    void HandleCamera(int index)
    {
        cameraObjects[index].rect = new Rect(cameraRect[index].x, cameraRect[index].y, 0.5f, 0.5f);
    }
}
