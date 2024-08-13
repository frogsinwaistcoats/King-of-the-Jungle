using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int playerID;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public bool isTarget;

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private Vector2 movementInput;

    public InputControls playerControls;
    private float score;

    MultiplayerInputManager multiplayerInputManager;

    private void Start()
    {
        MultiplayerInputManager.instance.onPlayerJoined += AssignInputs;
        multiplayerInputManager = MultiplayerInputManager.instance;
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
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
                playerControls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
                playerControls.Player.Move.canceled += ctx => movementInput = Vector2.zero;

                playerControls.Player.Dash.performed += ctx => StartCoroutine(Dash());
            }
            else
            {
                Debug.LogError($"Player {ID} controls not set up correctly.");
            }
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
        }
        else
        {
            Debug.LogError("Cannot disable movement controls - playerControls is null!");
        }
    }

    private void FixedUpdate()
    {
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

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
        isDashing = false;

        yield return new WaitForSeconds(1f); // 1-second cooldown
        canDash = true;
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public float GetScore()
    {
        return score;
    }
}