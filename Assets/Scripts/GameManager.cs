using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    
    public List<PlayerData> players = new List<PlayerData>();

    [Header("Maze")]
    public GameObject mazePlayerPrefab;
    public Transform[] mazeSpawnPoints;

    //[Header("Race")]
    //public GameObject racePlayerPrefab;
    //public List<Transform> raceSpawnPoints = new List<Transform>();

    //[Header("Dodgeball")]
    //public GameObject dodgeballPlayerPrefab;
    //public List<Transform> dodgeballSpawnPoints = new List<Transform>();

    //[Header("Bumper")]
    //public GameObject bumperPlayerPrefab;
    //public List<Transform> bumperSpawnPoints = new List<Transform>();

    //[Header("TugOWar")]
    //public GameObject tugOWarPlayerPrefab;
    //public List<Transform> tugOWarSpawnPoints = new List<Transform>();


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

    public void AddPlayer(int playerID, int characterIndex, string playerName)
    {
        PlayerData newPlayer = new PlayerData();
        newPlayer.SetPlayerName(playerName);
        newPlayer.SetCharacter(characterIndex);
        newPlayer.SetPlayerID(playerID);

        players.Add(newPlayer);
    }
}

