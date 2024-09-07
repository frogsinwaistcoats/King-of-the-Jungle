using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInputBumper : MonoBehaviour
{
    public int playerID;
    MultiplayerInputManager inputManager; 
    public Vector2 moveInput;
    public float moveSpeed;
    public float hitTimer;
    public float pushForce;
    private Rigidbody rb;
   
    public float FallingThreshold = -10f;  
    [HideInInspector]
    public bool Falling = false;

    InputControls inputControls;
    bool isHit;
    SpriteRenderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("attack"))
        {
            
            rend.material.color = Color.white;//turns white when hit
            FindAnyObjectByType<Spawner>().Stop(0.5f);
            StartCoroutine(WaitForSpawn());
        }
    }
    
    IEnumerator WaitForSpawn()
    {
        while (Time.timeScale != 0.5f)
            yield return null;
    }
    void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.Movement.performed += OnMove;
            inputControls.MasterControls.Movement.canceled += OnMove;
        }
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isHit == false)
        {
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime * 100;
            //rb.MovePosition(rb.position + movement);
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }
    }

    public void PlayerHit(Vector3 direction)
    {
        if (isHit == false)
        {
            isHit = true;
            rb.AddForce(direction * pushForce, ForceMode.Impulse);
            Invoke("HitCooldown", hitTimer);
        }
    }

    public void HitCooldown()
    {
        isHit = false;
        rb.velocity = Vector3.zero;
        rb.angularDrag = 0;
    }
    void Update()
    {
        if (rb.velocity.y < FallingThreshold)
        {
            Falling = true;
        }
        else
        {
            Falling = false;
        }

        if (Falling)
        {
            Fell();
        }
    }

    private void Fell()
    {
        Debug.Log("lose");
        SceneManager.LoadScene("Scores");
    }
}
