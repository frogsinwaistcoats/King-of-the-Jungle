using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public MultiplayerInputManager inputManager;
    public InputControls inputControls;
    public int playerID;

    [SerializeField] GameManager gameManager;
    [SerializeField] CharacterSelect characterSelect;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI joinText;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterText;
    [SerializeField] GameObject buttons;

    int currentCharacter = 0;

    private void Start()
    {
        SetCharacter(currentCharacter);

        inputManager = MultiplayerInputManager.instance;
        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }
    }

    public void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.CharacterSelectControls.Next.performed += OnNext;
            inputControls.CharacterSelectControls.Previous.performed += OnPrevious;
            inputControls.CharacterSelectControls.Confirm.performed += OnConfirm;
            inputControls.CharacterSelectControls.Start.performed += OnStart;
        }
    }

    public void OnDisable()
    {
        if (inputControls != null)
        {
            inputControls.CharacterSelectControls.Next.performed -= OnNext;
            inputControls.CharacterSelectControls.Previous.performed -= OnPrevious;
            inputControls.CharacterSelectControls.Confirm.performed -= OnConfirm;
            inputControls.CharacterSelectControls.Start.performed -= OnStart;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }

    private void OnNext(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NextCharacter();
    }

    private void OnPrevious(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PreviousCharacter();
    }

    private void OnConfirm(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Confirm();

        if (playerID != 0)
        {
            OnDisable();
        }
    }

    private void OnStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (playerID == 0 && inputManager.players.Count == gameManager.players.Count)
        {
            OnDisable();
            SceneLoader.instance.LoadMinigameSelection();
        }
        
    }

    public void NextCharacter()
    {
        currentCharacter++;
        if (currentCharacter >= characterSelect.characters.Count)
        {
            currentCharacter = 0;
        }

        SetCharacter(currentCharacter);
    }

    public void PreviousCharacter()
    {
        currentCharacter--;
        if (currentCharacter <= -1)
        {
            currentCharacter = characterSelect.characters.Count - 1;
        }

        SetCharacter(currentCharacter);
    }

    public void SetCharacter(int id)
    {
        characterImage.sprite = characterSelect.characters[id].characterSprite;
        characterText.text = characterSelect.characters[id].characterName;
    }

    public void Confirm()
    {
        ControllerType controllerType = MultiplayerInputManager.instance.players[playerID].controllerType;

        gameManager.AddPlayer(playerID, currentCharacter, controllerType);
        buttons.SetActive(false);
    }
}
