using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAiming : MonoBehaviour
{
    public int playerID;
    public GameObject aimIndicator;
    public bool isShooter;
    public float aimSpeed = 100f;
    public float maxAngle = 60f;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 20f;
    public float shootCooldown = 1f;

    private InputControls playerControls;
    private float keyboardAimInput;
    private Vector2 joystickAimInput;
    private float currentAngle = 0;
    private Quaternion initialRotation;
    private bool canShoot = true;

    public LineRenderer lineRenderer;
    public float lineLength = 0.5f; // Adjust to match Debug.DrawRay's length

    private void Start()
    {
        lineLength = 1f; // Example length, can be changed dynamically
        MultiplayerInputManager.instance.onPlayerJoined += AssignInputs;

        if (aimIndicator != null)
        {
            initialRotation = aimIndicator.transform.localRotation;
            //aimIndicator.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            currentAngle = 0;
        }

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned!");
        }
        else
        {
            // Set the line width and color for visibility
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
    }

    public void SetupControls()
    {
        playerControls.Player.Enable(); // Enable the entire Player action map

        if (playerControls != null)
        {
            // Handle the Line Renderer visibility based on the player's role
            if (lineRenderer != null)
            {
                lineRenderer.enabled = isShooter; // Only enable the line renderer if the player is a shooter
            }

            // Unsubscribe from previous events to avoid duplicate listeners
            UnsubscribeFromEvents();

            if (isShooter)
            {
                // Re-subscribe the input controls for shooters
                SubscribeToShooterControls();

                Debug.Log($"Player {playerID} set as shooter with controls assigned.");
            }
            else
            {
                // Disable shooting for the target player
                playerControls.Player.Shoot.Disable(); // Ensures the target cannot shoot
                Debug.Log($"Player {playerID} is not a shooter.");
            }
        }
        else
        {
            Debug.LogError("Cannot set up controls - playerControls is null!");
        }
    }

    // Method to unsubscribe from all events
    private void UnsubscribeFromEvents()
    {
        playerControls.Player.Shoot.performed -= OnShoot;
        playerControls.Player.AimKeyboardLeft.performed -= OnAimKeyboardLeft;
        playerControls.Player.AimKeyboardLeft.canceled -= OnAimKeyboardLeftCanceled;
        playerControls.Player.AimKeyboardRight.performed -= OnAimKeyboardRight;
        playerControls.Player.AimKeyboardRight.canceled -= OnAimKeyboardRightCanceled;
        playerControls.Player.AimJoystick.performed -= OnAimJoystick;
        playerControls.Player.AimJoystick.canceled -= OnAimJoystickCanceled;
    }

    // Method to subscribe to shooter-specific controls
    private void SubscribeToShooterControls()
    {
        playerControls.Player.Shoot.performed += OnShoot;
        playerControls.Player.AimKeyboardLeft.performed += OnAimKeyboardLeft;
        playerControls.Player.AimKeyboardLeft.canceled += OnAimKeyboardLeftCanceled;
        playerControls.Player.AimKeyboardRight.performed += OnAimKeyboardRight;
        playerControls.Player.AimKeyboardRight.canceled += OnAimKeyboardRightCanceled;
        playerControls.Player.AimJoystick.performed += OnAimJoystick;
        playerControls.Player.AimJoystick.canceled += OnAimJoystickCanceled;
    }

    // Separate methods to handle aiming and shooting
    private void OnAimKeyboardLeft(InputAction.CallbackContext ctx) => keyboardAimInput = -1f;
    private void OnAimKeyboardLeftCanceled(InputAction.CallbackContext ctx) => keyboardAimInput = 0;

    private void OnAimKeyboardRight(InputAction.CallbackContext ctx) => keyboardAimInput = 1f;
    private void OnAimKeyboardRightCanceled(InputAction.CallbackContext ctx) => keyboardAimInput = 0;

    private void OnAimJoystick(InputAction.CallbackContext ctx) => joystickAimInput = ctx.ReadValue<Vector2>();
    private void OnAimJoystickCanceled(InputAction.CallbackContext ctx) => joystickAimInput = Vector2.zero;

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Player {playerID}: Shoot button pressed.");

        if (canShoot)
        {
            ShootProjectile();
            StartCoroutine(ShootCooldown());
        }
    }

    public void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            MultiplayerInputManager.instance.onPlayerJoined -= AssignInputs;
            playerControls = MultiplayerInputManager.instance.players[playerID].playerControls;

            if (playerControls != null)
            {
                Debug.Log($"Player {playerID}: Input controls found. Setting up controls...");
                SetupControls();
            }
            else
            {
                Debug.LogError($"Player {playerID} controls are not assigned properly!");
            }
        }
    }

    private void Update()
    {
        if (isShooter && playerControls != null)
        {
            float inputDirection = 0;

            if (keyboardAimInput != 0)
            {
                inputDirection = keyboardAimInput;
            }
            else if (joystickAimInput.x != 0)
            {
                inputDirection = joystickAimInput.x;
            }

            currentAngle = Mathf.Clamp(currentAngle + inputDirection * aimSpeed * Time.deltaTime, -maxAngle, maxAngle);

            if (aimIndicator != null)
            {
                aimIndicator.transform.localRotation = initialRotation * Quaternion.Euler(currentAngle, 0, 0);
                Debug.DrawRay(aimIndicator.transform.position, aimIndicator.transform.forward * 5, Color.red); // Visualize the aim direction
            }

            if (isShooter && lineRenderer != null)
            {
                DrawAimLine();
            }
        }
    }

    void DrawAimLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2; // Ensure there are exactly 2 points in the line (start and end)

            // Use the same aimDirection and startPosition as the Debug.DrawRay
            Vector3 aimDirection = aimIndicator.transform.forward;
            Vector3 startPosition = aimIndicator.transform.position; // Match this with Debug.DrawRay

            // Calculate the end position based on the lineLength variable
            Vector3 endPosition = startPosition + (aimDirection.normalized * lineLength);

            // Update the LineRenderer positions
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            // Log the shoot point and direction
            Vector3 shootDirection = aimIndicator.transform.forward;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(shootDirection));
            AudioManager.instance.PlayRandomShoot();
            if (projectile != null)
            {

                // Assign shooter ID
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.shooterID = playerID;
                }
                else
                {
                    Debug.LogError($"Player {playerID}: Projectile script missing on the instantiated object.");
                }

                // Set the projectile velocity
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                if (projectileRb != null)
                {
                    projectileRb.velocity = shootDirection * projectileSpeed;
                    Debug.Log($"Player {playerID}: Projectile velocity set to {projectileRb.velocity}");
                }
                else
                {
                    Debug.LogError($"Player {playerID}: Rigidbody component missing on the projectile.");
                }
            }
            else
            {
                Debug.LogError($"Player {playerID}: Failed to spawn the projectile.");
            }
        }
        else
        {
            Debug.LogError($"Player {playerID}: Missing projectilePrefab or shootPoint.");
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}