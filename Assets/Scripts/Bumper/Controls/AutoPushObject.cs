using UnityEngine;

public class AutoPushObjects : MonoBehaviour
{
    //public float pushForce = 10f;
    public float pushDistance = 1f;
    public LayerMask pushableLayer;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Push objects based on movement
        PushObjects();
    }

    void PushObjects()
    {
        // Check for objects within a certain distance in front of the character
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushDistance / 2, pushableLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Rigidbody objectRb = collider.GetComponent<Rigidbody>();
                Debug.Log(Vector3.Distance(transform.position, collider.transform.position));
                if (objectRb != null)
                {
                    // Calculate direction to push
                    Vector3 pushDirection = (collider.transform.position - transform.position).normalized;
                    //collider.GetComponent<PlayerInputBumper>().PlayerHit(pushDirection);
                }
            }
        }
    }
}
