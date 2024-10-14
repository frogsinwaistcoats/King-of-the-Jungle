using UnityEngine;

public class StunnerObject : MonoBehaviour
{
    public AudioSource tickSource;
    private void Start()
    {
        tickSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            tickSource.Play();
            PlayerStun playerStun = collision.gameObject.GetComponent<PlayerStun>();
            if (playerStun != null)
            {
                playerStun.StunPlayer();
            }
        }
    }
}
