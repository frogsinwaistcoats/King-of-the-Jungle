using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    SceneLoader sceneLoader;

    private void Start()
    {
        // Find the GameManager in the scene
        sceneLoader = SceneLoader.instance;
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
            sceneLoader.ContinueGame();
        }
    }
}