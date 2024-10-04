using UnityEngine;
using UnityEngine.InputSystem;

public class RacePlayerInput : MonoBehaviour
{
    public MultiplayerInputManager inputManager;
    public InputControls inputControls;
    RaceCountdown raceCountdown;
    RaceFinishManager raceFinish;

    public int playerID;
    private Rigidbody rb;
    private bool hasFinished = false;

    public Vector2 moveInput;
    public float moveSpeed = 10f;
    public Vector2 jumpInput;
    public float jumpForce = 5f;

    public LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        raceCountdown = RaceCountdown.instance;
        raceFinish = RaceFinishManager.instance;
    }

    void Start()
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();
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
    }

    public void OnDisable()
    {
        if (inputControls != null)
        {
            inputControls.MasterControls.RaceMovement.performed -= OnMove;
            inputControls.MasterControls.RaceMovement.canceled -= OnMove;
            inputControls.MasterControls.Jump.performed -= OnJump;
            inputControls.MasterControls.Jump.canceled -= OnJump;
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
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.RaceMovement.performed += OnMove;
            inputControls.MasterControls.RaceMovement.canceled += OnMove;
            inputControls.MasterControls.Jump.performed += OnJump;
            inputControls.MasterControls.Jump.canceled += OnJump;
        }
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        if (!hasFinished && !raceCountdown.isRunning)
        {
            moveInput = obj.ReadValue<Vector2>();
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
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
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!hasFinished && RaceTimer.instance.timerIsRunning)
        {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void JumpPlayer()
    {
        if (!hasFinished && isGrounded && RaceTimer.instance.timerIsRunning)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RaceFinish"))
        {
            hasFinished = true;
            int placing = raceFinish.PlayerFinish(playerID);
            float score = raceFinish.CalculateScore(placing);
            GetComponent<PlayerStats>().playerData.SetPlayerScore(score);
            GetComponent<PlayerStats>().playerData.SetTotalScore(score);
            Debug.Log("Player " + playerID + " Placing: " + placing + " Score: " + score);
        }
    }

    //public int playerID;

    //public Vector2 moveInput;
    //public Vector2 jumpInput;
    //public float moveSpeed = 15f;
    //public float jumpForce = 5f;

    //public LayerMask groundLayer;

    //private Rigidbody rb;
    //private RaceCountdown raceCountdown;
    //MultiplayerInputManager inputManager;
    //InputControls inputControls;
    //RaceFinishManager raceFinish;

    //[SerializeField] private bool isGrounded;
    //private bool hasFinished = false;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();

    //    raceCountdown = RaceCountdown.instance;
    //    raceFinish = RaceFinishManager.instance;
    //}

    //void Start()
    //{
    //    PlayerStats playerStats = GetComponent<PlayerStats>();
    //    if (playerStats != null && playerStats.playerData != null)
    //    {
    //        playerID = playerStats.playerData.playerID;
    //    }


    //    inputManager = MultiplayerInputManager.instance;
    //    if (inputManager.players.Count >= playerID + 1)
    //    {
    //        AssignInputs(playerID);
    //    }
    //    else
    //    {
    //        inputManager.onPlayerJoined += AssignInputs;
    //    }
    //}

    ////public void OnDisable()
    ////{
    ////    if (inputControls != null)
    ////    {
    ////        inputControls.MasterControls.RaceMovement.performed -= OnMove;
    ////        inputControls.MasterControls.RaceMovement.canceled -= OnMove;
    ////        inputControls.MasterControls.Jump.performed -= OnJump;
    ////        inputControls.MasterControls.Jump.canceled -= OnJump;
    ////    }
    ////    else
    ////    {
    ////        inputManager.onPlayerJoined -= AssignInputs;
    ////    }
    ////}

    //void AssignInputs(int ID)
    //{
    //    if (playerID == ID)
    //    {
    //        inputManager.onPlayerJoined -= AssignInputs;
    //        inputControls = inputManager.players[playerID].playerControls;
    //        inputControls.MasterControls.RaceMovement.performed += OnMove;
    //        inputControls.MasterControls.RaceMovement.canceled += OnMove;
    //        inputControls.MasterControls.Jump.performed += OnJump;
    //        inputControls.MasterControls.Jump.canceled += OnJump;
    //    }
    //}

    //private void OnMove(InputAction.CallbackContext obj)
    //{
    //    if (!hasFinished && raceCountdown.isCountingDownFinished)
    //    {
    //        moveInput = obj.ReadValue<Vector2>();
    //    }
    //}

    //private void OnJump(InputAction.CallbackContext obj)
    //{
    //    if (obj.performed)
    //    {
    //        JumpPlayer();
    //    }
    //    else if (obj.canceled)
    //    {
    //        jumpInput = Vector2.zero;
    //    }

    //}


    //private void FixedUpdate()
    //{
    //    MovePlayer();
    //}

    //private void MovePlayer()
    //{
    //    if (!hasFinished && RaceTimer.instance.timerIsRunning)
    //    {
    //        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
    //        rb.MovePosition(rb.position + movement);
    //    }
    //}

    //private void JumpPlayer()
    //{
    //    if (!hasFinished && isGrounded && RaceTimer.instance.timerIsRunning)
    //    {
    //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //        jumpInput = Vector2.zero;
    //        isGrounded = false;
    //    }
    //}


    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("RaceFinish"))
    //    {
    //        hasFinished = true;
    //        int placing = raceFinish.PlayerFinish(playerID);
    //        float score = raceFinish.CalculateScore(placing);
    //        GetComponent<PlayerStats>().playerData.SetPlayerScore(score);
    //        GetComponent<PlayerStats>().playerData.SetTotalScore(score);
    //        Debug.Log("Player " + playerID + " Placing: " + placing + " Score: " + score);
    //    }
    //}
}