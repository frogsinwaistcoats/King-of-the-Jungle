using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 15f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private CountdownTimer countdownTimer;
    private InputManager1 inputManager;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
        inputManager = FindObjectOfType<InputManager1>();
    }

    void FixedUpdate()
    {
        if (countdownTimer.CanMove())
        {
            Vector2 moveInput = inputManager.move;
            Vector3 movement = new Vector3(moveInput.x, 0f, 0f);
            movement = movement.normalized * speed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

            if (inputManager.jump && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                inputManager.jump = false; // Reset jump input after applying force
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }
}