using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public TextMeshProUGUI[] playerPrompts;
    MultiplayerInputManager inputManager;

    private void Start()
    {
        inputManager = MultiplayerInputManager.instance;
        inputManager.onPlayerJoined += UpdatePrompts;
        int joinedPlayers = inputManager.players.Count;
        for (int i = 0; i < joinedPlayers; i++)
        {
            UpdatePrompts(i);
        }
    }

    public void UpdatePrompts(int id)
    {
        playerPrompts[id].text = "Choose Character";
    }

    public void ContinueToGame()
    {
        SceneManager.LoadScene("MinigameMaze");
    }

    /*
    public TextMeshProUGUI[] playerPrompts;
    public Sprite[] characterSprites;
    public Image[] playerCharacterImages;
    private int[] selectedCharacterIndices;
    MultiplayerInputManager inputManager;
    public int playerID;

    public InputControls inputControls;

    private void Start()
    {
        GetComponent<IndividualPlayerControls>().playerID = playerID;

        inputManager = MultiplayerInputManager.instance;
        inputManager.onPlayerJoined += UpdatePrompts;
        int joinedPlayers = inputManager.players.Count;
        selectedCharacterIndices = new int[joinedPlayers];
        for(int i = 0; i < joinedPlayers; i++)
        {
            UpdatePrompts(i);
        }

        inputControls = new InputControls();
        inputControls.Enable();
    }

    //private void OnEnable()
    //{
    //    inputControls.CharacterSelection.Next.performed += Next_performed;
    //    inputControls.CharacterSelection.Previous.performed += Previous_performed;
    //}

    //private void OnDisable()
    //{
    //    inputControls.CharacterSelection.Next.performed -= Next_performed;
    //    inputControls.CharacterSelection.Previous.performed -= Previous_performed;
    //    inputControls.Disable();
    //}

    //private void Next_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    CycleCharacter(playerID, 1);
    //}

    //private void Previous_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    CycleCharacter(playerID, -1);
    //}


    //private void CycleCharacter(int playerID, int direction)
    //{
    //    selectedCharacterIndices[playerID] += direction;
    //    if (selectedCharacterIndices[playerID] >= characterSprites.Length)
    //    {
    //        selectedCharacterIndices[playerID] = 0;
    //    }
    //    else if (selectedCharacterIndices[playerID] < 0)
    //    {
    //        selectedCharacterIndices[playerID] = characterSprites.Length - 1;
    //    }
    //    UpdateCharacterSprite(playerID);
    //}

    //private void UpdateCharacterSprite(int playerID)
    //{
    //    if (playerID < 0 || playerID >= playerCharacterImages.Length)
    //        return;

    //    playerCharacterImages[playerID].sprite = characterSprites[selectedCharacterIndices[playerID]];
    //}

    public void UpdatePrompts(int id)
    {
        playerPrompts[id].text = "Choose Character";
        //UpdateCharacterSprite(id);
    }

    public void ContinueToGame()
    {
        SceneManager.LoadScene("MinigameMaze");
    }
    */
}
