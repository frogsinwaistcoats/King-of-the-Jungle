using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && player.isTarget)
        {
            GameManager.instance.PlayerHitTarget(GetComponent<Projectile>().shooterID);
            Destroy(gameObject); // Destroy the projectile
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball touches the floor
        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }

        // Check if the ball touches the target player
        if (other.gameObject.CompareTag("TargetPlayer"))
        {
            Destroy(gameObject);
        }
    }
}