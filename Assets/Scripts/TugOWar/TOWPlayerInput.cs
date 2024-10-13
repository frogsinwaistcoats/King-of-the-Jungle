using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections;

public class TOWPlayerInput : MonoBehaviour
{
    public AudioSource tickSource;
    public int playerID;
    public ControllerType controllerType;
    public Transform rope;
    public float moveSpeed = -1f;
    public float maxDistance = 10f;
    public UI_ReloadButton buttonPrefab;
    (string, string) chosenKeys;
    public bool isButton1Pressed = false;
    public bool isButton2Pressed = false;
    public bool winConditionMet = false;

    public InputActionAsset actionAsset;

    [SerializeField] private int player1Pulls = 0;
    [SerializeField] private int player2Pulls = 0;

    MultiplayerInputManager inputManager; 
    InputControls inputControls;
    private UI_ReloadButton newButton1;
    private UI_ReloadButton newButton2;
    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = SceneLoader.instance;
    }

    private void Start()
    {
        tickSource = GetComponent<AudioSource>();
        PlayerStats playerStats = GetComponent<PlayerStats>();
        if (playerStats != null && playerStats.playerData != null)
        {
            playerID = playerStats.playerData.playerID;
            controllerType = playerStats.playerData.controllerType;
        }

        if (rope == null)
        {
            rope = GameObject.Find("Rope").transform;
        }

        inputManager = MultiplayerInputManager.instance;

        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }


        StartCoroutine(SpawnButtons());

        gameObject.transform.SetParent(rope); //players move with rope
    }

    public IEnumerator SpawnButtons()
    {
        yield return new WaitForSeconds(3f);
        newButton1 = Instantiate(buttonPrefab);
        newButton2 = Instantiate(buttonPrefab);
        newButton1.transform.SetParent(GameObject.Find("Canvas").transform, false);
        newButton2.transform.SetParent(GameObject.Find("Canvas").transform, false);
        chosenKeys = TOW_UI.instance.OpenReloadUI(newButton1, newButton2, playerID, controllerType);
    }

    private void OnDisable()
    {
        if (inputControls != null)
        {
            inputControls.MasterControls.Pull.performed -= OnPull;
            inputControls.MasterControls.Pull.canceled -= OnPull;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }

    public void AssignInputs(int ID)
    {
        if (playerID == 0)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.TugOWarControls.Pull0.performed += OnPull;
            inputControls.TugOWarControls.Pull0.canceled += OnPull;
        }
        if (playerID == 1)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.TugOWarControls.Pull1.performed += OnPull;
            inputControls.TugOWarControls.Pull1.canceled += OnPull;
        }
        if (playerID == 2)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.TugOWarControls.Pull2.performed += OnPull;
            inputControls.TugOWarControls.Pull2.canceled += OnPull;
        }
        if (playerID == 3)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.TugOWarControls.Pull3.performed += OnPull;
            inputControls.TugOWarControls.Pull3.canceled += OnPull;
        }
    }

    private void Update()
    {   
        //calculate the difference in pulls
        float pullDifference = (player1Pulls - player2Pulls) * moveSpeed * Time.deltaTime;

        //calculate new x position for rope
        float newXPosition = rope.position.x + pullDifference;

        //clamp the new position within max difference
        newXPosition = Mathf.Clamp(newXPosition, -maxDistance, maxDistance);

        //update rope position along x axis
        rope.position = new Vector3(newXPosition, rope.position.y, rope.position.z);
        
        if (!winConditionMet && (newXPosition == maxDistance || newXPosition == -maxDistance))
        {
            CheckWinCondition();
            winConditionMet = true;
        }
    }

    public void OnPull(InputAction.CallbackContext obj)
    {
        if (TOWTimer.instance.timerIsRunning && obj.performed)
        {
            tickSource.Play();
            Debug.Log(obj.control.ToString());
            string controlPressed = GetTextAfterLastSlash(obj.control.ToString());

            if (controlPressed == chosenKeys.Item1)
            {
                isButton1Pressed = true;
            }
            if (controlPressed == chosenKeys.Item2)
            {
                isButton2Pressed = true;
            }
            if (controlPressed != chosenKeys.Item1 && controlPressed != chosenKeys.Item2)
            {
                StartCoroutine(Penalty());
            }

            if (isButton1Pressed && isButton2Pressed)
            {
                if (playerID == 0)
                {
                    Debug.Log("Player 1 pull");
                    player1Pulls++;
                }
                else if (playerID == 1)
                {
                    Debug.Log("Player 2 pull");
                    if (inputManager.PlayerCount == 3)
                    {
                        player2Pulls += 2;
                    }
                    else
                    {
                        player2Pulls++;
                    }
                }
                else if (playerID == 2)
                {
                    Debug.Log("Player 3 pull");
                    player1Pulls++;
                }
                else if (playerID == 3)
                {
                    Debug.Log("Player 4 pull");
                    player2Pulls++;
                }

                isButton1Pressed = false;
                isButton2Pressed = false;

                chosenKeys = TOW_UI.instance.OpenReloadUI(newButton1, newButton2, playerID, controllerType);
            }
        }
    }

    private IEnumerator Penalty()
    {
        Debug.Log("PLAYER " + (playerID+1) + "  WRONG BUTTON");
        TOW_UI.instance.PenaltyButton(newButton1, newButton2);
        OnDisable();
        yield return new WaitForSeconds(1.5f);
        AssignInputs(playerID);
        chosenKeys = TOW_UI.instance.OpenReloadUI(newButton1, newButton2, playerID, controllerType);

    }

    public static string GetTextAfterLastSlash(string input)
    {
        // Find the index of the last slash
        int lastSlashIndex = input.LastIndexOf('/');

        // Check if a slash is found
        if (lastSlashIndex != -1)
        {
            // Return the substring after the last slash
            return input.Substring(lastSlashIndex + 1);
        }

        // If no slash is found, return the original string
        return input;
    }

    public void OnPullButton()
    {
        if (playerID == 0 || playerID == 2)
        {
            rope.position = new Vector3(rope.position.x + 1, rope.position.y, rope.position.z);
        }

        if (playerID == 1 || playerID == 3)
        {
            rope.position = new Vector3(rope.position.x - 1, rope.position.y, rope.position.z);
        }
    }

    private void CheckWinCondition()
    {
        if (rope.position.x <= maxDistance)
        {
            Debug.Log("Player 1/3 wins");
            if (playerID == 0 || playerID == 2)
            {
                GetComponent<PlayerStats>().playerData.SetPlayerScore(1);
                GetComponent<PlayerStats>().playerData.SetTotalScore(1);
            }

            TOWFinish.instance.GameOver();
        }
        else if (rope.position.x >= -maxDistance)
        {
            Debug.Log("Player 2/4 wins");
            if (playerID == 1 || playerID == 3)
            {
                GetComponent<PlayerStats>().playerData.SetPlayerScore(1);
                GetComponent<PlayerStats>().playerData.SetTotalScore(1);
            }

            TOWFinish.instance.GameOver();
        }
    }
}