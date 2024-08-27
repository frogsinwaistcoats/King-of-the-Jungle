using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullOnButtonPress : MonoBehaviour
{
    public static PullOnButtonPress instance;  

    public Transform target; // The point to pull the object towards
    public float pullForce = 10f; // The strength of the pull
    public KeyCode pullKey = KeyCode.Space; // The key to pull the object

    private Rigidbody rb;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the pull button is pressed
        if (Input.GetKey(pullKey))
        {
            PullTowardsTarget();
        }
    }

    public void PullTowardsTarget()
    {
        if (target != null)
        {
            // Calculate direction from the object to the target
            Vector3 direction = (target.position - rb.position).normalized;

            // Apply force towards the target
            rb.AddForce(direction * pullForce);
        }
    }
}
