using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DodgeballPlayerMovement : MonoBehaviour
{
    public int playerID;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public bool isTarget;

    public float dashSpeed = 30f;
    public float dashDuration = 0.4f;
    private bool isDashing;
    private bool canDash = true;
    private Vector2 movementInput;

    public InputControls playerControls;
    PlayerStats playerStats;
    Animator animator;
    SpriteRenderer rend;
    public float score;

    MultiplayerInputManager multiplayerInputManager;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (playerStats != null && playerStats.playerData != null)
        {
            playerID = playerStats.playerData.playerID;
        }

        multiplayerInputManager = MultiplayerInputManager.instance;
        multiplayerInputManager.onPlayerJoined += AssignInputs;
        rb = GetComponent<Rigidbody>();
        animator.enabled = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isTarget)
        {
            animator.enabled = true;
            animator.SetBool(playerStats.playerData.characterName, true);
            movementInput = context.ReadValue<Vector2>();

            if (context.canceled)
            {
                animator.enabled = false;
                rend.sprite = playerStats.playerData.characterSprite;
            }
        }
        
    }

    public void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            MultiplayerInputManager.instance.onPlayerJoined -= AssignInputs;

            // Check if ID is valid and within range
            if (ID < MultiplayerInputManager.instance.players.Count)
            {
                playerControls = MultiplayerInputManager.instance.players[playerID].playerControls;
            }
            else
            {
                Debug.LogError($"Invalid Player ID: {ID}. Cannot assign inputs.");
                return;
            }

            if (playerControls != null)
            {
                playerControls.Player.Move.performed += OnMove;
                playerControls.Player.Move.canceled += OnMove; //ctx => movementInput = Vector2.zero;

                playerControls.Player.Dash.performed += ctx => StartCoroutine(Dash());
            }
            else
            {
                Debug.LogError($"Player {ID} controls not set up correctly.");
            }
        }
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Player.Move.performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.Player.Move.canceled -= ctx => movementInput = Vector2.zero;

            playerControls.Player.Dash.performed -= ctx => StartCoroutine(Dash());
        }
        else
        {
            MultiplayerInputManager.instance.onPlayerJoined -= AssignInputs;
        }
    }


    public bool HasValidControls()
    {
        return playerControls != null;
    }

    public void EnableMovementControls()
    {
        if (playerControls != null)
        {
            playerControls.Player.Move.Enable();
            playerControls.Player.Dash.Enable();

            // Enable aiming and shooting as well
            GetComponent<PlayerAiming>().EnableShootingControls(); // Enable shooting
        }
        else
        {
            Debug.LogError("Cannot enable movement controls - playerControls is null!");
        }
    }

    public void DisableMovementControls()
    {
        if (playerControls != null)
        {
            playerControls.Player.Move.Disable();
            playerControls.Player.Dash.Disable();

            // Disable aiming and shooting as well
            GetComponent<PlayerAiming>().DisableShootingControls(); // Disable shooting
        }
        else
        {
            Debug.LogError("Cannot disable movement controls - playerControls is null!");
        }
    }

    private void FixedUpdate()
    {
        if (DodgeballCountdown.instance != null && !DodgeballCountdown.instance.canStartGame)
        {
            return;  // Stop player movement until the countdown is over
        }

        if (isTarget && !isDashing)
        {
            Vector3 movement = new Vector3(movementInput.x, 0, 0) * moveSpeed * Time.fixedDeltaTime; // Only allow movement along the x-axis
            rb.MovePosition(rb.position + movement);
        }
    }

    private IEnumerator Dash()
    {
        if (!isTarget || !canDash || isDashing) yield break;

        isDashing = true;
        canDash = false;

        Vector3 dashDirection = new Vector3(movementInput.x, 0, 0).normalized;
        rb.velocity = dashDirection * dashSpeed;
        AudioManager.instance.Play("Dash");

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
        isDashing = false;

        yield return new WaitForSeconds(1f); // 1-second cooldown
        canDash = true;
    }

    public void AddScore(float value)
    {
        score += value;
        score = Mathf.Round(score * 100f) / 100f; // Round to 2 decimal places
        //GetComponent<PlayerStats>().playerData.SetPlayerScore(score);
        //GetComponent<PlayerStats>().playerData.SetTotalScore(score);
    }

    public float GetScore()
    {
        return score;

    }

    public void DisablePhysics()
    {
        if (rb != null)
        {
            rb.isKinematic = true; // This disables physics interactions
            rb.useGravity = false;  // This disables gravity
        }
    }

    public void EnablePhysics()
    {
        if (rb != null)
        {
            rb.isKinematic = false; // This enables physics interactions
            rb.useGravity = true;   // This enables gravity
        }
    }
}