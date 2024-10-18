using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public float stunDuration = 2f;
    public float stunCooldown = 1f; 
    private bool isStunned = false;
    private bool canBeStunned = true; 

    private PlayerInputBumper playerMovement;


    private void Start()
    {
        playerMovement = GetComponent<PlayerInputBumper>();
    }

    private void Update()
    {
        if (isStunned)
        {
            
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
            canBeStunned = false; 
            
        }
    }

    private IEnumerator RecoverFromStun()
    {
        
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;

       
        yield return new WaitForSeconds(stunCooldown);
        canBeStunned = true; 
    }

}