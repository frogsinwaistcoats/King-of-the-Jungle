using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public int playerID;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;
    public bool isShooter; // Set this in the inspector or dynamically

    MultiplayerInputManager inputManager;
    private InputControls playerControls;

    private void Start()
    {
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

    //private void OnDisable()
    //{
    //    if (playerControls != null)
    //    {
    //        playerControls.Disable();
    //    }
    //}

    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            playerControls = inputManager.players[playerID].playerControls;

            if (isShooter)
            {
                playerControls.Player.Shoot.performed += ctx => Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * projectileSpeed; // Use forward direction for shooting
    }
}
