using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterSelect : MonoBehaviour
{
    public TextMeshProUGUI[] playerPrompts;
    public Image[] playerSprites;
    public Sprite[] characterSprites;

    MultiplayerInputManager inputManager;
    private List<InputControls> inputControls = new List<InputControls>();
    private List<int> selectedSpriteIndex;
    private int joinedPlayers;

    private void Start()
    {
        inputManager = MultiplayerInputManager.instance;
        inputManager.onPlayerJoined += OnPlayerJoinedHandler;

        joinedPlayers = inputManager.players.Count;
        selectedSpriteIndex = new List<int>(new int [joinedPlayers]);
        
        for (int i = 0; i < joinedPlayers; i++)
        {
            OnPlayerJoinedHandler(i);
        }
    }

    private void OnPlayerJoinedHandler(int playerID)
    {
        Debug.Log("Player" + playerID + "Joined");

        if (playerID >= inputControls.Count)
        {
            inputControls.Add(inputManager.players[playerID].playerControls);
            selectedSpriteIndex.Add(0);
        }

        UpdatePrompts(playerID);
        AssignInputs(playerID);
        
    }

    private void AssignInputs(int playerID)
    {
        inputManager.onPlayerJoined += AssignInputs;
        inputControls[playerID] = inputManager.players[playerID].playerControls;

        inputControls[playerID].CharacterSelectControls.Next.performed += context => OnNext(playerID);
        inputControls[playerID].CharacterSelectControls.Previous.performed += context => OnPrevious(playerID);

    }

    private void OnPrevious(int playerID)
    {
        Debug.Log("Previous for player " + playerID);
        CycleSprites(playerID, true);
    }

    private void OnNext(int playerID)
    {
        Debug.Log("Next for player " + playerID);
        CycleSprites(playerID, false);
    }

    public void UpdatePrompts(int playerID)
    {
        playerPrompts[playerID].text = "Choose Character";
        playerSprites[playerID].sprite = characterSprites[0];
        selectedSpriteIndex[playerID] = 0; 
    }

    public void CycleSprites(int playerID, bool next)
    {
        if(playerID < selectedSpriteIndex.Count)
        {
            if (next)
            {
                selectedSpriteIndex[playerID] = (selectedSpriteIndex[playerID] + 1) % characterSprites.Length;
            }
            else
            {
                selectedSpriteIndex[playerID] = (selectedSpriteIndex[playerID] - 1 + characterSprites.Length) % characterSprites.Length;
            }

            playerSprites[playerID].sprite = characterSprites[selectedSpriteIndex[playerID]];
        }
    }

    public void ContinueToGame()
    {
        SceneManager.LoadScene("MinigameMaze");
    } 
}
