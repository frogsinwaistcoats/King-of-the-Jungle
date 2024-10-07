using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public List<PlayerData> players = new List<PlayerData>();

    public ControllerType player1ControllerType;

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

    public void ResetInstance()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    public void AddPlayer(PlayerData newPlayer, int playerID, int characterIndex, string characterName, ControllerType controllerType, Sprite charactersprite)
    {
        //PlayerData newPlayer = new PlayerData();
        newPlayer.SetCharacter(characterIndex);
        newPlayer.SetName(characterName);
        newPlayer.SetPlayerID(playerID);
        newPlayer.SetControllerType(controllerType);
        newPlayer.SetCharacterSprite(charactersprite);
        
        if(!players.Contains(newPlayer)) 
        {
            players.Add(newPlayer);
        }

    }
}

