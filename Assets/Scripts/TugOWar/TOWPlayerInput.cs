using UnityEngine;

public class TOWPlayerInput : MonoBehaviour
{
    public int playerID;
    public Transform rope;
    public float moveSpeed = 1f;
    public float maxDistance = 10f;

    [SerializeField]private int player1Pulls = 0;
    [SerializeField] private int player2Pulls = 0;

    MultiplayerInputManager inputManager; 
    InputControls inputControls;


    private void Start()
    {
        inputManager = MultiplayerInputManager.instance;

        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }

        gameObject.transform.SetParent(rope); //players move with rope
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

    void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.Pull.performed += OnPull;
            inputControls.MasterControls.Pull.canceled += OnPull;
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
        
        CheckWinCondition();
    }

    private void OnPull(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
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
            
        }
    }

    private void CheckWinCondition()
    {
        if (rope.position.x <= maxDistance)
        {
            Debug.Log("Player 1 wins");
        }
        else if (rope.position.x >= -maxDistance)
        {
            Debug.Log("Player 2 wins");
        }
    }
}