using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public float stunDuration = 2f;
    public float stunCooldown = 20f; // Cooldown duration after being stunned
    private bool isStunned = false;
    private bool canBeStunned = true; // To track if player can be stunned

    private PlayerInputBumper playerMovement;


    private void Start()
    {
        playerMovement = GetComponent<PlayerInputBumper>();
    }

    private void Update()
    {
        if (isStunned)
        {
            // Disable player movement during stun
            playerMovement.moveInput = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else
        {
            // Enable player movement when not stunned
            //playerMovement.enabled = true;
        }
    }

    public void StunPlayer()
    {
        if (!isStunned && canBeStunned)
        {
            StartCoroutine(RecoverFromStun());
            isStunned = true;
            canBeStunned = false; // Disable further stuns
            
        }
    }

    private IEnumerator RecoverFromStun()
    {
        // Wait for the stun duration
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;

        // Wait for the cooldown duration before allowing further stuns
        yield return new WaitForSeconds(stunCooldown);
        canBeStunned = true; // Allow stuns again
    }

}