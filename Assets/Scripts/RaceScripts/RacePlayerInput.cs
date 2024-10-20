using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RacePlayerInput : MonoBehaviour
{
    public int playerID;

    public Vector2 moveInput;
    public Vector2 jumpInput;
    public float moveSpeed = 15f;
    public float jumpForce = 10f;

    public LayerMask groundLayer;

    private Rigidbody rb;
    private RaceCountdownTimer countdownTimer;
    MultiplayerInputManager inputManager;
    InputControls inputControls;
    RaceFinishManager finishRace;
    PlayerStats playerStats;
    Animator animator;
    SpriteRenderer rend;

    [SerializeField] private bool isGrounded;
    private bool hasFinished = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        countdownTimer = RaceCountdownTimer.instance;
        finishRace = FindObjectOfType<RaceFinishManager>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        
        if (playerStats != null && playerStats.playerData != null)
        {
            playerID = playerStats.playerData.playerID;
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

        animator.enabled = false;
    }

    void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.RaceMovement.performed += OnMove;
            inputControls.MasterControls.RaceMovement.canceled += OnMove;
            inputControls.MasterControls.Jump.performed += OnJump;
            inputControls.MasterControls.Jump.canceled += OnJump;
        }
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        if (!hasFinished && countdownTimer.canMove)
        {
            animator.enabled = true;
            animator.SetBool(playerStats.playerData.characterName, true);
            moveInput = obj.ReadValue<Vector2>();

            if (obj.canceled)
            {
                animator.enabled = false;
                rend.sprite = playerStats.playerData.characterSprite;
            }
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!hasFinished && RaceTimerText.instance.isTiming)
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
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!hasFinished && RaceTimerText.instance.isTiming)
        {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void JumpPlayer()
    {
        if (isGrounded && countdownTimer.canMove)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInput = Vector2.zero;
            isGrounded = false;
            AudioManager.instance.Play("Bounce");
        }
    }
   

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RaceFinish"))
        {
            animator.enabled = false;
            hasFinished = true;
            int placing = finishRace.PlayerFinish(playerID);
            float score = finishRace.CalculateScore(placing);
            GetComponent<PlayerStats>().playerData.SetPlayerScore(score);
            GetComponent<PlayerStats>().playerData.SetTotalScore(score);
            Debug.Log("Player " + playerID + " Placing: " + placing + " Score: " + score);
        }
    }
}