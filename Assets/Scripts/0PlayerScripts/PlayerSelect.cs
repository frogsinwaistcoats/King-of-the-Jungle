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
    [SerializeField] Image BG;
    public GameObject buttons;

    GameObject startButton;

    int currentCharacter = 0;

    private void Start()
    {
        gameManager = GameManager.instance;

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

        startButton = characterSelect.startButton;
        startButton.SetActive(false);
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


            Transform leftButton = buttons.transform.Find("Left");
            if (leftButton != null)
            {
                TextMeshProUGUI text = leftButton.GetComponentInChildren<TextMeshProUGUI>();

                if (text != null)
                {
                    if (inputManager.players[playerID].controllerType == ControllerType.Xbox)
                    {
                        text.text = "X";
                    }
                    else if (inputManager.players[playerID].controllerType == ControllerType.Keyboard)
                    {
                        text.text = "A";
                    }
                }
            }
            else
            {
                Debug.LogError("Left button not found");
            }

            Transform rightButton = buttons.transform.Find("Right");
            if (rightButton != null)
            {
                TextMeshProUGUI text = rightButton.GetComponentInChildren<TextMeshProUGUI>();

                if (text != null)
                {
                    if (inputManager.players[playerID].controllerType == ControllerType.Xbox)
                    {
                        text.text = "B";
                    }
                    else if (inputManager.players[playerID].controllerType == ControllerType.Keyboard)
                    {
                        text.text = "D";
                    }
                }
            }
            else
            {
                Debug.LogError("Right button not found");
            }

            Transform confirmButton = buttons.transform.Find("Confirm");
            if (confirmButton != null)
            {
                TextMeshProUGUI text = confirmButton.GetComponentInChildren<TextMeshProUGUI>();

                if (text != null)
                {
                    if (inputManager.players[playerID].controllerType == ControllerType.Xbox)
                    {
                        text.text = "A to Confirm";
                    }
                    else if (inputManager.players[playerID].controllerType == ControllerType.Keyboard)
                    {
                        text.text = "Enter to Confirm";
                    }
                }
            }
            else
            {
                Debug.LogError("Right button not found");
            }
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
        inputControls.CharacterSelectControls.Next.performed -= OnNext;
        inputControls.CharacterSelectControls.Previous.performed -= OnPrevious;
        inputControls.CharacterSelectControls.Confirm.performed -= OnConfirm;
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
            Debug.Log("next scene");
            SceneLoader.instance.LoadMinigameSelection();
        }
    }

    public void NextCharacter()
    {
        do
        {
            currentCharacter++;
            if (currentCharacter >= characterSelect.characters.Count)
            {
                currentCharacter = 0;
            }
        } while (characterSelect.characters[currentCharacter].isChosen);
        

        SetCharacter(currentCharacter);
    }

    public void PreviousCharacter()
    {
        do
        {
            currentCharacter--;
            if (currentCharacter <= -1)
            {
                currentCharacter = characterSelect.characters.Count - 1;
            }
        } while (characterSelect.characters[currentCharacter].isChosen);


        SetCharacter(currentCharacter);
    }

    public void SetCharacter(int id)
    {
        characterImage.sprite = characterSelect.characters[id].characterSprite;
        characterText.text = characterSelect.characters[id].characterName;
    }

    private void Update()
    {
        if (characterSelect.characters[currentCharacter].isChosen)
        {
            SetCharacter(currentCharacter);
        }
    }

    public void Confirm()
    {
        ControllerType controllerType = MultiplayerInputManager.instance.players[playerID].controllerType;

        gameManager.AddPlayer(gameManager.players[playerID], playerID, currentCharacter, characterSelect.characters[currentCharacter].characterName, controllerType, characterSelect.characters[currentCharacter].characterSprite);
        buttons.SetActive(false);

        characterSelect.haveJoined++;

        characterSelect.characters[currentCharacter].isChosen = true;

        ForceOtherPlayersToSwitch();

        SetCharacter(currentCharacter);

        if ((inputManager.players.Count == characterSelect.haveJoined) && gameManager.players.Count >= 2)
        {
            startButton.SetActive(true);
            
            if (startButton != null)
            {
                TextMeshProUGUI text = startButton.GetComponentInChildren<TextMeshProUGUI>();

                if (text != null)
                {
                    if (MultiplayerInputManager.instance.players[0].controllerType == ControllerType.Xbox)
                    {
                        text.text = "Player 1 press Y to Start";
                    }
                    else if (MultiplayerInputManager.instance.players[0].controllerType == ControllerType.Keyboard)
                    {
                        text.text = "Player 1 press Space to Start";
                    }
                }
            }
            else
            {
                Debug.LogError("start button not found");
            }
        }
    }

    private void ForceOtherPlayersToSwitch()
    {
        foreach (var player in FindObjectsOfType<PlayerSelect>())
        {
            if (player.playerID == this.playerID) continue;

            if (player.currentCharacter == this.currentCharacter)
            {
                player.NextCharacter();
            }
        }
    }
}
