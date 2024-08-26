using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerMovement : MonoBehaviour
{

    public float speed;
    float h;
    float v;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(h, v) * Time.deltaTime * speed * 100;
        rb.velocity = moveDirection;
    }

}
