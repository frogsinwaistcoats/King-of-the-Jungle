using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputRace : MonoBehaviour
{
    public int playerID;

    public Vector2 moveInput;
    public Vector2 jumpInput;
    public float moveSpeed = 15f;
    public float jumpForce = 5f;

    public LayerMask groundLayer;

    private Rigidbody rb;
    private CountdownTimer countdownTimer;
    MultiplayerInputManager inputManager;
    InputControls inputControls;

    [SerializeField] private bool isGrounded;

    void Start()
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

        rb = GetComponent<Rigidbody>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
       
    }

    void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.Move.performed += OnMove;
            inputControls.MasterControls.Move.canceled += OnMove;
            inputControls.MasterControls.Jump.performed += OnJump;
            inputControls.MasterControls.Jump.canceled += OnJump;
        }
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }

    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            JumpPlayer();
        }
        else if (obj.canceled)
        {
            jumpInput = Vector2.zero;
        }
        
    }


    private void FixedUpdate()
    {
        if (countdownTimer.CanMove())
        {
            MovePlayer();
            
        }  
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void JumpPlayer()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInput = Vector2.zero;
            isGrounded = false;
        }
    }
   

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}