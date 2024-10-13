using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public float stunDuration = 2f; // Duration for which the player is stunned
    private bool isStunned = false; // Flag to check if the player is stunned

    private PlayerInputBumper playerMovement; // Reference to the player's movement script (you should have one)

    private void Start()
    {
        playerMovement = GetComponent<PlayerInputBumper>(); // Assuming the player has a movement script
    }

    private void Update()
    {
        if (isStunned)
        {
            // Disable player movement during stun
            playerMovement.enabled = false;
        }
        else
        {
            // Enable player movement when not stunned
            playerMovement.enabled = true;
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
