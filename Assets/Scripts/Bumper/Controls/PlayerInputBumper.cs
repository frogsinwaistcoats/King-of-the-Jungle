using System.Collections;
using UnityEngine;

public class PlayerInputBumper : MonoBehaviour
{
    public int playerID;
    public Vector2 moveInput;
    public float moveSpeed;
    public float hitTimer;
    public float pushForce;

    private Rigidbody rb;
    private bool isHit;
    private bool hasFallen = false;

    private MultiplayerInputManager inputManager;
    private InputControls inputControls;

    BumperFinishManager finishManager; // Reference to the Finish Manager

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        finishManager = BumperFinishManager.instance; // Ensure the finish manager is assigned
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

    private void OnDisable()
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

    // Handle both bumping and falling off
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player bump logic
            Vector3 direction = (other.transform.position - transform.position).normalized;

            // Apply force to both players in opposite directions
            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                otherRb.AddForce(direction * pushForce, ForceMode.Impulse);
            }
            rb.AddForce(-direction * pushForce, ForceMode.Impulse);

            StartCoroutine(HitCooldown()); // Start cooldown to prevent instant bumping again
        }
    }

    // Handle falling off the platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BumperFinish") && !hasFallen) // Now using "BumperFinish"
        {
            hasFallen = true; // Ensure this only triggers once

            // Log the fall and destroy the player
            //Debug.Log($"Player {playerID} fell!");

            // Call PlayerFinish to calculate score and place
            int placing = finishManager.PlayerFinish(playerID);
            float score = finishManager.CalculateScore(placing);
            GetComponent<PlayerStats>().playerData.SetPlayerScore(score);
            GetComponent<PlayerStats>().playerData.SetTotalScore(score);

            // Destroy the player object after falling off
            Destroy(gameObject);

            //Debug.Log($"Player {playerID} Placing: {placing} with score {score}");
        }
    }

    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.Movement.performed += OnMove;
            inputControls.MasterControls.Movement.canceled += OnMove;
        }
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!isHit && BumperCountdown.instance.canMove)
        {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime * 100;
            movement.y = rb.velocity.y; // Preserve vertical velocity (gravity)
            rb.velocity = movement;
        }
    }

    private IEnumerator HitCooldown()
    {
        isHit = true;
        yield return new WaitForSeconds(hitTimer);
        isHit = false;
        rb.velocity = Vector3.zero;
    }
}