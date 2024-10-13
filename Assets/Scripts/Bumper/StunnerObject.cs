using UnityEngine;

public class StunnerObject : MonoBehaviour
{
    public float stunForce = 10f; 
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerStun playerStun = collision.gameObject.GetComponent<PlayerStun>();
            if (playerStun != null)
            {
                playerStun.StunPlayer();
            }
        }
    }
}
