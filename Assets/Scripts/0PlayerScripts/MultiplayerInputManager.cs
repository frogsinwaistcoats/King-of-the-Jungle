using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class MultiplayerInputManager : MonoBehaviour
{
    public static MultiplayerInputManager instance;
    public List<IndividualPlayerControls> players = new List<IndividualPlayerControls>();
    int maxPlayers = 4;

    public InputControls inputControls;

    public delegate void OnPlayerJoined(int PlayerID);
    public OnPlayerJoined onPlayerJoined;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInputs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //sets up the inputs for detecting different players
    void InitializeInputs()
    {
        inputControls = new InputControls();

        inputControls.MasterControls.JoinButton.performed += JoinButtonPerformed;
        inputControls.Enable();
    }


    private void JoinButtonPerformed(InputAction.CallbackContext obj)
    {
        if (players.Count >= maxPlayers)
        {
            return;
        }


        //check if device is already assigned to a player
        foreach (IndividualPlayerControls player in players)
        {
            if (player.inputDevice == obj.control.device)
            {
                //this device is already assigned so we can return out of this function
                return;
            }
        }
        
        IndividualPlayerControls newPlayer = new IndividualPlayerControls();
        newPlayer.SetupPlayer(obj, players.Count);
        players.Add(newPlayer);

        CharacterSelect.instance.UIPrompts[newPlayer.playerID].SetActive(false);
        CharacterSelect.instance.characterSelections[newPlayer.playerID].SetActive(true);

        if (onPlayerJoined != null)
        {
            onPlayerJoined.Invoke(newPlayer.playerID);
        } 
    }

    public int PlayerCount
    {
        get { return players.Count; }
    }
}
