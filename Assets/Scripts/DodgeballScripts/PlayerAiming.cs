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

        // Ensure aim indicator starts in the correct orientation
        if (aimIndicator != null)
        {
            initialRotation = aimIndicator.transform.localRotation;
            aimIndicator.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
            currentAngle = 0; // Ensure the current angle starts at 0
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

    private void Update()
    {
        if (isShooter && playerControls != null)
        {
            float inputDirection = DetermineInputDirection();
            RotateAimIndicator(inputDirection);

            // Determine input direction
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

            // Apply the rotation to the aim indicators
            if (aimIndicator != null)
            {
                aimIndicator.transform.localRotation = initialRotation * Quaternion.Euler(0, currentAngle, 0);
            }
            if (visualIndicator != null)
            {
                visualIndicator.transform.localRotation = initialRotation * Quaternion.Euler(0, currentAngle, 0);
            }
        }
    }

    float DetermineInputDirection()
    {
        float inputDirection = 0;
        if (keyboardAimInput != 0)
            inputDirection = keyboardAimInput;
        else if (joystickAimInput.x != 0)
            inputDirection = joystickAimInput.x;

        return inputDirection;
    }

    void RotateAimIndicator(float inputDirection)
    {
        currentAngle = Mathf.Clamp(currentAngle + inputDirection * aimSpeed * Time.deltaTime, -maxAngle, maxAngle);
        shootPoint.localRotation = Quaternion.Euler(0, currentAngle, 0); // Adjust rotation based on input
        if (visualIndicator != null)
            visualIndicator.transform.localRotation = Quaternion.Euler(0, currentAngle, 0); // Visual only
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