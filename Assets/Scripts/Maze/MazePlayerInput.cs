using Unity.VisualScripting;
using UnityEngine;

public class MazePlayerInput : MonoBehaviour
{
    public MultiplayerInputManager inputManager;
    public InputControls inputControls;
    private SpinHandler spinHandler;
    MazeCountdown mazeCountdown;
    MazeFinishManager finishManager;
    public Animator animator;

    public int playerID;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 respawnPosition;
    private bool hasFinished = false;

    public Vector2 moveInput;
    public float moveSpeed;

    public int finishPosition;
    
    private Collider disabledSpinCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        spinHandler = GetComponent<SpinHandler>();
        mazeCountdown = MazeCountdown.instance;
        finishManager = MazeFinishManager.instance;
    }

    private void Start()
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
            inputControls.MasterControls.Movement.performed -= OnMove;
            inputControls.MasterControls.Movement.canceled -= OnMove;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }

    public void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined += AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.Movement.performed += OnMove;
            inputControls.MasterControls.Movement.canceled += OnMove;
        }
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!hasFinished && !mazeCountdown.isRunning)
        {
            moveInput = obj.ReadValue<Vector2>();
            float movementValue = moveInput.sqrMagnitude > 0 ? 1f : 0f;
            animator.SetFloat("isMoving", movementValue);
        }  
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!hasFinished && MazeTimer.instance.timerIsRunning)
        {
            if (spinHandler.isSpinning)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = Vector2.zero;
                moveInput = Vector3.zero;
            }

            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);

        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeSpin1"))
        {
            ClearInputControls();
            spinHandler.StartSpin(1, other.transform.position);
            other.enabled = false;
            disabledSpinCollider = other;
        }
        else if (other.gameObject.CompareTag("MazeSpin2"))
        {
            ClearInputControls();
            spinHandler.StartSpin(2, other.transform.position);
            other.enabled = false;
            disabledSpinCollider = other;
        }
        else if (other.gameObject.CompareTag("MazeSpin3"))
        {
            ClearInputControls();
            spinHandler.StartSpin(3, other.transform.position);
            other.enabled = false;
            disabledSpinCollider = other;
        }
        else if (other.gameObject.CompareTag("MazeFinish"))
        {
            hasFinished = true;
            int placing = finishManager.PlayerFinish(playerID);
            float score = finishManager.CalculateScore(placing);
            GetComponent<PlayerStats>().playerData.SetPlayerScore(score);
            GetComponent<PlayerStats>().playerData.SetTotalScore(score);
            Debug.Log("Player " + playerID + " Placing: " + placing + " Score: " + score);
        }
        else if (other.gameObject.CompareTag("MazeCheckpoint1") || other.gameObject.CompareTag("MazeCheckpoint2") || other.gameObject.CompareTag("MazeCheckpoint3"))
        {
            respawnPosition = transform.position;
        }
        else if (other.gameObject.CompareTag("MazeBoulder"))
        {
            ReturnToStart();
            if (disabledSpinCollider != null)
            {
                disabledSpinCollider.enabled = true;
                disabledSpinCollider = null;
            }
        }
        else if (other.gameObject.CompareTag("MazeBoulder2"))
        {
            ReturnToCheckpoint2();
            if (disabledSpinCollider != null)
            {
                disabledSpinCollider.enabled = true;
                disabledSpinCollider = null;
            }
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void ReturnToStart()
    {
        gameObject.transform.position = startPosition;
        ClearInputControls();
        spinHandler.MasterControls(playerID);
    }

    public void ReturnToCheckpoint2()
    {
        gameObject.transform.position = respawnPosition;
        ClearInputControls();
        spinHandler.SpinControls1(playerID);
    }

    public void ClearInputControls()
    {
        inputControls.MasterControls.Movement.performed -= OnMove;
        inputControls.MasterControls.Movement.canceled -= OnMove;
        inputControls.SpinControl1.Movement.performed -= OnMove;
        inputControls.SpinControl1.Movement.canceled -= OnMove;
        inputControls.SpinControl2.Movement.performed -= OnMove;
        inputControls.SpinControl2.Movement.canceled -= OnMove;
        inputControls.SpinControl3.Movement.performed -= OnMove;
        inputControls.SpinControl3.Movement.canceled -= OnMove;
    }
}
