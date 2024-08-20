using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<PlayerData> players = new List<PlayerData>();

    public void SetPlayer(int playerID, int characterIndex, string playerName)
    {
        players[playerID].SetPlayerName(playerName);
        players[playerID].SetCharacter(characterIndex);
    }

    public void SpawnPlayer()
    {

    }
}
