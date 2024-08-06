using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Used for the unity's new input system

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public static InputControls inputControls; //Controls that we created through the Unity's input system
    [SerializeField] int maxPlayers = 4; //Amount of players that can be connected
    public List<IndividualDevice> players = new List<IndividualDevice>(); //List of all the players and the device that is assigned to them

    //This delegate can be used for scripts checking/waiting for a player to join
    public delegate void OnPlayerJoined(int playerID);
    public OnPlayerJoined onPlayerJoined;

    private void Awake()
    {
        //Check if a input manager already exists
        if (instance == null)
        {
            //Set it up if it doesn't
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInputs();
        }
        else
        {
            //Destroy this one as one already exists
            Destroy(gameObject);
        }
    }

    private void InitializeInputs()
    {
        inputControls = new InputControls();
        inputControls.Player.StartButton.performed += PlayerJoined;
        inputControls.Enable();
    }

    private void PlayerJoined(InputAction.CallbackContext obj)
    {
        //Check if the max amount of players have joined
        if (players.Count >= maxPlayers)
        {
            return;
        }

        //Check if the device is already registered to a player
        foreach (IndividualDevice individualDevice in players)
        {
            if (individualDevice.inputDevice == obj.control.device)
            {
                //This device is already registered so we can return out of this function
                return;
            }
        }

        //Since this device hasn't been registered to any player we can go ahead and create a new player
        IndividualDevice newPlayer = new IndividualDevice();
        newPlayer.SetUpPlayer(obj, players.Count); //send through the device information and the current player count for the players ID

        players.Add(newPlayer); //Add the new player to our list of players

        if (onPlayerJoined != null)
        {
            onPlayerJoined.Invoke(newPlayer.playerID);
        }
    }
}
