using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<PlayerData> players = new List<PlayerData>();

    public void SetPlayer(int playerID, int characterIndex, string playerName)
    {
        players[playerID].SetPlayerName(playerName);
        players[playerID].SetCharacter(characterIndex);
        players[playerID].SetPlayerID(playerID);
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
