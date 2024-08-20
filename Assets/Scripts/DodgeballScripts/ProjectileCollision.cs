using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && player.isTarget)
        {
            Projectile projectile = GetComponent<Projectile>();
            if (projectile != null && DodgeballGameManager.instance != null)
            {
                DodgeballGameManager.instance.PlayerHitTarget(projectile.shooterID);
            }
            else
            {
                Debug.LogError("Projectile or GameManager instance not found.");
            }
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