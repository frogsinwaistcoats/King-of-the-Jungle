using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
    public float shootCooldown = 1f; // Cooldown time between shots

    private InputControls playerControls;
    private float keyboardAimInput;
    private Vector2 joystickAimInput;
    private float currentAngle = 0;
    private Quaternion initialRotation;
    private bool canShoot = true;

    private void Start()
    {
        MultiplayerInputManager.instance.onPlayerJoined += AssignInputs;

        if (shootPoint != null)
        {
            // Capture the initial rotation at start to maintain a baseline for adjustments
            initialRotation = shootPoint.localRotation;
        }

        ResetAimIndicator();
    }

    private void ResetAimIndicator()
    {
        if (shootPoint != null)
        {
            // Reset rotation to initial state or another predefined state
            shootPoint.localRotation = initialRotation;
            if (visualIndicator != null)
                visualIndicator.transform.localRotation = initialRotation; // Ensure visual indicator matches
        }
    }
    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            MultiplayerInputManager.instance.onPlayerJoined -= AssignInputs;
            playerControls = MultiplayerInputManager.instance.players[playerID].playerControls;

            if (isShooter)
            {
                playerControls.Player.AimKeyboardLeft.performed += ctx => keyboardAimInput = -1f;
                playerControls.Player.AimKeyboardLeft.canceled += ctx => keyboardAimInput = 0;

                playerControls.Player.AimKeyboardRight.performed += ctx => keyboardAimInput = 1f;
                playerControls.Player.AimKeyboardRight.canceled += ctx => keyboardAimInput = 0;

                playerControls.Player.AimJoystick.performed += ctx => joystickAimInput = ctx.ReadValue<Vector2>();
                playerControls.Player.AimJoystick.canceled += ctx => joystickAimInput = Vector2.zero;

                playerControls.Player.Shoot.performed += ctx => {
                    if (canShoot)
                    {
                        ShootProjectile();
                        StartCoroutine(ShootCooldown());
                    }
                };

                // Ensure the aim indicator is properly oriented
                if (aimIndicator != null)
                {
                    aimIndicator.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
                    currentAngle = 0; // Reset current angle
                }
            }
        }
    }

    public void SetupControls()
    {
        if (isShooter)
        {
            playerControls.Player.AimKeyboardLeft.Enable();
            playerControls.Player.AimKeyboardRight.Enable();
            playerControls.Player.AimJoystick.Enable();
            playerControls.Player.Shoot.Enable();
        }
        else
        {
            playerControls.Player.AimKeyboardLeft.Disable();
            playerControls.Player.AimKeyboardRight.Disable();
            playerControls.Player.AimJoystick.Disable();
            playerControls.Player.Shoot.Disable();
        }
    }

    private void Update()
    {
        if (isShooter && playerControls != null)
        {
            float inputDirection = DetermineInputDirection();
            RotateAimIndicator(inputDirection);
        }
    }

    float DetermineInputDirection()
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

        return inputDirection;
    }

    void RotateAimIndicator(float inputDirection)
    {
        // Update the angle based on input, clamping it within the maximum range allowed
        currentAngle = Mathf.Clamp(currentAngle + inputDirection * aimSpeed * Time.deltaTime, -maxAngle, maxAngle);

        // Set the rotation of the visual indicator
        Quaternion visualRotation = Quaternion.Euler(-currentAngle, 0, 0);
        if (visualIndicator != null)
        {
            visualIndicator.transform.localRotation = initialRotation * visualRotation;
        }

        // Set the rotation of the actual aim indicator with an additional -90 degrees offset on the x-axis
        Quaternion aimRotation = Quaternion.Euler(-currentAngle +90, 0, 0);
        if (aimIndicator != null)
        {
            aimIndicator.transform.localRotation = initialRotation * aimRotation;
        }
    }

    private void ShootProjectile()
    {
        if (shootPoint != null && projectilePrefab != null && aimIndicator != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Shoot the projectile in the direction the aim indicator is pointing
                rb.velocity = aimIndicator.transform.forward * projectileSpeed;
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}