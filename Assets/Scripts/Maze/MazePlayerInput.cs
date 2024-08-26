using UnityEngine;

public class MazePlayerInput : MonoBehaviour
{
    public static MazePlayerInput instance;

    public int playerID;
    public MultiplayerInputManager inputManager; 
    public Vector2 moveInput;
    public float moveSpeed;
    private Rigidbody rb;

    //public int characterSpriteID;
    
    public Collider finishCollider;
    public InputControls inputControls;

    private Vector3 startPosition;
    private SpinHandler spinHandler;
    private bool hasFinished = false;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        spinHandler = GetComponent<SpinHandler>();
    }

    private void Start()
    {
        
        inputManager = MultiplayerInputManager.instance;
        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
            //AssignCharacterSprite(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }
        
    }

    //public void AssignCharacterSprite(int playerID)
    //{
    //    characterSpriteID = inputManager.players[playerID].characterID;
    //    gameObject.GetComponent<SpriteRenderer>().sprite = inputManager.characterSprites[characterSpriteID];
    //}

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

    void AssignInputs(int ID)
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
        if (!hasFinished)
        {
            moveInput = obj.ReadValue<Vector2>();
        }  
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!hasFinished)
        {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == spinHandler.spinCollider)
        {
            spinHandler.StartSpin();
        }

        if (other == finishCollider)
        {
            hasFinished = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ReturnToStart();
        }
    }

    public void ReturnToStart()
    {
        gameObject.transform.position = startPosition;
        ResetToMasterControls();
        spinHandler.spinCollider.enabled = true;
    }

    private void ResetToMasterControls()
    {
        //detatch current input controls
        inputControls.MasterControls.Movement.performed -= OnMove;
        inputControls.MasterControls.Movement.canceled -= OnMove;
        inputControls.SpinControl1.Movement.performed -= OnMove;
        inputControls.SpinControl1.Movement.canceled -= OnMove;
        inputControls.SpinControl2.Movement.performed -= OnMove;
        inputControls.SpinControl2.Movement.canceled -= OnMove;
        inputControls.SpinControl3.Movement.performed -= OnMove;
        inputControls.SpinControl3.Movement.canceled -= OnMove;

        //reassign master controls
        inputControls.MasterControls.Movement.performed += OnMove;
        inputControls.MasterControls.Movement.canceled += OnMove;
    }
}
