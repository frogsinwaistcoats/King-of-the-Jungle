using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public float stunDuration = 2f; 
    private bool isStunned = false; 

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
        if (!isStunned)
        {
            isStunned = true;
            StartCoroutine(RecoverFromStun());
        }
    }

    private IEnumerator RecoverFromStun()
    {
        // Wait for the stun duration before allowing the player to move again
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}
