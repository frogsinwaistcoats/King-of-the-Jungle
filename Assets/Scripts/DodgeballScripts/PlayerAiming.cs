using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    public int playerID;
    public GameObject aimIndicator; // The actual aiming indicator
    public GameObject visualIndicator; // The visual indicator
    public bool isShooter;
    public float aimSpeed = 100f; // Adjust aim speed as needed
    public float maxAngle = 60f; // Max angle for the arc movement (60 degrees left/right)
    public GameObject projectilePrefab;
    public Transform shootPoint; // Point from which projectiles are instantiated
    public float projectileSpeed = 10f; // Speed of the projectile

    MultiplayerInputManager inputManager;
    private InputControls playerControls;
    private float keyboardAimInput;
    private Vector2 joystickAimInput;
    private float currentAngle = 0;
    private Quaternion initialRotation;

    private void Start()
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

        // Ensure aim indicator starts in the correct orientation
        if (aimIndicator != null)
        {
            // Adjust this to match your setup
            initialRotation = aimIndicator.transform.localRotation;
            aimIndicator.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
            currentAngle = 0; // Ensure the current angle starts at 0
        }
    }

    //private void OnDisable()
    //{
    //    if (playerControls != null)
    //    {
    //        playerControls.Disable();
    //    }
    //}

    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            //Debug.Log($"Player {ID} assigned inputs.");
            inputManager.onPlayerJoined -= AssignInputs;
            playerControls = inputManager.players[playerID].playerControls;

            if (isShooter)
            {
                playerControls.Player.AimKeyboardLeft.performed += ctx => keyboardAimInput = -1f;
                playerControls.Player.AimKeyboardLeft.canceled += ctx => keyboardAimInput = 0;

                playerControls.Player.AimKeyboardRight.performed += ctx => keyboardAimInput = 1f;
                playerControls.Player.AimKeyboardRight.canceled += ctx => keyboardAimInput = 0;

                playerControls.Player.AimJoystick.performed += ctx => joystickAimInput = ctx.ReadValue<Vector2>();
                playerControls.Player.AimJoystick.canceled += ctx => joystickAimInput = Vector2.zero;

                playerControls.Player.Shoot.performed += ctx => ShootProjectile();

                // Ensure the aim indicator is properly oriented
                if (aimIndicator != null)
                {
                    aimIndicator.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
                    currentAngle = 0; // Reset current angle
                }
            }
        }
    }

    private void Update()
    {
        if (isShooter && playerControls != null)
        {
            float inputDirection = 0;

            // Combine keyboard and joystick inputs for aiming
            if (keyboardAimInput != 0)
            {
                inputDirection = keyboardAimInput;
            }
            else if (joystickAimInput.x != 0)
            {
                inputDirection = joystickAimInput.x;
            }

            // Calculate the new angle within the limits
            currentAngle = Mathf.Clamp(currentAngle + inputDirection * aimSpeed * Time.deltaTime, -maxAngle, maxAngle);

            // Rotate the aim indicator along the x-axis
            if (aimIndicator != null)
            {
                aimIndicator.transform.localRotation = initialRotation * Quaternion.Euler(currentAngle, 0, 0);
                //Debug.Log($"Aim Indicator {playerID} aiming with angle: {currentAngle}");
            }
        }
    }

    private void ShootProjectile()
    {
        if (shootPoint != null && projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Shoot the projectile in the direction the aim indicator is pointing
                rb.velocity = aimIndicator.transform.forward * projectileSpeed;
                //Debug.Log($"Projectile shot with velocity: {rb.velocity}");
            }
        }
    }
}