using UnityEngine;
using UnityEngine.InputSystem;

public class AimIndicatorMovement : MonoBehaviour
{
    public int playerID;
    public Rigidbody rb;
    public float moveSpeed = 100f; // Adjust as needed
    public float maxAngle = 80f; // Max angle for the arc movement (half of 160 degrees)
    public bool isShooter; // Set this in the inspector or dynamically
    private Quaternion initialRotation;

    MultiplayerInputManager inputManager;
    private InputControls playerControls;
    private float keyboardAimInput;
    private Vector2 joystickAimInput;
    private float currentAngle = 0;

    private void Start()
    {
        // Store the initial rotation of the aim indicator
        initialRotation = transform.localRotation;
        inputManager = MultiplayerInputManager.instance;

        inputManager.onPlayerJoined += AssignInputs;
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            //Debug.Log($"Player {ID} assigned inputs.");
            inputManager.onPlayerJoined -= AssignInputs;
            playerControls = inputManager.players[playerID].playerControls;

            if (isShooter)
            {
                playerControls.Player.AimKeyboardLeft.performed += ctx => {
                    keyboardAimInput = -1f; // Left input
                    //Debug.Log($"Keyboard left aim input received: {keyboardAimInput}");
                };
                playerControls.Player.AimKeyboardLeft.canceled += ctx => keyboardAimInput = 0;

                playerControls.Player.AimKeyboardRight.performed += ctx => {
                    keyboardAimInput = 1f; // Right input
                    //Debug.Log($"Keyboard right aim input received: {keyboardAimInput}");
                };
                playerControls.Player.AimKeyboardRight.canceled += ctx => keyboardAimInput = 0;

                playerControls.Player.AimJoystick.performed += ctx => {
                    joystickAimInput = ctx.ReadValue<Vector2>();
                    //Debug.Log($"Joystick aim input received: {joystickAimInput}");
                };
                playerControls.Player.AimJoystick.canceled += ctx => joystickAimInput = Vector2.zero;
            }
        }
    }

    private void Update()
    {
        if (isShooter && playerControls != null)
        {
            float inputDirection = 0;

            // Handle keyboard input for aiming
            if (keyboardAimInput != 0)
            {
                inputDirection = keyboardAimInput;
            }

            // Handle joystick input for aiming
            if (joystickAimInput.x != 0)
            {
                inputDirection = joystickAimInput.x;
            }

            // Update the current angle with smooth rotation
            currentAngle = Mathf.Clamp(currentAngle + inputDirection * moveSpeed * Time.deltaTime, -maxAngle, maxAngle);

            // Apply the rotation to the aim indicator
            transform.localRotation = initialRotation * Quaternion.Euler(currentAngle, 0, 0);
            //Debug.Log($"Aim Indicator {playerID} aiming with angle: {currentAngle}");
        }
    }
}
