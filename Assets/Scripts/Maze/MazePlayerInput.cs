using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MazePlayerInput : MonoBehaviour
{
    public MultiplayerInputManager inputManager;
    public InputControls inputControls;
    private MazeSpinHandler spinHandler;
    MazeCountdown mazeCountdown;
    MazeFinishManager finishManager;
    PlayerStats playerStats;
    PlayerData playerData;

    public int playerID;
    private Rigidbody rb;
    public Animator animator;
    public SpriteRenderer rend;
    //private Vector3 startPosition;
    private Vector3 respawnPosition;
    private bool hasFinished = false;

    public Vector2 moveInput;
    public float moveSpeed;

    public int finishPosition;
    public GameObject colliderVisual;


    private Collider disabledSpinCollider;

    private void Awake()
    {
        mazeCountdown = MazeCountdown.instance;
        finishManager = MazeFinishManager.instance;

        rb = GetComponent<Rigidbody>();
        
        respawnPosition = transform.position;

        playerStats = GetComponent<PlayerStats>();
        spinHandler = GetComponent<MazeSpinHandler>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        playerData = GetComponent<PlayerStats>().playerData;
    }

    private void Start()
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
            StopAllCoroutines();
            ClearInputControls();
            spinHandler.StartSpin(1, other.transform.position);
            other.enabled = false;
            disabledSpinCollider = other;
            StartCoroutine(DisableSpinVisuals(other));
            StartCoroutine(StopSpin());
        }
        else if (other.gameObject.CompareTag("MazeSpin2"))
        {
            StopAllCoroutines();
            ClearInputControls();
            spinHandler.StartSpin(2, other.transform.position);
            other.enabled = false;
            disabledSpinCollider = other;
            StartCoroutine(DisableSpinVisuals(other));
        }
        else if (other.gameObject.CompareTag("MazeSpin3"))
        {
            StopAllCoroutines();
            ClearInputControls();
            spinHandler.StartSpin(3, other.transform.position);
            other.enabled = false;
            disabledSpinCollider = other;
            StartCoroutine(DisableSpinVisuals(other));
            StartCoroutine(StopSpin());
        }
        else if (other.gameObject.CompareTag("MazeFinish"))
        {
            hasFinished = true;
            animator.enabled = false;
            rend.sprite = playerStats.playerData.characterSprite;
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
            MazeAudioManager.instance.PlayThudSFX();
            StopAllCoroutines();
            ReturnToCheckpoint();
            if (disabledSpinCollider != null)
            {
                disabledSpinCollider.enabled = true;
                disabledSpinCollider = null;
                colliderVisual.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("MazeBoulder2"))
        {
            MazeAudioManager.instance.PlayThudSFX();
            StopAllCoroutines();
            ReturnToCheckpoint();
            if (disabledSpinCollider != null)
            {
                disabledSpinCollider.enabled = true;
                disabledSpinCollider = null;
                colliderVisual.SetActive(true);
            }
        }
    }

    public void ReturnToCheckpoint()
    {
        gameObject.transform.position = respawnPosition;
        ClearInputControls();
        spinHandler.MasterControls(playerID);
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

    IEnumerator StopSpin()
    {
        yield return new WaitForSeconds(10f);
        ClearInputControls();
        spinHandler.MasterControls(playerID);
    }

    IEnumerator DisableSpinVisuals(Collider other)
    {
        yield return new WaitForSeconds(1f);
        Transform parentTransform = other.transform.parent;
        if (parentTransform != null)
        {
            colliderVisual = parentTransform.Find("SpinVisual").gameObject;
            colliderVisual.SetActive(false);
        }
    }
}
