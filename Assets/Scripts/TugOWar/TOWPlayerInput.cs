using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TOWPlayerInput : MonoBehaviour
{
    public int playerID;
    public ControllerType controllerType;
    public Transform rope;
    public float moveSpeed = 1f;
    public float maxDistance = 10f;
    public UI_ReloadButton buttonPrefab;
    string chosenKey;

    public InputActionAsset actionAsset;

    [SerializeField] private int player1Pulls = 0;
    [SerializeField] private int player2Pulls = 0;

    MultiplayerInputManager inputManager; 
    InputControls inputControls;
    private UI_ReloadButton newbutton;

    private void Start()
    {
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

        newbutton = Instantiate(buttonPrefab);
        newbutton.transform.SetParent(GameObject.Find("Canvas").transform, false);
        chosenKey = TOW_UI.instance.OpenReloadUI(newbutton, playerID, controllerType);
        
        //ChangeBindingToKey(randomKey);

        //gameObject.transform.SetParent(rope); //players move with rope
    }


    /*
    public void ChangeBindingToKey(string randomKey)
    {
        InputActionMap tugOWarControls = actionAsset.FindActionMap("TugOWarControls");

        InputAction pullxAction = tugOWarControls.FindAction("Pull" + playerID);

        if(pullxAction != null)
        {
            Debug.Log("change binding");
            pullxAction.RemoveAllBindingOverrides();
            pullxAction.ApplyBindingOverride(randomKey);
            pullxAction.Disable();
            pullxAction.Enable();
        }
        else
        {
            Debug.Log("not found");
        }
    }
    */

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
        
        //CheckWinCondition();
        
    }

    public void OnPull(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Debug.Log(obj.control.ToString());

            if (chosenKey == GetTextAfterLastSlash(obj.control.ToString()))
            {
                if (playerID == 0)
                {
                    Debug.Log("Player 1 pull");
                    player1Pulls++;
                }
                if (playerID == 1)
                {
                    Debug.Log("Player 2 pull");
                    player2Pulls++;
                }
                if (playerID == 2)
                {
                    Debug.Log("Player 3 pull");
                    player1Pulls++;
                }
                if (playerID == 3)
                {
                    Debug.Log("Player 4 pull");
                    player2Pulls++;
                }

                chosenKey = TOW_UI.instance.OpenReloadUI(newbutton, playerID, controllerType);
            }
        }
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

    //private void CheckWinCondition()
    //{
    //    if (rope.position.x <= maxDistance)
    //    {
    //        Debug.Log("Player 1 wins");
    //        //SceneManager.LoadScene("Scores");
    //    }
    //    else if (rope.position.x >= -maxDistance)
    //    {
    //        Debug.Log("Player 2 wins");
    //        //SceneManager.LoadScene("Scores");
    //    }
    //}
}