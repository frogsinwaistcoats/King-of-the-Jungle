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
    public float projectileSpeed = 10f;
    public float shootCooldown = 1f;

    private InputControls playerControls;
    private float keyboardAimInput;
    private Vector2 joystickAimInput;
    private float currentAngle = 0;
    private Quaternion initialRotation;
    private bool canShoot = true;

    public LineRenderer lineRenderer;
    public float lineLength = 5f; // Adjust to match Debug.DrawRay's length

    private void Start()
    {
        MultiplayerInputManager.instance.onPlayerJoined += AssignInputs;

        if (aimIndicator != null)
        {
            initialRotation = aimIndicator.transform.localRotation;
            aimIndicator.transform.localRotation = Quaternion.Euler(-90f, -90f, 0f);
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
        if (playerControls != null)
        {
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

                Debug.Log($"Player {playerID} set as shooter with controls assigned.");
            }
            else
            {
                Debug.Log($"Player {playerID} is not a shooter.");
            }
        }
        else
        {
            Debug.LogError("Cannot set up controls - playerControls is null!");
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
                SetupControls();
                Debug.Log($"Player {playerID} controls assigned successfully.");
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
        Vector3 aimDirection = aimIndicator.transform.forward;
        Vector3 startPosition = shootPoint.position; // Ensure the shootPoint is centered and aligned correctly
        Vector3 endPosition = startPosition + (aimDirection * lineLength);

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            // Get the forward direction from the aim indicator
            Vector3 shootDirection = aimIndicator.transform.forward;

            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(shootDirection));
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.shooterID = playerID;
            }

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.velocity = shootDirection * projectileSpeed;
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}