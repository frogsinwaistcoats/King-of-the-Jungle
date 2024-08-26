using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject playerPrefab;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<PlayerData> players = new List<PlayerData>();

    private void Awake()
    {
        instance = this;
    }

    public void AddPlayer(int playerID, int characterIndex, string playerName)
    {
        PlayerData newPlayer = new PlayerData();
        newPlayer.SetPlayerName(playerName);
        newPlayer.SetCharacter(characterIndex);
        newPlayer.SetPlayerID(playerID);

        players.Add(newPlayer);
    }

    public void SpawnPlayer()
    {
        foreach (PlayerData player in players)
        {
            GameObject obj = Instantiate(playerPrefab, spawnPoints[player.playerID].position, Quaternion.identity);
            obj.GetComponent<TutorialPlayerStats>().UpdatePlayer(players[player.playerID]);
        }
    }
}

