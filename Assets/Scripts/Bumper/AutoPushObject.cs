using UnityEngine;

public class AutoPushObjects : MonoBehaviour
{
    public float pushForce = 10f;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * pushDistance, pushDistance, pushableLayer);

        foreach (Collider collider in colliders)
        {
            Rigidbody objectRb = collider.GetComponent<Rigidbody>();
            if (objectRb != null)
            {
                // Calculate direction to push
                Vector3 pushDirection = (collider.transform.position - transform.position).normalized;
                objectRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}
