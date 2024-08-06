using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int playerID;
    MultiplayerInputManager inputManager;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public bool isTarget; // Set this in the inspector or dynamically

    private InputControls playerControls;
    private Vector2 movement;

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
        rb.freezeRotation = true; // Prevent rotation
    }

    //private void OnDisable()
    //{
    //    if (playerControls != null)
    //    {
    //        playerControls.Disable();
    //    }
    //}

    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            //Debug.Log($"Player {ID} assigned inputs.");
            inputManager.onPlayerJoined -= AssignInputs;
            playerControls = inputManager.players[playerID].playerControls;

            if (isTarget)
            {
                playerControls.Player.Move.performed += ctx => 
                {
                    movement = ctx.ReadValue<Vector2>();
                    //Debug.Log($"Movement input received: {movement}");
                };
                playerControls.Player.Move.canceled += ctx => movement = Vector2.zero;
            }
        }
    }

    private void Update()
    {
        if (isTarget && playerControls != null)
        {
            Vector3 move = new Vector3(movement.x, 0, 0) * moveSpeed; // Apply speed directly to the movement vector
            rb.velocity = new Vector3(move.x, rb.velocity.y, rb.velocity.z); // Update only x velocity
            //Debug.Log($"Player {playerID} moving with velocity: {rb.velocity}");
        }
    }
}