using UnityEngine;

public class AutoPushObjects : MonoBehaviour
{
    public float pushForce = 50f;
    public float pushDistance = 2f;
    public LayerMask pushableLayer;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        PushObjects();
    }


    void PushObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushDistance, pushableLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Rigidbody objectRb = collider.GetComponent<Rigidbody>();

                if (objectRb != null)
                {
                    Vector3 pushDirection = (collider.transform.position - transform.position).normalized; // Calculate direction to push

                    objectRb.AddForce(pushForce * pushDirection, ForceMode.Impulse);
                }
            }
        }
    }
}
