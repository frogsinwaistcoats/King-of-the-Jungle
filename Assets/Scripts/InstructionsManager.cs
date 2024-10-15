using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InstructionsManager : MonoBehaviour
{
    
    public InputControls inputControls;
    int playerID;

    GameManager gameManager;
    MultiplayerInputManager inputManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        inputManager = MultiplayerInputManager.instance;
        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }

        GameObject continueButton = GameObject.Find("ContinueText");
        TextMeshProUGUI text = continueButton.GetComponentInChildren<TextMeshProUGUI>();

        if (gameManager.player1ControllerType == ControllerType.Xbox)
        {
            text.text = "Press Y to Continue";
        }
        else if (gameManager.player1ControllerType == ControllerType.Keyboard)
        {
            text.text = "Press Space to Continue";
        }
    }

    public void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.CharacterSelectControls.Start.performed += OnStart;
        }
    }

    public void OnDisable()
    {
        if (inputControls != null)
        {
            inputControls.CharacterSelectControls.Start.performed -= OnStart;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }

    private void OnStart(InputAction.CallbackContext obj)
    {
        if (obj.control.device == inputManager.players[0].inputDevice)
        {
            OnDisable();
            Debug.Log("Load Minigame");
            SceneLoader.instance.LoadMinigame();
        }
    }
}
