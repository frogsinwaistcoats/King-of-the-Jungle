using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    
    public List<PlayerData> players = new List<PlayerData>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(int playerID, int characterIndex)
    {
        PlayerData newPlayer = new PlayerData();
        newPlayer.SetCharacter(characterIndex);
        newPlayer.SetPlayerID(playerID);

        players.Add(newPlayer);
    }
}

